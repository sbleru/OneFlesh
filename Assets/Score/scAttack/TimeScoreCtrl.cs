using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeScoreCtrl : MonoBehaviour {

	// スコア表示
	public Text scoreText;
	// ハイスコアを表示するGUIText
	public Text highcoreText;

	private float high_score;	// ハイスコア


	// PlayerPrefsで保存するためのキー
	// ステージごとに別に保存
	private string highScoreKey = "highScore" + GameMgr.stage_num;
	private string thistimeScoreKey = "thistimeScore";

	// Use this for initialization
	void Start () {
		Initialize ();

		// ゲームリタイアかどうか
		if(!GameMgr.isRetire){
			// タイムがはやくなれば
			if (high_score > GameMgr.time_score) {
				high_score = GameMgr.time_score;
				Save();
			}
			// スコア・ハイスコアを表示する
			highcoreText.text = "HIGH : " + high_score;
			scoreText.text = "SCORE : " + GameMgr.time_score;

		} else {
			// スコア・ハイスコアを表示する
			highcoreText.text = "HIGH : " + high_score;
			scoreText.text = "RETIRE";
		}

		//Save ();
	}

	// 初期化
	private void Initialize(){ 
		// ハイスコアを取得する。保存されてなければ60を取得する。
		high_score = PlayerPrefs.GetFloat (highScoreKey, 60f);
	}

	// ハイスコアの保存
	public void Save ()
	{
		// ハイスコアを保存する
		//PlayerPrefs.SetInt (highScoreKey, 0);
		PlayerPrefs.SetFloat (highScoreKey, high_score);
		//PlayerPrefs.SetFloat (highScoreKey, 60f);
		PlayerPrefs.SetFloat (thistimeScoreKey, GameMgr.time_score);
		PlayerPrefs.Save ();

		// ゲーム開始前の状態に戻す
		Initialize ();
	}
}