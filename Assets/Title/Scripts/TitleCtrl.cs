using UnityEngine;
using System.Collections;

public class TitleCtrl : MonoBehaviour {

	public string scene, game_mode;

	// スクロールモードでゲーム開始
	public void Scroll(){
		scene = "scPlay1";
		game_mode = "Scroll";
		GameMgr.GameMode (game_mode);
		//Application.LoadLevelAdditive (scene);
		Application.LoadLevel(scene);
	}

	// タイムアタックモードでゲーム開始
	public void TimeAttack(){
		game_mode = "TimeAttack";
		GameMgr.GameMode (game_mode);
		scene = "scStageSelect";
		Application.LoadLevel(scene);
	}
}
