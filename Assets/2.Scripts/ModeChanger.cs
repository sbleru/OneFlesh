using UnityEngine;
using System.Collections;


public class ModeChanger : MonoBehaviour {

	[SerializeField]
	private GameObject[] player2d;
	private GameObject tempObj;

	// プレイヤーのモードを表す列挙体
	public enum MODE{
		SNAKE = 0,
		CHASER = 1,
	};
	private MODE _mode = MODE.SNAKE;


	public void ModeChange(){
		tempObj = GameObject.FindWithTag ("PlayerA");

		float i = tempObj.transform.position.x;
		float j = tempObj.transform.position.y;
		Destroy (tempObj);

		switch(_mode){
		case MODE.SNAKE:
			tempObj = Instantiate (player2d [1], new Vector2 ((float)i, (float)j), Quaternion.Euler (new Vector2 (0f, 0f))) 
				as GameObject;
			tempObj.GetComponent<Player2DCtrl> ().enabled = true;
			_mode = MODE.CHASER;
			break;

		case MODE.CHASER:
			tempObj = Instantiate (player2d [0], new Vector2 ((float)i, (float)j), Quaternion.Euler (new Vector2 (0f, 0f))) 
				as GameObject;
			tempObj.GetComponent<Player2DCtrl> ().enabled = true;
			_mode = MODE.SNAKE;
			break;

		default:
			break;
			
		}

	}
}
