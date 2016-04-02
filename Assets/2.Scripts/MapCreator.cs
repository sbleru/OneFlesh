using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BlockCreator))]
public class MapCreator : MonoBehaviour {
	public static float BLOCK_WIDTH = 1.0f;
	public static float BLOCK_HEIGHT = 0.2f;
	public static int BLOCK_NUM_IN_SCREEN = 40;
	public static int SCREEN_HEIGHT = 22;
	// ブロックの種類
	public static int RED = 0;
	public static int BLUE = 1;
	public static int METAL = 2;
	public static int NEEDLE = 3;

	public float INTERVAL;  // ブロックの出現間隔 ゲームの難易度を決める

	private LevelCtrl _level_ctrl;
	public LevelCtrl level_ctrl
	{
		get { 
			_level_ctrl = _level_ctrl ?? (gameObject.AddComponent<LevelCtrl> ());
			return this._level_ctrl; 
		}
	}

	private BlockCreator _block_creator;
	public BlockCreator block_creator
	{
		get { 
			_block_creator = _block_creator ?? (this.gameObject.GetComponent<BlockCreator> ());
			return this._block_creator; 
		}
	}

	private ScoreCtrl _score_ctrl;
	public ScoreCtrl score_ctrl
	{
		get { 
			_score_ctrl = _score_ctrl ?? (this.gameObject.GetComponent<ScoreCtrl> ());
			return this._score_ctrl; 
		}
	}

	public TextAsset level_data_txt = null;
	public TextAsset hlevel_data_txt = null;
	public TextAsset vhlevel_data_txt = null;

	// 上下のブロック
	private struct BorderBlock{
		public bool is_last_block;
		public Vector3 block_pos;
	};
		
	BorderBlock last_block, last_up_block;
	[SerializeField]
	private GameObject player_keeper;

