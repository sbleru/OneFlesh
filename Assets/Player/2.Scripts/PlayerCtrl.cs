using UnityEngine;
using System.Collections;


public class PlayerCtrl : MonoBehaviour {
	public static float ACCELERATION = 10.0f;
	public static float SPEED_MIN = 4.0f;
	public static float SPEED_MAX = 10.0f;
	public static float JUMP_HEIGHT_MAX = 10.0f;
	public static float JUMP_KEY_RELEASE_REDUCE = 0.5f;  // ジャンプからの減速値

	// 
	Rigidbody rigidbody_a, rigidbody_b, rigidbody_center, rigidbody_keeper;
	MapCreator map_creator;
	UICtrl ui_ctrl;
	SoundMgr sound_mgr;
	StageCreator stage_creator;

	public float current_speed = 0.0f;	// 現在のスピード
	public LevelCtrl level_ctrl = null;	// LevelCtrlを保持

	public GameObject vanishEffect; // プレイヤーの消滅エフェクト

	public AudioClip clip;	// 消滅サウンド
	public bool isVanish;	// 消滅したか


	// Use this for initialization
	void Start () {
		rigidbody_a = GameObject.FindGameObjectWithTag("PlayerA").GetComponent<Rigidbody>();
		rigidbody_b = GameObject.FindGameObjectWithTag("PlayerB").GetComponent<Rigidbody>();
		ui_ctrl = GameObject.FindGameObjectWithTag ("Root").GetComponent<UICtrl> ();
		sound_mgr = GameObject.FindGameObjectWithTag ("Root").GetComponent<SoundMgr> ();
		isVanish = false;
		if (GameMgr.game_mode == "TimeAttack") {
			stage_creator = GameObject.FindGameObjectWithTag ("Root").GetComponent<StageCreator> ();
		}
		if(GameMgr.game_mode == "Scroll"){
			map_creator = GameObject.FindGameObjectWithTag ("Root").GetComponent<MapCreator> ();
			rigidbody_keeper = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Rigidbody>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		// タイムアタックモード
		if (GameMgr.game_mode == "TimeAttack") {
			// 速度を設定
			Vector3 velocity_a = this.rigidbody_a.velocity;  

			// UIボタンからプレイヤーの移動先を決定する
			// プレイヤーA
			ui_ctrl.player_direction();
			int X = ui_ctrl.movingXpos;
			int Y = ui_ctrl.movingYpos;
			if(X != 0 || Y != 0){
				velocity_a.x = (float)X * Mathf.Sqrt(2.0f * 9.8f * PlayerCtrl.JUMP_HEIGHT_MAX);
				velocity_a.y = (float)Y * Mathf.Sqrt(2.0f * 9.8f * PlayerCtrl.JUMP_HEIGHT_MAX);
			}

			if (!Application.isMobilePlatform) {
				// プレイヤーAの操作
				// 順に左、上、下、右
				if (Input.GetKey (KeyCode.H)) {
					velocity_a.x = -Mathf.Sqrt (2.0f * 9.8f * PlayerCtrl.JUMP_HEIGHT_MAX);
				}
				if (Input.GetKey (KeyCode.J)) {
					velocity_a.y = Mathf.Sqrt (2.0f * 9.8f * PlayerCtrl.JUMP_HEIGHT_MAX);
				}
				if (Input.GetKey (KeyCode.K)) {
					velocity_a.y = -Mathf.Sqrt (2.0f * 9.8f * PlayerCtrl.JUMP_HEIGHT_MAX);
				}
				if (Input.GetKey (KeyCode.L)) {
					velocity_a.x = Mathf.Sqrt (2.0f * 9.8f * PlayerCtrl.JUMP_HEIGHT_MAX);
				}
			}

			// 速度を適用
			this.rigidbody_a.velocity = velocity_a;

			// しきい値からでたらゲームオーバー
			if(stage_creator.isOut(this.gameObject)){
				if(!isVanish){
					// 消滅エフェクトのプレハブを呼び出す
					Instantiate (vanishEffect, this.gameObject.transform.position, Quaternion.identity);
					sound_mgr.PlayClip (clip);
					StartCoroutine ("NextScene");
				}
				isVanish = true;
			}
		}


		// スクロールモード
		if(GameMgr.game_mode == "Scroll"){
			// 速度を設定
			Vector3 velocity_a = this.rigidbody_a.velocity;  
			Vector3 velocity_b = this.rigidbody_b.velocity;
			Vector3 velocity_keeper = this.rigidbody_keeper.velocity;
			// レベルに応じた最高速度
			this.current_speed = this.map_creator.level_ctrl.getPlayerSpeed ();

			// プレイヤーを加速
			velocity_a.x += PlayerCtrl.ACCELERATION * Time.deltaTime;
			velocity_b.x += PlayerCtrl.ACCELERATION * Time.deltaTime;
			velocity_keeper.x += PlayerCtrl.ACCELERATION * Time.deltaTime;

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
			int X = ui_ctrl.movingXpos;
			int Y = ui_ctrl.movingYpos;
			if(X != 0 || Y != 0){
				velocity_a.x = (float)X * Mathf.Sqrt(2.0f * 9.8f * PlayerCtrl.JUMP_HEIGHT_MAX);
				velocity_a.y = (float)Y * Mathf.Sqrt(2.0f * 9.8f * PlayerCtrl.JUMP_HEIGHT_MAX);
			}
			// プレイヤーB
			int X_b = ui_ctrl.movingXpos_b;
			int Y_b = ui_ctrl.movingYpos_b;
			if(X_b != 0 || Y_b != 0){
				velocity_b.x = X_b * Mathf.Sqrt(2.0f * 9.8f * PlayerCtrl.JUMP_HEIGHT_MAX);
				velocity_b.y = Y_b * Mathf.Sqrt(2.0f * 9.8f * PlayerCtrl.JUMP_HEIGHT_MAX);
			}


			if(!Application.isMobilePlatform){
				// プレイヤーAの操作
				// 順に左、上、下、右
				if(Input.GetKey(KeyCode.H)){
					velocity_a.x = -Mathf.Sqrt(2.0f * 9.8f * PlayerCtrl.JUMP_HEIGHT_MAX);
				}
				if(Input.GetKey(KeyCode.J)){
					velocity_a.y = Mathf.Sqrt(2.0f * 9.8f * PlayerCtrl.JUMP_HEIGHT_MAX);
				}
				if(Input.GetKey(KeyCode.K)){
					velocity_a.y = -Mathf.Sqrt(2.0f * 9.8f * PlayerCtrl.JUMP_HEIGHT_MAX);
				}
				if(Input.GetKey(KeyCode.L)){
					velocity_a.x = Mathf.Sqrt(2.0f * 9.8f * PlayerCtrl.JUMP_HEIGHT_MAX);
				}

				// プレイヤーBの操作
				// 順に左、上、右、下
				if(Input.GetKey(KeyCode.A)){
					velocity_b.x = -Mathf.Sqrt(2.0f * 9.8f * PlayerCtrl.JUMP_HEIGHT_MAX);
				}
				if(Input.GetKey(KeyCode.S)){
					velocity_b.y = Mathf.Sqrt(2.0f * 9.8f * PlayerCtrl.JUMP_HEIGHT_MAX);
				}
				if(Input.GetKey(KeyCode.D)){
					velocity_b.y = -Mathf.Sqrt(2.0f * 9.8f * PlayerCtrl.JUMP_HEIGHT_MAX);
				}
				if(Input.GetKey(KeyCode.F)){
					velocity_b.x = Mathf.Sqrt(2.0f * 9.8f * PlayerCtrl.JUMP_HEIGHT_MAX);
				}
			}


			// 速度を適用
			this.rigidbody_a.velocity = velocity_a;
			this.rigidbody_b.velocity = velocity_b;
			this.rigidbody_keeper.velocity = velocity_keeper;

			// しきい値からでたらゲームオーバー
			if(map_creator.isOut(this.gameObject)){
				if(!isVanish){
					// 消滅エフェクトのプレハブを呼び出す
					Instantiate (vanishEffect, this.gameObject.transform.position, Quaternion.identity);
					sound_mgr.PlayClip (clip);
					StartCoroutine ("NextScene");
				}
				isVanish = true;
			}
		}
	}

	IEnumerator NextScene(){
		yield return new WaitForSeconds (1.0f);
		Application.LoadLevel ("scScore");
	}
}
