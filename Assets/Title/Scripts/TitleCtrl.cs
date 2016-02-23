using UnityEngine;
using System.Collections;

public class TitleCtrl : MonoBehaviour {

	public string scene;

	// Use this for initialization
	void Start () {
	
	}


	public void Scroll(){
		scene = "scPlay1";
		//Application.LoadLevelAdditive (scene);
		Application.LoadLevel(scene);
	}

	// タイムアタックモード
	public void TimeAttack(){
		scene = "scAttack";
		Application.LoadLevel(scene);
	}
}
