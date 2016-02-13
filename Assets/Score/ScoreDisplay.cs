using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	// スコア表示
	public Text scoreText;
	// ハイスコアを表示するGUIText
	public Text highcoreText;

	private int high_score;	// ハイスコア


	// PlayerPrefsで保存するためのキー
	private string highScoreKey = "highScore";
	private string thistimeScoreKey = "thistimeScore";

	// Use this for initialization
	void Start () {
		Initialize ();

		// スコアがハイスコアより大きければ
		if (high_score < SetValue.total_score) {
			high_score = SetValue.total_score;
			Save();
		}
		// スコア・ハイスコアを表示する
		highcoreText.text = "HIGH : " + high_score;
		scoreText.text = "SCORE : " + SetValue.total_score;
	}

	// 初期化
	private void Initialize(){ 

		// ハイスコアを取得する。保存されてなければ0を取得する。
		high_score = PlayerPrefs.GetInt (highScoreKey, 0);
	}

	// ハイスコアの保存
	public void Save ()
	{
		// ハイスコアを保存する
		//PlayerPrefs.SetInt (highScoreKey, 0);
		PlayerPrefs.SetInt (highScoreKey, high_score);
		PlayerPrefs.SetInt (thistimeScoreKey, SetValue.total_score);
		PlayerPrefs.Save ();

		// ゲーム開始前の状態に戻す
		Initialize ();
	}
}