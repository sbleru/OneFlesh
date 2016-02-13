using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICtrl : MonoBehaviour {

	private float currentXpos, currentYpos, startXpos, startYpos;
	private float currentXpos_b, currentYpos_b, startXpos_b, startYpos_b;
	// プレイヤーに渡す移動方向を決める要素
	public int movingXpos, movingYpos;
	public int movingXpos_b, movingYpos_b;
	private bool touchStart;

	public Text score;	//ゲームスコア

	// Use this for initialization
	void Start () {
		currentXpos = 0.0f;
		currentYpos = 0.0f;
		currentXpos_b = 0.0f;
		currentYpos_b = 0.0f;
		movingXpos = 0;
		movingYpos = 0;
		movingXpos_b = 0;
		movingYpos_b = 0;
		touchStart = false;

		score.text = "Score: " + 0;
	}

	void Update(){
		// スコア更新
		score.text = "Score: " + SetValue.total_score;
	}

	// 仮想操作パッド
	public void player_direction(){
		// 仮想操作パッド
		for(int i = 0; i < Input.touchCount; i++){
			// 画面の右側に指があるか判定
			if(Input.GetTouch(i).position.x > (Screen.width / 2.0f)){
				// 指があった場合、座標を格納
				currentXpos = Input.GetTouch(i).position.x;
				currentYpos = Input.GetTouch(i).position.y;
				if(!touchStart){
					// タッチした瞬間の座標を保存
					startXpos = currentXpos;
					startYpos = currentYpos;
					touchStart = true;
				}
			} else {
				currentXpos_b = Input.GetTouch(i).position.x;
				currentYpos_b = Input.GetTouch(i).position.y;
				if(!touchStart){
					// タッチした瞬間の座標を保存
					startXpos_b = currentXpos_b;
					startYpos_b = currentYpos_b;
					touchStart = true;
				}
			}
		}

		if(Input.touchCount == 0){
			// 画面に指が触れていない場合
			currentXpos = 0.0f;
			currentYpos = 0.0f;
			startXpos = 0.0f;
			startYpos = 0.0f;

			currentXpos_b = 0.0f;
			currentYpos_b = 0.0f;
			startXpos_b = 0.0f;
			startYpos_b = 0.0f;
			movingXpos = 0;
			movingYpos = 0;
			movingXpos_b = 0;
			movingYpos_b = 0;
			touchStart = false;
		}
		// 移動地計算 X軸
		if((startXpos - currentXpos) < (Screen.width * -0.05f)){
			movingXpos = 1;
		} else if((startXpos - currentXpos) > (Screen.width * 0.05f)){
			movingXpos = -1;
		} else {
			movingXpos = 0;
		}

		// 移動地計算 Y軸
		if((startYpos - currentYpos) < (Screen.height * -0.08f)){
			movingYpos = 1;
		} else if ((startYpos - currentYpos) > (Screen.height * 0.08f)){
			movingYpos = -1;
		} else {
			movingYpos = 0;
		}

		// プレイヤーB
		// 移動地計算 X軸
		if((startXpos_b - currentXpos_b) < (Screen.width * -0.05f)){
			movingXpos = 1;
		} else if((startXpos_b - currentXpos_b) > (Screen.width * 0.05f)){
			movingXpos_b = -1;
		} else {
			movingXpos_b = 0;
		}

		// 移動地計算 Y軸
		if((startYpos_b - currentYpos_b) < (Screen.height * -0.08f)){
			movingYpos_b = 1;
		} else if ((startYpos_b - currentYpos_b) > (Screen.height * 0.08f)){
			movingYpos_b = -1;
		} else {
			movingYpos_b = 0;
		}
	}
}
