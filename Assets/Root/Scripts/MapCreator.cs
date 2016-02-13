using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BlockCreator))]
public class MapCreator : MonoBehaviour {
	public static float BLOCK_WIDTH = 1.0f;
	public static float BLOCK_HEIGHT = 0.2f;
	public static int BLOCK_NUM_IN_SCREEN = 24;
	public static int SCREEN_HEIGHT = 12;

	public float INTERVAL;  // エネミーの出現間隔 ゲームの難易度を決める

	public LevelCtrl level_ctrl = null;
	public TextAsset level_data_txt = null;

	private ScoreCtrl score_ctrl;

	private struct FloorBlock{
		public bool is_last_block;
		public Vector3 block_pos;
	};

	FloorBlock last_block;
	BlockCreator block_creator;
	GameObject player_ctrl;

	// Use this for initialization
	void Start () {
		block_creator = this.gameObject.GetComponent<BlockCreator> ();
		player_ctrl = GameObject.FindGameObjectWithTag ("Keeper");
		last_block.is_last_block = false;
		// 最初の敵を作成
		this.create_enemy ();

		this.level_ctrl = new LevelCtrl ();
		this.level_ctrl.initialize ();
		this.level_ctrl.loadLevelData (this.level_data_txt);
		this.score_ctrl = new ScoreCtrl ();
	}
	
	// Update is called once per frame
//	void Update () {
//		// しきい値の位置
//		float block_generation_x = player_ctrl.transform.position.x + (((float)BLOCK_NUM_IN_SCREEN + 1) / 2.0f);
//		// プレイヤーがしきい値を超えたらブロックを作成する
//		if(this.last_block.block_pos.x < block_generation_x){
//			create_floor_block ();
//		}
//	}
//
//	// ブロックを作成する位置を決める
//	private void create_floor_block(){
//
// Floor作成
//		// これから作るブロックの位置
//		Vector3 block_position;
//		
//		// 最後のブロックが作られていなかったら
//		if(!last_block.is_last_block){
//			
//			// ブロックの位置をとりあえずプレイヤーの下にする
//			block_position = player_ctrl.transform.position;
//			// ブロックの位置をスクリーンの左端に移動
//			block_position.x -= BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
//			// ブロックのY位置を0に
//			block_position.y = 0.0f;
//
//		} else {
//			// 今回作るブロックを前回作ったブロックと同じ位置にする
//			block_position = last_block.block_pos;
//		}
//
//		//ブロックの位置を1ブロック分、右へ移動
//		block_position.x += BLOCK_WIDTH;
//
//		// ブロック作成指示
//		block_creator.createBlock (block_position);
//
//		// last_blockを更新
//		last_block.block_pos = block_position;
//		// 
//		last_block.is_last_block = true;
//	}

	// エネミーを作るかどうか
	public bool isCreate(GameObject enemy){
		// レベルデザインのためスコアを渡す
		INTERVAL = this.level_ctrl.getPlayerInterval (this.score_ctrl.Return ());

		bool ret = false;
		// エネミーが出てくる間隔 しきい値で指定する
		float enemy_limit = player_ctrl.transform.position.x - (((float)BLOCK_NUM_IN_SCREEN) / 2.0f) + INTERVAL;
		if (enemy.transform.position.x < enemy_limit) {
			this.create_enemy ();
			ret = true;
		}
		return(ret);
	}

	// エネミーの位置を決定
	public void create_enemy(){
		// これから作るエネミーの位置
		Vector3 next_enemy_position;
		// エネミーの位置をとりあえずプレイヤーの下にする
		next_enemy_position = player_ctrl.transform.position;
		// エネミーのX位置
		next_enemy_position.x += BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
		//next_enemy_position.x = Random.Range(next_enemy_position.x, next_enemy_position.x + INTERVAL/2);
		// エネミーのY位置
		next_enemy_position.y = Random.Range (-(float)SCREEN_HEIGHT, (float)SCREEN_HEIGHT);
		// エネミー作成指示
		block_creator.createBlock (next_enemy_position);
	}

	// 渡されたオブジェクトを削除するか判定
	public bool isDelete(GameObject block_obj){
		bool ret = false;
		// しきい値の計算
		float limit = player_ctrl.transform.position.x - (((float)BLOCK_NUM_IN_SCREEN + 5) / 2.0f);
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
			float limit_left = player_ctrl.transform.position.x - (((float)BLOCK_NUM_IN_SCREEN + 5) / 2.0f);
			// ブロックがしきい値をでていたら削除の指示
			if(player.transform.position.x < limit_left){
				ret = true;
				break;
			}
			// 上のしきい値の計算
			float limit_up = player_ctrl.transform.position.y + (float)SCREEN_HEIGHT;
			if(player.transform.position.y > limit_up){
				ret = true;
				break;
			}
			// 上のしきい値の計算
			float limit_down = player_ctrl.transform.position.y - (float)SCREEN_HEIGHT;
			if(player.transform.position.y < limit_down){
				ret = true;
				break;
			}
		} while(false);

		return ret;
	}
}
