using UnityEngine;
using System.Collections;

public class GameStoper : MonoBehaviour {

	private bool isGameClear;

	// Use this for initialization
	void Start () {
		isGameClear = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isGameClear){
			// すべてのブロックを破壊したら
			if(GameMgr.left_block < 1){
				//終了の合図を送る
				BroadcastMessage ("GameClear");
				GameObject.FindWithTag ("PlayerA").SendMessage ("GameClear");
				GameObject.FindWithTag ("Root").SendMessage ("GameClear");

				Time.timeScale = 0.3f;	// スローモーションにする
				isGameClear = true;
				StartCoroutine ("NextScene");
			}
		}
	}

	IEnumerator NextScene(){
		yield return new WaitForSeconds (1.0f);
		Time.timeScale = 1.0f;
		Application.LoadLevel ("scTimeScore");
	}
}
