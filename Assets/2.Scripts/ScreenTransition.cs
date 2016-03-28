using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenTransition : MonoBehaviour {

	private int stage_num;

	//
	public void ToTitle(){
		FadeManager.Instance.LoadLevel ("scTitle0", 0.1f);
	}

	public void ToTimeAttack(){
		GameMgr.stage_num = int.Parse(this.GetComponent<Text> ().text);
		FadeManager.Instance.LoadLevel ("scAttack", 0.1f);
	}

	public void ToScroll(){
		GameMgr.scroll_stage_num = int.Parse (this.GetComponent<Text> ().text);
		FadeManager.Instance.LoadLevel ("scScroll", 0.1f);
	}

	public void BackToStageSelect(){
		Time.timeScale = 1.0f;
		if(GameMgr.game_mode == "TimeAttack"){
			FadeManager.Instance.LoadLevel ("scStageSelect", 0.1f);

		} else if(GameMgr.game_mode == "Scroll"){
			FadeManager.Instance.LoadLevel ("scScrollStageSelect", 0.1f);
		}
	}

	public void Redo(){
		if(GameMgr.game_mode == "TimeAttack"){
			FadeManager.Instance.LoadLevel ("scAttack", 0.1f);

		} else if(GameMgr.game_mode == "Scroll"){
			FadeManager.Instance.LoadLevel ("scScroll", 0.1f);
		}
	}
		
}
