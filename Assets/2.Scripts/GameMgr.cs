using UnityEngine;
using System.Collections;

public class GameMgr : MonoBehaviour {
	static public string game_mode;
	static public int stage_num;
	static public int scroll_stage_num;
	static public int left_block;
	static public float time_score;
	static public bool isRetire;	// ゲームリタイアかどうか
	static public int total_score;
	
	//
	static public void initialize(){
		game_mode = "TimeAttack";
		stage_num = 1;
		scroll_stage_num = 1;
		left_block = 0;
		isRetire = false;
		total_score = 0;
	}

	// タイトル画面のゲームモード選択で文字列変更
//	static public void GameMode(string mode){
//		game_mode = mode;
//	}
}

//public class AudioManager : MonoBehaviour {
//	//static: 新しくインスタンス化しても変数の中身を保持する
//	public static AudioManager instance;
//	public AudioClip audioClip;
//	AudioSource audioSource;
//
//	void Awake ()
//	{
//		//AudioManagerインスタンスが存在したら
//		if (instance != null) {
//			//今回インスタンス化したAudioManagerを破棄
//			Destroy(this.gameObject);
//			//AudioManagerインスタンスがなかったら
//		} else if (instance == null){
//			//このAudioManagerをインスタンスとする
//			instance = this;
//		}
//		//シーンを跨いでもAudioManagerインスタンスを破棄しない
//		DontDestroyOnLoad (this.gameObject);
//	}
//	//指定したBGMを再生する
//	void Start () {
//		audioSource = GetComponent<AudioSource>();
//		audioSource.clip = audioClip;
//		audioSource.Play();
//	}
//}