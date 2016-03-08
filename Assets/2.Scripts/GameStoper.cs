using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStoper : MonoBehaviour {

	private bool isGameClear;
	public Text finish;

	// Use this for initialization
	void Start () {
		isGameClear = false;
		finish.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if(GameMgr.game_mode == "TimeAttack"){
			if(!isGameClear){
				// すべてのブロックを破壊したら
				if(GameMgr.left_block < 1){
					SendGameOver ();
					isGameClear = true;
					GameMgr.isRetire = false;	// ゲームクリア
				}
			}
		} 
	}

	// ゲームオーバーを伝える
	public void SendGameOver(){
		if(GameMgr.game_mode == "TimeAttack"){
			//終了の合図を送る
			BroadcastMessage ("GameClear");
			GameObject.FindWithTag ("PlayerA").SendMessage ("GameClear");
			GameObject.FindWithTag ("Root").SendMessage ("GameClear");
			// ゲームリタイアかどうか
			StartCoroutine ("NextScene", "scTimeScore");
		} 
		else {
			StartCoroutine ("NextScene", "scScrollScore");
		}
	}


	IEnumerator NextScene(string scene){
		finish.text = "FINISH!!";
		Time.timeScale = 0.3f;	// スローモーションにする
		yield return new WaitForSeconds (1.0f);
		Time.timeScale = 1.0f;
//		Application.LoadLevel ("scTimeScore");
		Application.LoadLevel (scene);
	}
		
}
