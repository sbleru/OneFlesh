using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BlockCreator))]
public class StageCreator : MonoBehaviour {
	public static float BLOCK_WIDTH = 1.0f;
	public static float BLOCK_HEIGHT = 0.2f;
	public static int BLOCK_NUM_IN_SCREEN = 40;
	public static int SCREEN_HEIGHT = 20;

	GameObject Player;
	ScoreCtrl score_ctrl;
	PlayerCtrl player_ctrl;
	BlockCreator block_creator;

	// Use this for initialization
	void Start () {
		this.Player = GameObject.FindGameObjectWithTag ("Player");
		this.score_ctrl = this.gameObject.GetComponent<ScoreCtrl> ();
		this.player_ctrl = GameObject.FindGameObjectWithTag ("PlayerA").GetComponent<PlayerCtrl> ();
		this.block_creator = GameObject.FindGameObjectWithTag ("Root").GetComponent<BlockCreator> ();
		// 最初の敵を作成
		this.create_enemy ();
		create_stage ();
	}

	// ステージ作成
	private void create_stage(){
		
	}

	// エネミーの位置を決定
	public void create_enemy(){
		// これから作るエネミーの位置
		Vector3 next_enemy_position;
		// エネミーの位置をとりあえずプレイヤーの下にする
		next_enemy_position = Player.transform.position;
		// エネミーのX位置
		next_enemy_position.x += BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
		//next_enemy_position.x = Random.Range(next_enemy_position.x, next_enemy_position.x + INTERVAL/2);
		// エネミーのY位置
		next_enemy_position.y = Random.Range (-(float)SCREEN_HEIGHT, (float)SCREEN_HEIGHT);
		// エネミー作成指示
		block_creator.createBlock (next_enemy_position);
	}
		

	// プレイヤーがステージ外へ出ているか
	public bool isOut(GameObject player){
		bool ret = false;

		do {
			// 左のしきい値の計算
			float limit_left = this.transform.position.x - (((float)BLOCK_NUM_IN_SCREEN + 8) / 2.0f);
			// ブロックがしきい値をでていたら削除の指示
			if(player.transform.position.x < limit_left){
				ret = true;
				break;
			}
			// 上のしきい値の計算
			float limit_up = this.transform.position.y + (float)SCREEN_HEIGHT;
			if(player.transform.position.y > limit_up){
				ret = true;
				break;
			}
			// 上のしきい値の計算
			float limit_down = this.transform.position.y - (float)SCREEN_HEIGHT;
			if(player.transform.position.y < limit_down){
				ret = true;
				break;
			}
		} while(false);

		return ret;
	}
}