	// Use this for initialization
	void Start () {
//		player_keeper = GameObject.FindGameObjectWithTag ("Keeper");
		last_block.is_last_block = false;
		last_up_block.is_last_block = false;
		// 最初の敵を作成
		this.create_block ();
		this.level_ctrl.initialize ();
		// レベルに応じたテキストを読み込む
		switch(GameMgr.scroll_stage_num){
		case 1:
			this.level_ctrl.loadLevelData (this.level_data_txt);
			break;
		case 2:
			this.level_ctrl.loadLevelData (this.hlevel_data_txt);
			break;
		case 3:
			this.level_ctrl.loadLevelData (this.vhlevel_data_txt);
			break;
		default:
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// しきい値の位置
		float block_generation_x = player_keeper.transform.position.x + (((float)BLOCK_NUM_IN_SCREEN + 1) / 2.0f);
		// プレイヤーがしきい値を超えたらブロックを作成する
		if(this.last_block.block_pos.x < block_generation_x){
			create_border_block ();
		}
	}


	// ブロックを作成する位置を決める
	private void create_border_block(){
 		//Floor作成
		// これから作るブロックの位置
		Vector3 block_position;
		Vector3 up_block_position;
		
		// 最後のブロックが作られていなかったら
		if(!last_block.is_last_block){
			
			// ブロックの位置をとりあえずプレイヤーの下にする
			block_position = player_keeper.transform.position;
			// ブロックの位置をスクリーンの左端に移動
			block_position.x -= BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
			// ブロックのY位置を上下の境界線にする
			block_position.y = -((float)SCREEN_HEIGHT - 1.0f) / 2.0f;
		} else {
			// 今回作るブロックを前回作ったブロックと同じ位置にする
			block_position = last_block.block_pos;
		}

		// 最後のブロックが作られていなかったら
		if(!last_up_block.is_last_block){

			// ブロックの位置をとりあえずプレイヤーの下にする
			up_block_position = player_keeper.transform.position;
			// ブロックの位置をスクリーンの左端に移動
			up_block_position.x -= BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
			// ブロックのY位置を上下の境界線にする
			up_block_position.y = ((float)SCREEN_HEIGHT - 1.0f) / 2.0f;
		} else {
			// 今回作るブロックを前回作ったブロックと同じ位置にする
			up_block_position = last_up_block.block_pos;
		}

		//ブロックの位置を1ブロック分、右へ移動
		block_position.x += BLOCK_WIDTH;
		up_block_position.x += BLOCK_WIDTH;

		// ブロック作成指示
		if (GameMgr.scroll_stage_num == 1 || GameMgr.scroll_stage_num == 2) {
			block_creator.createBlock2 (block_position, METAL, true);
			block_creator.createBlock2 (up_block_position, METAL, true);
		} 
		else if(GameMgr.scroll_stage_num == 3){
//			block_creator.createBlock2 (block_position, NEEDLE, true);
//			block_creator.createBlock2 (up_block_position, NEEDLE, true);
		}


		// last_blockを更新
		last_block.block_pos = block_position;
		last_up_block.block_pos = up_block_position;
		// 
		last_block.is_last_block = true;
		last_up_block.is_last_block = true;
	}


//	// ブロックを作るかどうか
//	public bool isCreate(GameObject block){
//		// レベルデザインのためスコアを渡す
//		INTERVAL = this.level_ctrl.getPlayerInterval (this.score_ctrl.Return ());
//
//		bool ret = false;
//		// ブロックが出てくる間隔 しきい値で指定する
//		float block_limit = player_keeper.transform.position.x - (((float)BLOCK_NUM_IN_SCREEN) / 2.0f) + INTERVAL;
//		if (block.transform.position.x < block_limit) {
//			this.create_block ();
//			ret = true;
//		}
//		return(ret);
//	}

	// ブロックを作るかどうか
	public bool isCreate(float elapsed_time){
		// レベルデザインのためスコアを渡す
		INTERVAL = this.level_ctrl.getPlayerInterval (this.score_ctrl.Return ());

		bool ret = false;
		// ブロックが出てくる時間間隔 しきい値で指定する
		float block_limit = INTERVAL;
		if (elapsed_time > block_limit) {
			this.create_block ();
			ret = true;
		}
		return(ret);
	}

	// ブロックの位置を決定
	public void create_block(){
		// これから作るブロックの位置
		Vector3 next_block_position;
		// ブロックの位置をとりあえずプレイヤーの下にする
		next_block_position = player_keeper.transform.position;
		// ブロックのX位置
		next_block_position.x += BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
		//next_block_position.x = Random.Range(next_block_position.x, next_block_position.x + INTERVAL/2);
		// ブロックのY位置
		next_block_position.y = Random.Range (-(float)SCREEN_HEIGHT/2.0f, (float)SCREEN_HEIGHT/2.0f);
		// ブロック作成指示  ステージレベルごとに
		if(GameMgr.scroll_stage_num == 1){
			block_creator.createBlock2 (next_block_position, Random.Range(0,2), false);
		}
		else if(GameMgr.scroll_stage_num == 2){
			switch(Random.Range(1,4) % 3){
			case 0:
				block_creator.createBlock2 (next_block_position, RED, false);
				break;
			case 1:
				block_creator.createBlock2 (next_block_position, BLUE, false);
				break;
			case 2:
				block_creator.createBlock2 (next_block_position, NEEDLE, false);
				break;
			default:
				break;
			}
			//block_creator.createBlock2 (next_block_position, Random.Range(0,4), false);
		}
		else if(GameMgr.scroll_stage_num == 3){
			switch(Random.Range(1,4) % 3){
			case 0:
				block_creator.createBlock2 (next_block_position, RED, false);
				break;
			case 1:
				block_creator.createBlock2 (next_block_position, BLUE, false);
				break;
			case 2:
				block_creator.createBlock2 (next_block_position, NEEDLE, false);
				break;
			default:
				break;
			}
			//block_creator.createBlock2 (next_block_position, Random.Range(0,4), false);
		}
	}

	// 渡されたオブジェクトを削除するか判定
	public bool isDelete(GameObject block_obj){
		bool ret = false;
		// しきい値の計算
		float limit = player_keeper.transform.position.x - (((float)BLOCK_NUM_IN_SCREEN + 8) / 2.0f);
		// ブロックがしきい値をでていたら削除の指示
		if(block_obj.transform.position.x < limit){
			ret = true;
		}

		return(ret);
	}

	// プレイヤーがステージ外へ出ているか
	public bool isOut(GameObject player){
		bool ret = false;

		do {
			// 左のしきい値の計算
			float limit_left = player_keeper.transform.position.x - (((float)BLOCK_NUM_IN_SCREEN + 8) / 2.0f);
			// ブロックがしきい値をでていたら削除の指示
			if(player.transform.position.x < limit_left + 2.0f){
				ret = true;
				break;
			}
			// 上のしきい値の計算
			float limit_up = player_keeper.transform.position.y + (float)SCREEN_HEIGHT / 2.0f;
			if(player.transform.position.y > limit_up + 3.0f){
				ret = true;
				break;
			}
			// 上のしきい値の計算
			float limit_down = player_keeper.transform.position.y - (float)SCREEN_HEIGHT / 2.0f;
			if(player.transform.position.y < limit_down - 3.0f){
				ret = true;
				break;
			}
		} while(false);

		return ret;
	}

}
