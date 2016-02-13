using UnityEngine;
using System.Collections;

public class TitleCtrl : MonoBehaviour {

	public string scene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play(){
		scene = "scPlay1";
		Application.LoadLevel (scene);
	}
}
