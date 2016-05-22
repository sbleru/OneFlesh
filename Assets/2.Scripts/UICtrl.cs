using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 画面操作によるプレイヤーの動きの処理
public class UICtrl : MonoBehaviour {

	#region public property

	private ModeChanger _mode_changer;
	public ModeChanger mode_changer
	{
		get { 
			_mode_changer = _mode_changer ?? (this.gameObject.GetComponent<ModeChanger>());
			return this._mode_changer; 
		}
	}

	// プレイヤーに渡す移動方向を決める要素
	[SerializeField]
	private float _movingXpos;
	public float movingXpos
	{
		get { return this._movingXpos; }
		private set { this._movingXpos = value; }
	}

	[SerializeField]
	private float _movingYpos;
	public float movingYpos
	{
		get { return this._movingYpos; }
		private set { this._movingYpos = value; }
	}

	#endregion


	#region private property

	private float currentXpos, currentYpos, startXpos, startYpos;
	private bool touchStart;
	[SerializeField]
	private Text score;	//ゲームスコア

	// コントローラUIを表現するオブジェクト
	[SerializeField]
	private GameObject controll_area, controll_stick;

	#endregion


	#region event
	// Use this for initialization
	void Start () {
		currentXpos = 0.0f;
		currentYpos = 0.0f;
		movingXpos = 0.0f;
		movingYpos = 0.0f;
		touchStart = false;
		score.text = "Score: " + 0;

		// タッチしていない時はコントローラは非表示
		controll_area.SetActive (false);
		controll_stick.SetActive (false);
	}

	void Update(){
		// スコア更新
		score.text = "Score: " + GameManager.Instance.total_score;

		// モードチェンジする関数を呼び出す
		if (!Application.isMobilePlatform) {
			// Aを入力
			if(Input.GetKeyDown(KeyCode.A)){
				mode_changer.ModeChange ();
			}
		}
		else{
			// プレイヤーの操作とは別のタップを検出
			if (Input.touchCount > 1)
			{
				if(Input.GetTouch(1).phase == TouchPhase.Ended){
					mode_changer.ModeChange ();
				}
			}
		}
	}

	#endregion
		

	#region public method

	// 仮想操作パッド
	public void player_direction(){

		// デバッグ用
		if (!Application.isMobilePlatform) {
			if(Input.GetMouseButton(0)){
				// 指があった場合、座標を格納
				currentXpos = Input.mousePosition.x;
				currentYpos = Input.mousePosition.y;
				if (!touchStart) {
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
				Vector3 l = (controll_area.transform.position - Input.mousePosition) * 0.4f;
				controll_stick.transform.position = new Vector3 (startXpos-l.x, startYpos-l.y, 0.0f);

			}
			else {
				// 画面に指が触れていない場合
				currentXpos = 0.0f;
				currentYpos = 0.0f;
				startXpos = 0.0f;
				startYpos = 0.0f;
				movingXpos = 0.0f;
				movingYpos = 0.0f;
				// コントローラを非表示にする
				controll_area.SetActive (false);
				controll_stick.SetActive (false);
				touchStart = false;
			}
		}


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

					//タッチした場所にコントローラを表示
					controll_area.SetActive (true);
					controll_stick.SetActive (true);
					controll_area.transform.position = new Vector3 (startXpos, startYpos, 0.0f);
					controll_stick.transform.position = new Vector3 (startXpos, startYpos, 0.0f);

					touchStart = true;
				}

				// エリアは固定 小円は指に追従
				Vector3 l = (controll_area.transform.position - Input.mousePosition) * 0.4f;
				controll_stick.transform.position = new Vector3 (startXpos-l.x, startYpos-l.y, 0.0f);

			}
		}

		if (Application.isMobilePlatform) {
			if(Input.touchCount == 0){
				// 画面に指が触れていない場合
				currentXpos = 0.0f;
				currentYpos = 0.0f;
				startXpos = 0.0f;
				startYpos = 0.0f;
				movingXpos = 0.0f;
				movingYpos = 0.0f;
				// コントローラを非表示にする
				controll_area.SetActive (false);
				controll_stick.SetActive (false);
				touchStart = false;
			}
		}

		// 移動地計算 X軸
		if((startXpos - currentXpos) < (Screen.width * -0.02f)){
			movingXpos = 0.5f;
			if ((startXpos - currentXpos) < (Screen.width * -0.06f)) {
				movingXpos = 1.0f;
			}

		} else if((startXpos - currentXpos) > (Screen.width * 0.02f)){
			movingXpos = -0.5f;
			if ((startXpos - currentXpos) > (Screen.width * 0.06f)) {
				movingXpos = -1.0f;
			}
		} else {
			movingXpos = 0.0f;
		}

		// 移動地計算 Y軸
		if((startYpos - currentYpos) < (Screen.height * -0.03f)){
			movingYpos = 0.5f;
			if ((startYpos - currentYpos) < (Screen.height * -0.10f)) {
				movingYpos = 1.0f;
			}
		} else if ((startYpos - currentYpos) > (Screen.height * 0.03f)){
			movingYpos = -0.5f;
			if ((startYpos - currentYpos) > (Screen.height * 0.10f)) {
				movingYpos = -1.0f;
			}
		} else {
			movingYpos = 0.0f;
		}
	}
		
	public void StartGame(){
		enabled = true;
	}

	public void GameClear(){
		controll_area.SetActive (false);
		controll_stick.SetActive (false);
		enabled = false;
	}

	#endregion
}
