using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timekeeper : MonoBehaviour {

	public Text time;
	private float elapsedTime; // ゲームの長さ
	string text;

	void Start(){
		elapsedTime = 0.0f;
	}


	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		//表示用のカウント
		//小数第2位まで
		//タイマーの時間を切り上げして整数にする
		elapsedTime = Mathf.CeilToInt (elapsedTime * 100);
		elapsedTime /= 100;
		text = elapsedTime.ToString ();
		time.color = new Color (1, 1, 1, elapsedTime - Mathf.FloorToInt (elapsedTime));
		// 残り時間を更新
		time.text = text;

	}

	//
	void StartGame(){
		enabled = true;
	}

	void GameClear(){
		GameMgr.time_score = elapsedTime;
		enabled = false;
	}
}
