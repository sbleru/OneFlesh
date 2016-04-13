using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStarter : MonoBehaviour {

	public Text starter;
	private float timer;

	void Awake(){
		GameMgr.left_block = 0;
		GameMgr.total_score = 0;
	}

	// Use this for initialization
	void Start () {
		timer = 3.2f; //3秒 + α
	}

	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		//表示用のカウント
		//タイマーの時間を切り上げして整数にする
		string text = Mathf.CeilToInt (timer).ToString ();
		//タイマーの少数部分をアルファ値とすることで文字をフェードアウト
		starter.color = new Color (1, 1, 1, timer - Mathf.FloorToInt (timer));
		starter.text = text;

		if(timer < 0.0f){
			// 開始メッセージをブロードキャストして終了する
			BroadcastMessage ("StartGame");
			GameObject.FindWithTag ("Root").SendMessage ("StartGame");
			GameObject.FindWithTag ("PlayerA").SendMessage ("StartGame");

			starter.text = "GO!!";
			if(timer < -1.0f){
				enabled = false;
				starter.enabled = false;
			}
		}
	}

	// 空呼び出し
	private void StartGame(){
		// 
	}

}