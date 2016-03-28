using UnityEngine;
using System.Collections;

public class TitleCtrl : MonoBehaviour {

	[SerializeField]
	private GameObject player_a, player_b;

	void Awake(){
		GameMgr.initialize ();
	}

	// スクロールモードでゲーム開始
	public void Scroll(){
		iTween.MoveTo( player_a.gameObject, new Vector3(1.0f,-1.0f,0.0f), 0.5f ); 
		GameMgr.game_mode = "Scroll";
		FadeManager.Instance.LoadLevel ("scScrollStageSelect", 0.5f);
	}


	// タイムアタックモードでゲーム開始
	public void TimeAttack(){
		iTween.MoveTo( player_b.gameObject, new Vector3(-1.0f,-1.0f,0.0f), 0.5f ); 
		GameMgr.game_mode = "TimeAttack";
		FadeManager.Instance.LoadLevel ("scStageSelect", 0.5f);
	}
		
}
