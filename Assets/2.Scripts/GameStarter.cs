using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStarter : MonoBehaviour {

	#region private propety

	[SerializeField]
	private Text starter;
	private float count_down_secs;

	private TimeKeeper _time_keeper;
	private TimeKeeper time_keeper
	{
		get { 
			_time_keeper = _time_keeper ?? (this.gameObject.GetComponent<TimeKeeper>());
			return this._time_keeper; 
		}
	}

	private UICtrl _ui_ctrl;
	private UICtrl ui_ctrl
	{
		get { 
			_ui_ctrl = _ui_ctrl ?? (GameObject.FindWithTag ("Root").GetComponent<UICtrl>());
			return this._ui_ctrl; 
		}
	}

	private Player2DCtrl _player_2dctrl;
	private Player2DCtrl player_2dctrl
	{
		get { 
			_player_2dctrl = _player_2dctrl ?? (GameObject.FindWithTag ("PlayerA").GetComponent<Player2DCtrl>());
			return this._player_2dctrl; 
		}
	}

	#endregion


	#region event

	void Awake(){
		GameManager.Instance.left_block = 0;
		GameManager.Instance.total_score = 0;
		Application.targetFrameRate = 60;

		SoundManager.Instance.PlayBGM ();
	}

	// Use this for initialization
	void Start () {
		count_down_secs = 3.2f; //3秒 + α
	}

	// Update is called once per frame
	void Update () {
		count_down_secs -= Time.deltaTime;
		//表示用のカウント
		//タイマーの時間を切り上げして整数にする
		string text = Mathf.CeilToInt (count_down_secs).ToString ();
		//タイマーの少数部分をアルファ値とすることで文字をフェードアウト
		starter.color = new Color (1, 1, 1, count_down_secs - Mathf.FloorToInt (count_down_secs));
		starter.text = text;

		if(count_down_secs < 0.0f){

			starter.text = "GO!!";
			if(count_down_secs < -0.5f){
				if(GameManager.Instance.game_mode == "TimeAttack"){
					time_keeper.StartGame ();
				}
				ui_ctrl.StartGame ();
				player_2dctrl.StartGame ();

				enabled = false;
				starter.enabled = false;
			}
		}
	}

	#endregion
}