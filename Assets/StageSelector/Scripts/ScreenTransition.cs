using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenTransition : MonoBehaviour {

	private int stage_num;

	//
	public void ToTitle(){
		Application.LoadLevel ("scTitle0");
	}

	public void ToTimeAttack(){
		GameMgr.stage_num = int.Parse(this.GetComponent<Text> ().text);
		Application.LoadLevel ("scAttack");
	}

	public void ToScroll(){
		GameMgr.scroll_stage_num = int.Parse (this.GetComponent<Text> ().text);
		Application.LoadLevel ("scScroll");
	}

	public void BackToStageSelect(){
		Time.timeScale = 1.0f;
		if(GameMgr.game_mode == "TimeAttack"){
			Application.LoadLevel ("scStageSelect");

		} else if(GameMgr.game_mode == "Scroll"){
			Application.LoadLevel ("scScrollStageSelect");
		}
	}

	public void Redo(){
		if(GameMgr.game_mode == "TimeAttack"){
			Application.LoadLevel ("scAttack");

		} else if(GameMgr.game_mode == "Scroll"){
			Application.LoadLevel ("scScroll");
		}
	}
		
}
