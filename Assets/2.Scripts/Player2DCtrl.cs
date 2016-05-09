using UnityEngine;
using System.Collections;


public class Player2DCtrl : MonoBehaviour {

	#region const

	private const float ACCELERATION = 8.0f;
	private const float SPEED = 8.0f;

	#endregion


	#region private propety

	private Rigidbody2D _rigidbody_a;
	private Rigidbody2D rigidbody_a
	{
		get { 
			_rigidbody_a = _rigidbody_a ?? (GameObject.FindGameObjectWithTag("PlayerA").GetComponent<Rigidbody2D>());
			return this._rigidbody_a; 
		}
	}

	private Rigidbody2D _rigidbody_b;
	private Rigidbody2D rigidbody_b
	{
		get { 
			_rigidbody_b = _rigidbody_b ?? (GameObject.FindGameObjectWithTag("PlayerB").GetComponent<Rigidbody2D>());
			return this._rigidbody_b; 
		}
	}

	private Rigidbody _rigidbody_keeper;
	private Rigidbody rigidbody_keeper
	{
		get { 
			_rigidbody_keeper = _rigidbody_keeper ?? (GameObject.FindGameObjectWithTag("Keeper").GetComponent<Rigidbody>());
			return this._rigidbody_keeper; 
		}
	}

	private MapCreator _map_creator;
	private MapCreator map_creator
	{
		get { 
			_map_creator = _map_creator ?? (GameObject.FindGameObjectWithTag ("Root").GetComponent<MapCreator> ());
			return this._map_creator; 
		}
	}

	private UICtrl _ui_ctrl;
	private UICtrl ui_ctrl
	{
		get { 
			_ui_ctrl = _ui_ctrl ?? (GameObject.FindGameObjectWithTag ("Root").GetComponent<UICtrl> ());
			return this._ui_ctrl; 
		}
	}

	private StageCreator _stage_creator;
	private StageCreator stage_creator
	{
		get { 
			_stage_creator = _stage_creator ?? (GameObject.FindGameObjectWithTag ("Root").GetComponent<StageCreator> ());
			return this._stage_creator; 
		}
	}

	private float current_speed = 0.0f;	 // 現在のスピード
	[SerializeField]
	private GameObject vanishEffect;     // プレイヤーの消滅エフェクト
	private bool isVanish;				 // プレイヤーが消滅したか

	#endregion


	#region
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

			if(stage_creator.isOutOfScreen(this.gameObject)){
				if(!isVanish){
					KillPlayer ();
				}
				isVanish = true;
			}
		}


		// スクロールモード
		if(GameMgr.game_mode == "Scroll"){
			// 速度を設定
			Vector2 velocity_a = this.rigidbody_a.velocity;  
			Vector3 velocity_keeper = this.rigidbody_keeper.velocity;
			// レベルに応じた最高速度
			this.current_speed = this.map_creator.level_ctrl.getPlayerSpeed ();

			// プレイヤーを加速
			velocity_a.x += Player2DCtrl.ACCELERATION * Time.deltaTime;
			velocity_keeper.x += Player2DCtrl.ACCELERATION * Time.deltaTime;

			// 速度が最高速度の制限を超えたら
			if(Mathf.Abs(velocity_keeper.x) > this.current_speed){
				// 最高速度の制限以下に保つ
				velocity_keeper.x *= this.current_speed / Mathf.Abs (velocity_keeper.x);
				velocity_a.x *= this.current_speed / Mathf.Abs (velocity_a.x);
			}

			// UIボタンからプレイヤーの移動先を決定する
			ui_ctrl.player_direction();

			if(Mathf.Abs(ui_ctrl.movingXpos) > 0.0f || Mathf.Abs(ui_ctrl.movingYpos) > 0.0f){
				//if(X != 0 || Y != 0){
				velocity_a.x = ui_ctrl.movingXpos * Mathf.Sqrt(2.0f * 9.8f * Player2DCtrl.SPEED);
				velocity_a.y = ui_ctrl.movingYpos * Mathf.Sqrt(2.0f * 9.8f * Player2DCtrl.SPEED);
			}


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
			}


			// 速度を適用
			this.rigidbody_a.velocity = velocity_a;
			this.rigidbody_keeper.velocity = velocity_keeper;

			// しきい値からでたらゲームオーバー
			if(map_creator.isOutOfScreen(this.gameObject)){
				if(!isVanish){
					KillPlayer ();
				}
				isVanish = true;
			}
		}
	}

	#endregion


	#region public method

	// プレイヤーを消滅させる
	public void KillPlayer(){
		// 消滅エフェクトのプレハブを呼び出す
		Instantiate (vanishEffect, this.gameObject.transform.position, Quaternion.identity);
		SoundManager.Instance.PlaySoundEffect (SoundManager.Instance.sound_vanish);
		this.gameObject.GetComponent<Renderer> ().enabled = false;
		GameObject.FindWithTag("PlayerB").GetComponent<Renderer> ().enabled = false;

		// ゲームオーバーをタイムマネージャーに伝える
		GameObject.FindWithTag ("TimeMgr").SendMessage ("SendGameOver");
	}

	public void StartGame(){
		enabled = true;
	}

	public void GameClear(){
		enabled = false;
	}

	#endregion
}
