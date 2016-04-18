using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using System.Collections;
using System.Collections.Generic;

public class ScrollScoreCtrl : MonoBehaviour {

	// スコア表示
	public Text scoreText;
	// ハイスコアを表示するGUIText
	public Text highcoreText;

	private int high_score;	// ハイスコア

	[SerializeField]
	UnityAdsController unity_ads_controller;


	// PlayerPrefsで保存するためのキー
	private string highScoreKey = "scrollhighScore" + GameMgr.scroll_stage_num;
	private string thistimeScoreKey = "thistimeScore";

	// Use this for initialization
	void Start () {
		Initialize ();

		// スコアがハイスコアより大きければ
		if (high_score < GameMgr.total_score) {
			high_score = GameMgr.total_score;
			Save();
		}
		// スコア・ハイスコアを表示する
		highcoreText.text = "HIGHSCORE : " + high_score;
		scoreText.text = "SCORE : " + GameMgr.total_score;

		// CustomEvent を作る　クリア時
		Analytics.CustomEvent ("Clear", new Dictionary<string, object> {
			{ "scene ID", SceneManager.GetActiveScene().buildIndex },
			{"thistime score", GameMgr.total_score},
			{ "high score", high_score },
		});

		// UnityAds表示
		unity_ads_controller.WaitAndShowUnityAds (1.0f);
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
		PlayerPrefs.SetInt (thistimeScoreKey, GameMgr.total_score);
		PlayerPrefs.Save ();

		// ゲーム開始前の状態に戻す
		Initialize ();
	}
}