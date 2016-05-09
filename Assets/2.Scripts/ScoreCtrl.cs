using UnityEngine;
using System.Collections;

public class ScoreCtrl : MonoBehaviour {

	#region public method

	// 引数のスコアを加点する
	public void Add(int score){
		GameMgr.total_score += score;
	}

	// スコアを返す
	public int Return(){
		return GameMgr.total_score;
	}

	#endregion
}
