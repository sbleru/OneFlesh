using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageSelect2 : MonoBehaviour {

	public string next_scene;
	private int stage_num;


	void Awake(){
		GameMgr.left_block = 0;
		GameMgr.total_score = 0;
	}

	//
	public void ToTitle(){
		Application.LoadLevel ("scTitle");
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
		Application.LoadLevel (next_scene);
	}
		
}
