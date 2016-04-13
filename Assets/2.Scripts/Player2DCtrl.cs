using UnityEngine;
using System.Collections;


public class Player2DCtrl : MonoBehaviour {
	public static float ACCELERATION = 8.0f;
	public static float SPEED = 8.0f;

	private Rigidbody2D _rigidbody_a;
	public Rigidbody2D rigidbody_a
	{
		get { 
			_rigidbody_a = _rigidbody_a ?? (GameObject.FindGameObjectWithTag("PlayerA").GetComponent<Rigidbody2D>());
			return this._rigidbody_a; 
		}
	}

	private Rigidbody2D _rigidbody_b;
	public Rigidbody2D rigidbody_b
	{
		get { 
			_rigidbody_b = _rigidbody_b ?? (GameObject.FindGameObjectWithTag("PlayerB").GetComponent<Rigidbody2D>());
			return this._rigidbody_b; 
		}
	}

	private Rigidbody _rigidbody_keeper;
	public Rigidbody rigidbody_keeper
	{
		get { 
			_rigidbody_keeper = _rigidbody_keeper ?? (GameObject.FindGameObjectWithTag("Keeper").GetComponent<Rigidbody>());
			return this._rigidbody_keeper; 
		}
	}

	private MapCreator _map_creator;
	public MapCreator map_creator
	{
		get { 
			_map_creator = _map_creator ?? (GameObject.FindGameObjectWithTag ("Root").GetComponent<MapCreator> ());
			return this._map_creator; 
		}
	}

	private UICtrl _ui_ctrl;
	public UICtrl ui_ctrl
	{
		get { 
			_ui_ctrl = _ui_ctrl ?? (GameObject.FindGameObjectWithTag ("Root").GetComponent<UICtrl> ());
			return this._ui_ctrl; 
		}
	}

	private SoundMgr _sound_mgr;
	public SoundMgr sound_mgr
	{
		get { 
			_sound_mgr = _sound_mgr ?? (GameObject.FindGameObjectWithTag ("Root").GetComponent<SoundMgr> ());
			return this._sound_mgr; 
		}
	}

	private StageCreator _stage_creator;
	public StageCreator stage_creator
	{
		get { 
			_stage_creator = _stage_creator ?? (GameObject.FindGameObjectWithTag ("Root").GetComponent<StageCreator> ());
			return this._stage_creator; 
		}
	}

	public float current_speed = 0.0f;	// 現在のスピード

	public GameObject vanishEffect; // プレイヤーの消滅エフェクト

	public AudioClip clip;	// 消滅サウンド
	public bool isVanish;	// 消滅したか


	// Use this for initialization
	void Start () {
		isVanish = false;
	}

