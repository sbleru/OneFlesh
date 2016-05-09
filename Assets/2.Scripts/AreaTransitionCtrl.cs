using UnityEngine;
using System.Collections;

// ステージセレクトのエリア遷移各種処理 /* 1エリア6ステージとする */
// カメラにアタッチされている
public class AreaTransitionCtrl : MonoBehaviour {

	#region const
	
	private static readonly int AREA_NUM = 3;	// エリア数
	private const float AREA_INTERVAL = 300F;	// ステージ選択エリア間隔

	#endregion


	#region private property

	private float currentYpos, startYpos;
	private bool touchStart, swipestart;
	private int current_area;	// カメラの現在エリア
	Vector3 start_pos;			// カメラの初期位置

	[SerializeField]
	private GameObject go_up, go_down;

	#endregion


	#region event

	// Use this for initialization
	void Start () {
		current_area = 0;
		go_up.SetActive (false);
		start_pos = this.gameObject.transform.position;
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
			swipestart = false;		// スワイプ一回で一つエリアを移動させるための処理
		}

		// 移動地計算 Y軸
		if(swipestart){
			if((startYpos - currentYpos) < (Screen.height * -0.08f)){
				GoLowerArea ();
				swipestart = false;	
			} else if ((startYpos - currentYpos) > (Screen.height * 0.08f)){
				GoHigherArea ();
				swipestart = false;
			} 
		}
	}

	#endregion


	#region public method

	/* ボタンを押したときにもエリア移動可能とするため、<current_area>は関数側で処理する */
	public void GoLowerArea(){
		// 下限であれば
		if(current_area++ < AREA_NUM - 1){

			// 移動するターゲットエリアを計算し、移動アニメーション
			/* 関数化するべきか */
			Vector3 target_area = this.gameObject.transform.position;
			target_area.y = start_pos.y - AREA_INTERVAL * current_area;
			iTween.MoveTo (this.gameObject, target_area, 1.0f);
		
			// 最下エリアの場合、下エリアへ移動するボタンを消す
			go_up.SetActive (true);
			if(current_area == AREA_NUM - 1){
				go_down.SetActive (false);
			}
		}

	}

	public void GoHigherArea(){
		// 上限であれば
		if(current_area-- > 0){

			Vector3 target_area = this.gameObject.transform.position;
			target_area.y = start_pos.y - AREA_INTERVAL * current_area;
			iTween.MoveTo (this.gameObject, target_area, 1.0f);

			// 最上エリアの場合、上エリアへ移動するボタンを消す
			go_down.SetActive (true);
			if(current_area == 0){
				go_up.SetActive (false);
			}
		}
	}

	#endregion
}
