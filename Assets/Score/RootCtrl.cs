using UnityEngine;
using System.Collections;

public class RootCtrl : MonoBehaviour {

	public void NextScene(){
		Application.LoadLevel ("scTitle");
	}

	public void ToStageSelect(){
		Application.LoadLevel ("scStageSelect");
	}
}