	// Update is called once per frame
	void Update () {
		// タイムアタックモード
		if (GameMgr.game_mode == "TimeAttack") {
			// 速度を設定
			Vector2 velocity_a = this.rigidbody_a.velocity;  

			// UIボタンからプレイヤーの移動先を決定する
			// プレイヤーA
			ui_ctrl.player_direction();

			if(Mathf.Abs(ui_ctrl.movingXpos) > 0.0f || Mathf.Abs(ui_ctrl.movingYpos) > 0.0f){
				velocity_a.x = ui_ctrl.movingXpos * Mathf.Sqrt(2.0f * 9.8f * Player2DCtrl.SPEED);
				velocity_a.y = ui_ctrl.movingYpos * Mathf.Sqrt(2.0f * 9.8f * Player2DCtrl.SPEED);
			}

			if (!Application.isMobilePlatform) {
				// プレイヤーAの操作
				// 順に左、上、下、右
				if (Input.GetKey (KeyCode.H)) {
					velocity_a.x = -Mathf.Sqrt (2.0f * 9.8f * Player2DCtrl.SPEED);
				}
				if (Input.GetKey (KeyCode.J)) {
					velocity_a.y = Mathf.Sqrt (2.0f * 9.8f * Player2DCtrl.SPEED);
				}
				if (Input.GetKey (KeyCode.K)) {
					velocity_a.y = -Mathf.Sqrt (2.0f * 9.8f * Player2DCtrl.SPEED);
				}
				if (Input.GetKey (KeyCode.L)) {
					velocity_a.x = Mathf.Sqrt (2.0f * 9.8f * Player2DCtrl.SPEED);
				}
			}

			// 速度を適用
			this.rigidbody_a.velocity = velocity_a;

			// しきい値からでたらゲームオーバー
//			if(stage_creator.isOut(this.gameObject)){
//				if(!isVanish){
//					// 消滅エフェクトのプレハブを呼び出す
//					Instantiate (vanishEffect, this.gameObject.transform.position, Quaternion.identity);
//					sound_mgr.PlayClip (clip);
//					StartCoroutine ("NextScene");
//				}
//				isVanish = true;
//			}
			if(stage_creator.isOut(this.gameObject)){
				if(!isVanish){
					StartCoroutine (Vanish());
				}
				isVanish = true;
			}
		}


		// スクロールモード
		if(GameMgr.game_mode == "Scroll"){
			// 速度を設定
			Vector2 velocity_a = this.rigidbody_a.velocity;  
			Vector2 velocity_b = this.rigidbody_b.velocity;
			Vector3 velocity_keeper = this.rigidbody_keeper.velocity;
			// レベルに応じた最高速度
			this.current_speed = this.map_creator.level_ctrl.getPlayerSpeed ();

			// プレイヤーを加速
			velocity_a.x += Player2DCtrl.ACCELERATION * Time.deltaTime;
			velocity_b.x += Player2DCtrl.ACCELERATION * Time.deltaTime;
			velocity_keeper.x += Player2DCtrl.ACCELERATION * Time.deltaTime;

			// 速度が最高速度の制限を超えたら
			if(Mathf.Abs(velocity_keeper.x) > this.current_speed){
				// 最高速度の制限以下に保つ
				velocity_keeper.x *= this.current_speed / Mathf.Abs (velocity_keeper.x);
				velocity_a.x *= this.current_speed / Mathf.Abs (velocity_a.x);
				velocity_b.x *= this.current_speed / Mathf.Abs (velocity_b.x);
			}

			// UIボタンからプレイヤーの移動先を決定する
			// プレイヤーA
			ui_ctrl.player_direction();

			if(Mathf.Abs(ui_ctrl.movingXpos) > 0.0f || Mathf.Abs(ui_ctrl.movingYpos) > 0.0f){
				//if(X != 0 || Y != 0){
				velocity_a.x = ui_ctrl.movingXpos * Mathf.Sqrt(2.0f * 9.8f * Player2DCtrl.SPEED);
				velocity_a.y = ui_ctrl.movingYpos * Mathf.Sqrt(2.0f * 9.8f * Player2DCtrl.SPEED);
			}
			// プレイヤーB
			//			int X_b = ui_ctrl.movingXpos_b;
			//			int Y_b = ui_ctrl.movingYpos_b;
			//			if(X_b != 0 || Y_b != 0){
			//				velocity_b.x = X_b * Mathf.Sqrt(2.0f * 9.8f * Player2DCtrl.SPEED);
			//				velocity_b.y = Y_b * Mathf.Sqrt(2.0f * 9.8f * Player2DCtrl.SPEED);
			//			}


			if(!Application.isMobilePlatform){
				// プレイヤーAの操作
				// 順に左、上、下、右
				if(Input.GetKey(KeyCode.H)){
					velocity_a.x = -Mathf.Sqrt(2.0f * 9.8f * Player2DCtrl.SPEED);
				}
				if(Input.GetKey(KeyCode.J)){
					velocity_a.y = Mathf.Sqrt(2.0f * 9.8f * Player2DCtrl.SPEED);
				}
				if(Input.GetKey(KeyCode.K)){
					velocity_a.y = -Mathf.Sqrt(2.0f * 9.8f * Player2DCtrl.SPEED);
				}
				if(Input.GetKey(KeyCode.L)){
					velocity_a.x = Mathf.Sqrt(2.0f * 9.8f * Player2DCtrl.SPEED);
				}

				// プレイヤーBの操作
				// 順に左、上、右、下
				if(Input.GetKey(KeyCode.A)){
					velocity_b.x = -Mathf.Sqrt(2.0f * 9.8f * Player2DCtrl.SPEED);
				}
				if(Input.GetKey(KeyCode.S)){
					velocity_b.y = Mathf.Sqrt(2.0f * 9.8f * Player2DCtrl.SPEED);
				}
				if(Input.GetKey(KeyCode.D)){
					velocity_b.y = -Mathf.Sqrt(2.0f * 9.8f * Player2DCtrl.SPEED);
				}
				if(Input.GetKey(KeyCode.F)){
					velocity_b.x = Mathf.Sqrt(2.0f * 9.8f * Player2DCtrl.SPEED);
				}
			}


			// 速度を適用
			this.rigidbody_a.velocity = velocity_a;
			this.rigidbody_b.velocity = velocity_b;
			this.rigidbody_keeper.velocity = velocity_keeper;

			// しきい値からでたらゲームオーバー
			if(map_creator.isOut(this.gameObject)){
				if(!isVanish){
					StartCoroutine (Vanish());
				}
				isVanish = true;
			}
		}
	}

	// ゲームオーバー
	IEnumerator Vanish(){
		// 消滅エフェクトのプレハブを呼び出す
		Instantiate (vanishEffect, this.gameObject.transform.position, Quaternion.identity);
		sound_mgr.PlayClip (clip);
		this.gameObject.GetComponent<Renderer> ().enabled = false;
		GameObject.FindWithTag("PlayerB").GetComponent<Renderer> ().enabled = false;

		// ゲームオーバーをタイムマネージャーに伝える
		GameObject.FindWithTag ("TimeMgr").SendMessage ("SendGameOver");
		yield return null;
	}

	void StartGame(){
//		Debug.Log ("hoge2");
		enabled = true;
	}

	void GameClear(){
		enabled = false;
	}
}
