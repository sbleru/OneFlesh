using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageSelect : MonoBehaviour {

	private string next_scene;


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
}
