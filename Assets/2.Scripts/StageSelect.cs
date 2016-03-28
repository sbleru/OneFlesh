using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// タイムアタックのステージセレクトで呼ばれる
// 初期化、各種ステージスタンプ、シーン遷移
public class StageSelect : MonoBehaviour {

	private int stage_num;

	[SerializeField]
	private GameObject[] rank_stamp;

	void Awake(){
		GameMgr.left_block = 0;
		GameMgr.total_score = 0;
	}

	void Start(){
		this.stage_num = int.Parse (this.GetComponent<Text> ().text);
		
		// スタンプを選択する
		#region select stamp
		switch(PlayerPrefs.GetInt ("rank"+this.stage_num, 0)){

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
		FadeManager.Instance.LoadLevel ("scTitle", 0.1f);
	}

	public void ToTimeAttack(){
		GameMgr.stage_num = this.stage_num;
		FadeManager.Instance.LoadLevel ("scAttack", 0.1f);
	}

	public void ToScroll(){
		GameMgr.scroll_stage_num = this.stage_num;
		FadeManager.Instance.LoadLevel ("scScroll", 0.1f);
	}
		
}
