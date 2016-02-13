using UnityEngine;
using System.Collections;

public class ScoreCtrl : MonoBehaviour {

	public int result;

	// 引数のスコアを加点する
	public void Add(int score){
		SetValue.total_score += score;
	}

	// スコアを返す
	public int Return(){
		result = SetValue.total_score;
		return result;
	}
}
