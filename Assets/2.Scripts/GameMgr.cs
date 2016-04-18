using UnityEngine;
using System.Collections;

public class GameMgr {
	static public string game_mode;
	static public int stage_num;
	static public int scroll_stage_num;
	static public int left_block;
	static public float time_score;
	static public bool isRetire;	// ゲームリタイアかどうか
	static public int total_score;
	static public int count_for_ads;	// Adsをどれくらいの頻度で表示するか

	static public int[] rank = new int[30];
	
	//
	static public void initialize(){
		game_mode = "TimeAttack";
		stage_num = 1;
		scroll_stage_num = 1;
		left_block = 0;
		isRetire = false;
		total_score = 0;
		count_for_ads = 0;

		for(int i=0; i<30; i++){
			rank [i] = 1;
		}
	}
}
