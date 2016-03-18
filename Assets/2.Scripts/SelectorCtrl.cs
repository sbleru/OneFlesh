using UnityEngine;
using System.Collections;

public class SelectorCtrl : MonoBehaviour {

	private float currentXpos, currentYpos, startYpos;
	private bool touchStart, swipestart;
	// エリアポイント
	public GameObject[] area_point;
	public GameObject selector;
	private int current_pos;
	private float interval_area = 300f;	// ステージ選択エリア間隔
	Vector3[] pointa = new Vector3[5];
	float temp_area;	// 移動量を一時保存
	Vector3 start_pos;	// カメラの初期位置

	// Use this for initialization
	void Start () {
		current_pos = 0;
		start_pos = this.gameObject.transform.position;

		for(int i=0; i<5; i++){
			pointa [i] = area_point [i].transform.position;
		}
	}


	void Update(){
		// 仮想操作パッド スワイプ処理
		for (int i = 0; i < Input.touchCount; i++) {
			// 
			if (Input.GetTouch (i).position.x > 0.0f) {
				// 指があった場合、座標を格納
				currentYpos = Input.GetTouch (i).position.y;
				if (!touchStart) {
					// タッチした瞬間の座標を保存
					startYpos = currentYpos;
					touchStart = true;
					swipestart = true;
				}
			}
		}

		if(Input.touchCount == 0){
			// 画面に指が触れていない場合
			currentYpos = 0.0f;
			startYpos = 0.0f;
			touchStart = false;
			swipestart = false;
		}

		// 移動地計算 Y軸
		if(swipestart){
			if((startYpos - currentYpos) < (Screen.height * -0.08f)){
				LowerArea ();
			} else if ((startYpos - currentYpos) > (Screen.height * 0.08f)){
				HigherArea ();
			} 
		}
		swipestart = false;	// スワイプ一回で一つエリアを移動させるための処理

	}


	public void LowerArea(){
		// 下限であれば
		if(current_pos < 4){
			current_pos++;

			// 移動量計算
			float temp_pos_x = this.gameObject.transform.position.x;
			temp_area = (start_pos.y - interval_area * current_pos);
			// 移動アニメーション
			iTween.MoveTo (this.gameObject, new Vector3(temp_pos_x, temp_area, -10f), 1.0f);
//			iTween.MoveTo (selector, area[current_pos], 1.0f);
			iTween.MoveTo (selector, pointa[current_pos], 1.0f);
		}

	}

	public void HigherArea(){
		// 上限であれば
		if(current_pos > 0){
			current_pos--;

			// 移動量計算
			float temp_pos_x = this.gameObject.transform.position.x;
			temp_area = (start_pos.y - interval_area * current_pos);
			// 移動アニメーション
			iTween.MoveTo (this.gameObject, new Vector3(temp_pos_x, temp_area, -10f), 1.0f);
			iTween.MoveTo (selector, pointa[current_pos], 1.0f);
		}
	}

	// 各ポインタへ移動する関数　もっといい方法ありそう、、、
	public void Pointa0(){
		current_pos = 0;
		float temp_pos_x = this.gameObject.transform.position.x;
		temp_area = (start_pos.y - interval_area * current_pos);
		// 移動アニメーション
		iTween.MoveTo (this.gameObject, new Vector3(temp_pos_x, temp_area, -10f), 1.0f);
		iTween.MoveTo (selector, pointa[current_pos], 1.0f);
	}

	public void Pointa1(){
		current_pos = 1;
		float temp_pos_x = this.gameObject.transform.position.x;
		temp_area = (start_pos.y - interval_area * current_pos);
		// 移動アニメーション
		iTween.MoveTo (this.gameObject, new Vector3(temp_pos_x, temp_area, -10f), 1.0f);
		iTween.MoveTo (selector, pointa[current_pos], 1.0f);
	}

	public void Pointa2(){
		current_pos = 2;
		float temp_pos_x = this.gameObject.transform.position.x;
		temp_area = (start_pos.y - interval_area * current_pos);
		// 移動アニメーション
		iTween.MoveTo (this.gameObject, new Vector3(temp_pos_x, temp_area, -10f), 1.0f);
		iTween.MoveTo (selector, pointa[current_pos], 1.0f);
	}

	public void Pointa3(){
		current_pos = 3;
		float temp_pos_x = this.gameObject.transform.position.x;
		temp_area = (start_pos.y - interval_area * current_pos);
		// 移動アニメーション
		iTween.MoveTo (this.gameObject, new Vector3(temp_pos_x, temp_area, -10f), 1.0f);
		iTween.MoveTo (selector, pointa[current_pos], 1.0f);
	}

	public void Pointa4(){
		current_pos = 4;
		float temp_pos_x = this.gameObject.transform.position.x;
		temp_area = (start_pos.y - interval_area * current_pos);
		// 移動アニメーション
		iTween.MoveTo (this.gameObject, new Vector3(temp_pos_x, temp_area, -10f), 1.0f);
		iTween.MoveTo (selector, pointa[current_pos], 1.0f);
	}
}
