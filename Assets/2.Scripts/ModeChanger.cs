using UnityEngine;
using System.Collections;


public class ModeChanger : MonoBehaviour {

	#region private property

	[SerializeField]
	private GameObject[] player;
	private GameObject player_before_change, changed_player;

	// プレイヤーのモードを表す列挙体
	/* モードの種類を増やす可能性があるので列挙体にしておく */
	private enum MODE{
		SNAKE = 0,
		CHASER = 1,
	};
	private MODE player_mode = MODE.SNAKE;

	#endregion


	#region public method

	public void ModeChange(){

		player_before_change = GameObject.FindWithTag ("PlayerA");
		float i = player_before_change.transform.position.x;
		float j = player_before_change.transform.position.y;
		Destroy (player_before_change);

		/* モードの種類を増やした場合、処理を追加する */
		switch(player_mode){
		case MODE.SNAKE:
			player_mode = MODE.CHASER;
			break;

		case MODE.CHASER:
			player_mode = MODE.SNAKE;
			break;

		default:
			break;
			
		}

		changed_player = Instantiate (player [(int)player_mode], new Vector2 ((float)i, (float)j), Quaternion.Euler (new Vector2 (0f, 0f))) 
			as GameObject;
		changed_player.GetComponent<Player2DCtrl> ().enabled = true;

	}

	#endregion
}
