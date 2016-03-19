using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeScoreCtrl : MonoBehaviour {
	public static int STAGE_NUM = 6;	// 全ステージ数
	public static int RANK_NUM = 3;		// ランクの段階数

	public Text scoreText;	// スコア表示
	public Text highcoreText;	// ハイスコアを表示するGUIText
	private float high_score;	// ハイスコア

	// PlayerPrefsで保存するためのキー
	// ステージごとに別に保存
	private string highScoreKey = "highScore" + GameMgr.stage_num;
	private string thistimeScoreKey = "thistimeScore";

	#region value related rank
	private TextAsset rank_asset;  // ステージテキストを取り込む
	string rank_txt;
	public float[,] rank_data = new float[STAGE_NUM, RANK_NUM];
	private string rankKey = "rank" + GameMgr.stage_num;

	public GameObject[] rank_stamp_thistime;
	public GameObject[] rank_stamp;
	public Text to_next_rank;
	public GameObject retire_txt;	// リタイアテキスト
	#endregion

	// Use this for initialization
	void Start () {
		Initialize ();

		// ハイスコアからステージごとのランクを決定するデータを得る
		#region decision rank
		rank_asset = Resources.Load ("rank_data1") as TextAsset;
		rank_txt = rank_asset.text;
		get_rank_data();
		#endregion

		// ゲームリタイアかどうか
		if(!GameMgr.isRetire){
			// タイムがはやくなれば
			if (high_score > GameMgr.time_score) {
				high_score = GameMgr.time_score;
				Save();
			}
			// スコア・ハイスコアを表示する
			highcoreText.text = "HIGHSCORE : " + high_score;
			scoreText.text = "SCORE : " + GameMgr.time_score;

			// ハイスコアからランクを登録する
			rank_register();

			// 現スコアの評価を与える
			rank_thistime ();

		} else {
			// スコア・ハイスコアを表示する
			highcoreText.text = "HIGHSCORE : " + high_score;
			scoreText.text = "";
			retire_txt.SetActive (true);
		}

		// 次のランク解放のためのスコアを表示する
		next_rank ();
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


	private void get_rank_data(){

		string[] lines = rank_txt.Split ('\n');
		int i=0, j=0;
		bool isBreak = false;

		//lines内の各行に対して、順番に処理していくループ
		foreach(var line in lines){
			if(line == ""){ //行がなければ
				continue;  
			}

			//print (line);
			string[] words = line.Split ();

			//words内の各ワードに対して、順番に処理していくループ
			foreach(var word in words){
				if(word.StartsWith("#")){	// ワードの先頭文字が#なら
					isBreak = true;
					break;
				}
				if(word == ""){
					continue;
				}
				rank_data [i, j] = float.Parse (word);
				j++;
				if (j > RANK_NUM-1){
					break;
				}
			}
			if(!isBreak){
				j = 0;
				i++;
			}
			isBreak = false;

			if (i > STAGE_NUM-1){
				break;
			}
		}
	}

	//
	private void rank_register(){

		for(int i=0; i<RANK_NUM; i++){

			if(high_score < rank_data[GameMgr.stage_num-1, i]){
				
				PlayerPrefs.SetInt (rankKey, i+1);
				PlayerPrefs.Save ();
			}
		}
	}

	//
	private void rank_thistime(){
		for(int i=RANK_NUM-1; i>=0; i--){

			if(GameMgr.time_score < rank_data[GameMgr.stage_num-1, i]){
				rank_stamp_thistime [i].SetActive (true);
				break;
			}
		}
	}

	//
	private void next_rank(){
		for(int i=RANK_NUM-1; i>0; i--){

			if(high_score < rank_data[GameMgr.stage_num-1, i]){
				if(i==RANK_NUM-1){
					to_next_rank.text = "Max Rank";
				} else {
					to_next_rank.text = "Next : " + rank_data [GameMgr.stage_num - 1, i+1];
				}
				rank_stamp [i].SetActive (true);
				break;
			}
		}
	}
}