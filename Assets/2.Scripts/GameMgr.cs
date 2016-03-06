using UnityEngine;
using System.Collections;

public class GameMgr : MonoBehaviour {
	static public string game_mode;
	static public int stage_num;
	static public int left_block;
	static public float time_score;
	//public static int stage_num;
	
	//
	static public void initialize(){
		game_mode = "TimeAttack";
		stage_num = 1;
		left_block = 0;
	}

	// タイトル画面のゲームモード選択で文字列変更
	static public void GameMode(string mode){
		game_mode = mode;
	}
}
