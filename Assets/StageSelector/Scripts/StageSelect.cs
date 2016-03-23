using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageSelect : MonoBehaviour {

	private int stage_num;

	public GameObject[] rank_stamp;


	void Awake(){
		GameMgr.left_block = 0;
		GameMgr.total_score = 0;
	}

	void Start(){
		this.stage_num = int.Parse (this.GetComponent<Text> ().text);
		
		// スタンプを選択する
		#region select stamp
		switch(PlayerPrefs.GetInt ("rank"+this.stage_num, 0)){
//		switch(PlayerPrefs.GetInt ("rank"+this.stage_num, -1)){

		case 0:	// ランクなし
			// 何もしない
			break;
		case 1: // Cランク
			rank_stamp[0].SetActive(true);
			break;
		case 2: // Bランク
			rank_stamp[1].SetActive(true);
			break;
		case 3:	// Aランク
			rank_stamp[2].SetActive(true);
			break;
		case 4: // Sランク
			rank_stamp[3].SetActive(true);
			break;
		default:
			break;
		}
		#endregion

	}

	//
	public void ToTitle(){
		Application.LoadLevel ("scTitle");
	}

	public void ToTimeAttack(){
		GameMgr.stage_num = this.stage_num;
//		GameMgr.stage_num = int.Parse(this.GetComponent<Text> ().text);
		Application.LoadLevel ("scAttack");
	}

	public void ToScroll(){
		GameMgr.scroll_stage_num = this.stage_num;
//		GameMgr.scroll_stage_num = int.Parse (this.GetComponent<Text> ().text);
		Application.LoadLevel ("scScroll");
	}
		
}
