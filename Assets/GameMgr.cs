using UnityEngine;
using System.Collections;

public class GameMgr : MonoBehaviour {
	static public string game_mode;

	// Use this for initialization
	void Start () {
		initialize ();
	}
	
	//
	private void initialize(){
		game_mode = "TimeAttack";
	}

	// タイトル画面のゲームモード選択で文字列変更
	static public void GameMode(string mode){
		game_mode = mode;
	}
}
