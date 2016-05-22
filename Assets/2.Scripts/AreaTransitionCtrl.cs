using UnityEngine;
using System.Collections;

// ステージセレクトのエリア遷移各種処理 /* 1エリア6ステージとする */
// カメラにアタッチされている
public class AreaTransitionCtrl : MonoBehaviour {

	#region const
	
	private static readonly int AREA_NUM = 3;	// エリア数
	private const float AREA_INTERVAL = 300F;	// ステージ選択エリア間隔
	private const float FLICK_FEEDBACK = 50f;	// フリック時の反応

	#endregion


	#region private property

	private float currentYpos, startYpos;
	private bool touchStart, swipeStart, isSwiped;
	private int current_area;	// カメラの現在エリア
	Vector3 start_pos;			// カメラの初期位置
	Vector3 camera_pos, temp_pos;
	bool isTransition;

	[SerializeField]
	private GameObject go_up, go_down;

	#endregion


	#region event

	// Use this for initialization
	void Start () {
		current_area = 0;
		go_up.SetActive (false);
		start_pos = this.gameObject.transform.position;

		isTransition = true;
		swipeStart = false;
	}


	void Update(){
		
		// 仮想操作パッド スワイプ処理
		for (int i = 0; i < Input.touchCount; i++) {
			// 
			if (Input.GetTouch (i).position.x > 0.0f) {

				// 指があった場合、座標を格納
				currentYpos = Input.GetTouch (i).position.y;
				if (!touchStart) {
					camera_pos = this.gameObject.transform.position;
					isTransition = false;

					// タッチした瞬間の座標を保存
					startYpos = currentYpos;
					touchStart = true;
					swipeStart = true;
					isSwiped = true;
				}
			}
		}

		if(Input.touchCount == 0){
			// 画面に指が触れていない場合
			currentYpos = 0.0f;
			startYpos = 0.0f;
			touchStart = false;
			swipeStart = false;
			isSwiped = false;		// スワイプ一回で一つエリアを移動させるための処理

			if(!isTransition){
				iTween.MoveTo (this.gameObject, camera_pos, 1.0f);
			}
		}

		// 移動地計算 Y軸
		if(isSwiped){
			if((startYpos - currentYpos) < (Screen.height * -0.08f)){

				// スワイプする前のフィードバックを返す処理
				if(swipeStart){
					swipeStart = false;
					Vector3 target_area = this.gameObject.transform.position;
					target_area.y = target_area.y - FLICK_FEEDBACK;
					iTween.MoveTo (this.gameObject, target_area, 1.0f);
				}
					
				// 下エリアへ移動
				if ((startYpos - currentYpos) < (Screen.height * -0.2f)) {
					isSwiped = false;
					GoLowerArea ();
				}

			} else if ((startYpos - currentYpos) > (Screen.height * 0.08f)){

				// スワイプする前のフィードバックを返す処理
				if(swipeStart){
					swipeStart = false;
					Vector3 target_area = this.gameObject.transform.position;
					target_area.y = target_area.y + FLICK_FEEDBACK;
					iTween.MoveTo (this.gameObject, target_area, 1.0f);
				}

				// 上エリアへ移動
				if ((startYpos - currentYpos) > (Screen.height * 0.2f)) {
					isSwiped = false;
					GoHigherArea ();
				}
			}
		}
	}

	#endregion


	#region public method

	/* ボタンを押したときにもエリア移動可能とするため、<current_area>は関数側で処理する */
	public void GoLowerArea(){
		// 下限であれば
		if(current_area < AREA_NUM - 1){
			current_area++;
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

			isTransition = true;
		}

	}

	public void GoHigherArea(){
		// 上限であれば
		if(current_area > 0){
			current_area--;

			Vector3 target_area = this.gameObject.transform.position;
			target_area.y = start_pos.y - AREA_INTERVAL * current_area;
			iTween.MoveTo (this.gameObject, target_area, 1.0f);

			// 最上エリアの場合、上エリアへ移動するボタンを消す
			go_down.SetActive (true);
			if(current_area == 0){
				go_up.SetActive (false);
			}

			isTransition = true;
		}
	}

	#endregion
}
