using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICtrl : MonoBehaviour {

	private float currentXpos, currentYpos, startXpos, startYpos;
//	private float currentXpos_b, currentYpos_b, startXpos_b, startYpos_b;
	// プレイヤーに渡す移動方向を決める要素
	public int movingXpos, movingYpos;
//	public int movingXpos_b, movingYpos_b;
	private bool touchStart;

	public Text score;	//ゲームスコア

	LinkCtrl link_ctrl;
	RaycastHit[] hits;	// レイに当たったオブジェクトを格納
	private Ray worldPoint;	// レイをだすポイント
	public bool isLinkMode;
	public bool isMainPlayer;

	// コントローラUIを表現するオブジェクト
	public GameObject controll_area, controll_stick;
//	private SpriteRenderer spRenderer;

	// Use this for initialization
	void Start () {
	//	enabled = false;	// スクリプト無効
		currentXpos = 0.0f;
		currentYpos = 0.0f;
//		currentXpos_b = 0.0f;
//		currentYpos_b = 0.0f;
		movingXpos = 0;
		movingYpos = 0;
//		movingXpos_b = 0;
//		movingYpos_b = 0;
		touchStart = false;

		score.text = "Score: " + 0;
		this.link_ctrl = GameObject.FindGameObjectWithTag ("Root").GetComponent<LinkCtrl> ();

		isLinkMode = false;
		isMainPlayer = false;
		// タッチしていない時はコントローラは非表示
		controll_area.SetActive (false);
		controll_stick.SetActive (false);
	}

	void Update(){
		// スコア更新
		score.text = "Score: " + GameMgr.total_score;

		// 画面のタップを検出
		if(Input.GetMouseButtonDown(0)){
			
			// リンクモードかどうか
			if(!isLinkMode){
				worldPoint = Camera.main.ScreenPointToRay (Input.mousePosition);
				hits = Physics.RaycastAll (worldPoint.origin, worldPoint.direction, 100);
				// プレイヤーAorBがタップされているか
				foreach (RaycastHit hit in hits) {
					if(hit.collider.gameObject.tag == "PlayerA"){
						isMainPlayer = true;
					} else if(hit.collider.gameObject.tag == "PlayerB"){
						isMainPlayer = false;
					}
					// リンクスタート
					isLinkMode = true;
					this.link_ctrl.Link_start ();
				}
			}
		}
	}

	// リンクモード終了
	public void LinkEnd(){
		this.link_ctrl.Link_end ();
		isLinkMode = false;
	}

	// 仮想操作パッド
	public void player_direction(){

		// デバッグ用
//		if(Input.GetMouseButton(0)){
//			// 指があった場合、座標を格納
//			currentXpos = Input.mousePosition.x;
//			currentYpos = Input.mousePosition.y;
//			if (!touchStart) {
//				// タッチした瞬間の座標を保存
//				startXpos = currentXpos;
//				startYpos = currentYpos;
//				// タッチした場所にコントローラを表示
//				controll_area.SetActive (true);
//				controll_stick.SetActive (true);
//
//				controll_area.transform.position = new Vector3 (startXpos, startYpos, 0.0f);
//				controll_stick.transform.position = new Vector3 (startXpos, startYpos, 0.0f);
//				touchStart = true;
//			}
//			// エリアは固定 小円は指に追従
//			Vector3 l = (controll_area.transform.position - Input.mousePosition) * 0.2f;
//			controll_stick.transform.position = new Vector3 (startXpos-l.x, startYpos-l.y, 0.0f);
//		}
//		else {
//			// 画面に指が触れていない場合
//			currentXpos = 0.0f;
//			currentYpos = 0.0f;
//			startXpos = 0.0f;
//			startYpos = 0.0f;
//
//			//			currentXpos_b = 0.0f;
//			//			currentYpos_b = 0.0f;
//			//			startXpos_b = 0.0f;
//			//			startYpos_b = 0.0f;
//			movingXpos = 0;
//			movingYpos = 0;
//			//			movingXpos_b = 0;
//			//			movingYpos_b = 0;
//			// コントローラを非表示にする
//			controll_area.SetActive (false);
//			controll_stick.SetActive (false);
//			touchStart = false;
//		}

		// 仮想操作パッド
		for(int i = 0; i < Input.touchCount; i++){
			// 
			if(Input.GetTouch(i).position.x > 0.0f){
				// 指があった場合、座標を格納
				currentXpos = Input.GetTouch(i).position.x;
				currentYpos = Input.GetTouch(i).position.y;
				if(!touchStart){
					// タッチした瞬間の座標を保存
					startXpos = currentXpos;
					startYpos = currentYpos;
					// タッチした場所にコントローラを表示
					controll_area.SetActive (true);
					controll_stick.SetActive (true);
			
					controll_area.transform.position = new Vector3 (startXpos, startYpos, 0.0f);
					controll_stick.transform.position = new Vector3 (startXpos, startYpos, 0.0f);
					touchStart = true;
				}

				// エリアは固定 小円は指に追従
				Vector3 l = (controll_area.transform.position - Input.mousePosition) * 0.2f;
				controll_stick.transform.position = new Vector3 (startXpos-l.x, startYpos-l.y, 0.0f);

			}
				
			// 画面の右側に指があるか判定
//			if(Input.GetTouch(i).position.x > (Screen.width / 2.0f)){
//				// 指があった場合、座標を格納
//				currentXpos = Input.GetTouch(i).position.x;
//				currentYpos = Input.GetTouch(i).position.y;
//				if(!touchStart){
//					// タッチした瞬間の座標を保存
//					startXpos = currentXpos;
//					startYpos = currentYpos;
//					touchStart = true;
//				}
//			} else {
//				currentXpos_b = Input.GetTouch(i).position.x;
//				currentYpos_b = Input.GetTouch(i).position.y;
//				if(!touchStart){
//					// タッチした瞬間の座標を保存
//					startXpos_b = currentXpos_b;
//					startYpos_b = currentYpos_b;
//					touchStart = true;
//				}
//			}
		}

		if(Input.touchCount == 0){
			// 画面に指が触れていない場合
			currentXpos = 0.0f;
			currentYpos = 0.0f;
			startXpos = 0.0f;
			startYpos = 0.0f;

//			currentXpos_b = 0.0f;
//			currentYpos_b = 0.0f;
//			startXpos_b = 0.0f;
//			startYpos_b = 0.0f;
			movingXpos = 0;
			movingYpos = 0;
//			movingXpos_b = 0;
//			movingYpos_b = 0;
			// コントローラを非表示にする
			controll_area.SetActive (false);
			controll_stick.SetActive (false);
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
//		if((startXpos_b - currentXpos_b) < (Screen.width * -0.05f)){
//			movingXpos = 1;
//		} else if((startXpos_b - currentXpos_b) > (Screen.width * 0.05f)){
//			movingXpos_b = -1;
//		} else {
//			movingXpos_b = 0;
//		}
//
//		// 移動地計算 Y軸
//		if((startYpos_b - currentYpos_b) < (Screen.height * -0.08f)){
//			movingYpos_b = 1;
//		} else if ((startYpos_b - currentYpos_b) > (Screen.height * 0.08f)){
//			movingYpos_b = -1;
//		} else {
//			movingYpos_b = 0;
//		}
	}
		
	void StartGame(){
		enabled = true;
	}

	void GameClear(){
		controll_area.SetActive (false);
		controll_stick.SetActive (false);
		enabled = false;
	}
}
