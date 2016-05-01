using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStoper : MonoBehaviour {

	private bool isGameClear;
	private bool isExecuteSendGameOver;
	public Text finish;

	// Use this for initialization
	void Start () {
		isGameClear = false;
		isExecuteSendGameOver = false;
		finish.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if(GameMgr.game_mode == "TimeAttack"){
			if(!isGameClear){
				// すべてのブロックを破壊したら
				if(GameMgr.left_block < 1){
					isGameClear = true;
					SendGameOver ();
				}
			}
		} 
	}

	// ゲームオーバーを伝える
	public void SendGameOver(){
		// この関数は一回だけ機能するようにする
		if(!isExecuteSendGameOver){
			if(GameMgr.game_mode == "TimeAttack"){
				//終了の合図を送る
				BroadcastMessage ("GameClear");
				GameObject.FindWithTag ("PlayerA").SendMessage ("GameClear");
				GameObject.FindWithTag ("Root").SendMessage ("GameClear");
				// ゲームクリアの方なら
				if(isGameClear){
					GameMgr.isRetire = false;
				} else {
					GameMgr.isRetire = true;
				}
				StartCoroutine ("NextScene", "scTimeScore");
			} 
			else {
				StartCoroutine ("NextScene", "scScrollScore");
			}
			isExecuteSendGameOver = true;
		}

	}


	IEnumerator NextScene(string scene){
		finish.text = "FINISH!!";
		Time.timeScale = 0.3f;	// スローモーションにする
		yield return new WaitForSeconds (0.5f);
		Time.timeScale = 1.0f;
		SceneManager.LoadScene(scene);
	}
		
}
