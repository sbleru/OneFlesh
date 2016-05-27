using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BlockCreator))]
public class MapCreator : MonoBehaviour {

	#region const
	// ブロックの生成の間隔に利用
	private const float BLOCK_WIDTH = 1.0f;

	// プレイヤーが画面から外れた場合のゲームオーバーの閾値に利用
	private const int BLOCK_NUM_IN_SCREEN_WIDTH  = 41; /*上下の壁が画面から見切れない値　調整が必要かも*/
	private const int BLOCK_NUM_IN_SCREEN_HEIGHT = 22;

	// 生成するブロックの種類に番号を割り当てる
	private const int RED = 0, BLUE = 1, METAL = 2, NEEDLE = 3;

	#endregion


	#region public property

	private LevelCtrl _level_ctrl;
	public LevelCtrl level_ctrl
	{
		get { 
			_level_ctrl = _level_ctrl ?? (gameObject.AddComponent<LevelCtrl> ());
			return this._level_ctrl; 
		}
	}

	#endregion


	#region private property

	private BlockCreator _block_creator;
	private BlockCreator block_creator
	{
		get { 
			_block_creator = _block_creator ?? (this.gameObject.GetComponent<BlockCreator> ());
			return this._block_creator; 
		}
	}

	private ScoreCtrl _score_ctrl;
	private ScoreCtrl score_ctrl
	{
		get { 
			_score_ctrl = _score_ctrl ?? (this.gameObject.GetComponent<ScoreCtrl> ());
			return this._score_ctrl; 
		}
	}
	// カメラが追いかけるオブジェクトで常に画面の中心に位置する　スクロール速度の管理
	[SerializeField]
	private GameObject player_keeper;
	[SerializeField]
	private TextAsset level_data_txt = null;

	// 上下の壁を作るブロック
	private struct WallBlock{
		public bool isFirstBlock;
		public bool isUpWall;
		public Vector2 last_pos;
	};
	// 壁を構成する最新のブロック
	WallBlock bottom_wall_block, up_wall_block;

	#endregion


	#region event

	// Use this for initialization
	void Start () {
		bottom_wall_block.isFirstBlock = true;
		up_wall_block.isFirstBlock = true;

		bottom_wall_block.isUpWall = false;
		up_wall_block.isUpWall = true;
		// 最初の敵を作成
		this.CreateNotWallBlock ();
		this.level_ctrl.initialize ();
		this.level_ctrl.loadLevelData (this.level_data_txt);
	}
	
	// Update is called once per frame
	void Update () {
		// しきい値の位置
		float generate_new_wall_limit = player_keeper.transform.position.x + ((float)BLOCK_NUM_IN_SCREEN_WIDTH / 2.0f);

		if(this.bottom_wall_block.last_pos.x < generate_new_wall_limit){
			CreateWallBlock (ref bottom_wall_block);
			CreateWallBlock (ref up_wall_block);
		}
	}
		
	#endregion


	#region public method

	// ブロックが生成されてからのelapsed_timeが閾値より大きければブロック作成を許す
	public bool isBlockCreated(float elapsed_time){

		// ブロックが出てくる時間間隔 しきい値で指定する
		float block_limit = this.level_ctrl.getPlayerInterval (this.score_ctrl.Return ());;

		if (elapsed_time > block_limit) {
			this.CreateNotWallBlock ();
			return true;
		}
		return false;
	}

	// 渡されたオブジェクトがステージ外へ出ているか 左->上->下の順に調べる
	/* ブロックは左にのみ外れるため左の閾値から調べてはやく返す */
	public bool isOutOfScreen(GameObject game_obj){

		float limit_left = player_keeper.transform.position.x - (((float)BLOCK_NUM_IN_SCREEN_WIDTH + 8) / 2.0f) + 2.0f;
		if(game_obj.transform.position.x < limit_left){
			return true;
		}

		float limit_up = player_keeper.transform.position.y + (float)BLOCK_NUM_IN_SCREEN_HEIGHT / 2.0f + 3.0f;
		if(game_obj.transform.position.y > limit_up){
			return true;
		}

		float limit_bottom = player_keeper.transform.position.y - (float)BLOCK_NUM_IN_SCREEN_HEIGHT / 2.0f - 3.0f;
		if(game_obj.transform.position.y < limit_bottom){
			return true;
		}

		return false;
	}

	#endregion


	#region private method

	// マップ上下の壁を作る
	private void CreateWallBlock(ref WallBlock wall_block){
		Vector2 _position;

		if(wall_block.isFirstBlock) {
			// ブロックの位置を画面の中心 -> 画面左端へ
			_position = player_keeper.transform.position;
			_position.x -= BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN_WIDTH / 2.0f);

			if(wall_block.isUpWall){
				_position.y =  ((float)BLOCK_NUM_IN_SCREEN_HEIGHT - 1.0f) / 2.0f;
			} else {
				_position.y = -((float)BLOCK_NUM_IN_SCREEN_HEIGHT - 1.0f) / 2.0f;
			}

		} else {
			// 今回作るブロックを前回作ったブロックと同じ位置にする
			_position = wall_block.last_pos;
		}

		//ブロックの位置を1ブロック分、右へ移動
		_position.x += BLOCK_WIDTH;

		// ブロック作成指示
		block_creator.CreateBlock (_position, METAL, true);

		// last_blockを更新
		wall_block.last_pos = _position;
		wall_block.isFirstBlock = false;
	}

	// 壁とならないブロックの位置を決定  /* もう少しいい名前あるかな */
	private void CreateNotWallBlock(){
		// これから作るブロックの位置
		Vector2 next_block_position;

		// ブロックの位置をとりあえずプレイヤーの下にする
		next_block_position = player_keeper.transform.position;

		next_block_position.x += BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN_WIDTH / 2.0f);
		next_block_position.y = Random.Range (-(float)BLOCK_NUM_IN_SCREEN_HEIGHT/2.0f, (float)BLOCK_NUM_IN_SCREEN_HEIGHT/2.0f);

		// ランダムな種類でブロックを生成
		switch(Random.Range(1,5) % 4){
		case 0:
		case 1:
			block_creator.CreateBlock (next_block_position, RED, false);
			break;
		case 2:
			block_creator.CreateBlock (next_block_position, BLUE, false);
			break;
		case 3:
			block_creator.CreateBlock (next_block_position, NEEDLE, false);
			break;
		default:
			break;
		}
	}

	#endregion

}
