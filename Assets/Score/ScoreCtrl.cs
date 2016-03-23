using UnityEngine;
using System.Collections;

public class ScoreCtrl : MonoBehaviour {

	private int result;

	// 引数のスコアを加点する
	public void Add(int score){
		GameMgr.total_score += score;
	}

	// スコアを返す
	public int Return(){
		result = GameMgr.total_score;
		return result;
	}
}
