using UnityEngine;
using System.Collections;

public class TitleCtrl : MonoBehaviour {

	public string scene, game_mode;
	public GameObject player_a, player_b;

	void Awake(){
		GameMgr.initialize ();
	}

	void Start(){
		
	}

	// スクロールモードでゲーム開始
	public void Scroll(){
		iTween.MoveTo( player_a.gameObject, new Vector3(1.0f,-1.0f,0.0f), 0.5f ); 
		StartCoroutine ("PlayScroll");
	}

	IEnumerator PlayScroll(){
		//scroll_button.SetActive (false);
		yield return new WaitForSeconds (1.0f);
		scene = "scScrollStageSelect";
		game_mode = "Scroll";
		GameMgr.GameMode (game_mode);
		Application.LoadLevel(scene);
	}

	// タイムアタックモードでゲーム開始
	public void TimeAttack(){
		iTween.MoveTo( player_b.gameObject, new Vector3(-1.0f,-1.0f,0.0f), 0.5f ); 
		StartCoroutine ("PlayTimeAttack");
	}

	IEnumerator PlayTimeAttack(){
		yield return new WaitForSeconds (1.0f);
		scene = "scStageSelect";
		game_mode = "TimeAttack";
		GameMgr.GameMode (game_mode);
		Application.LoadLevel(scene);
	}
}
