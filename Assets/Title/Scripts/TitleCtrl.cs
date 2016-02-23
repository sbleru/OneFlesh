using UnityEngine;
using System.Collections;

public class TitleCtrl : MonoBehaviour {

	public string scene, game_mode;

	// Use this for initialization
	void Start () {
	
	}


	public void Scroll(){
		scene = "scPlay1";
		game_mode = "Scroll";
		GameMgr.GameMode (game_mode);
		//Application.LoadLevelAdditive (scene);
		Application.LoadLevel(scene);
	}

	// タイムアタックモード
	public void TimeAttack(){
		game_mode = "TimeAttack";
		GameMgr.GameMode (game_mode);
		scene = "scAttack";
		Application.LoadLevel(scene);
	}
}
