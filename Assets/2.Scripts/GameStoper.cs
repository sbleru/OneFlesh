using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStoper : MonoBehaviour {

	#region private property

	private bool isGameClear;
	private bool isExecutedSendGameOver;
	[SerializeField]
	private Text finish;

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

	// Use this for initialization
	void Start () {
		isGameClear = false;
		isExecutedSendGameOver = false;
		finish.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.Instance.game_mode == "TimeAttack"){
			if(!isGameClear){
				// すべてのブロックを破壊したら
				if(GameManager.Instance.left_block < 1){
					isGameClear = true;
					SendGameOver ();
				}
			}
		} 
	}

	#endregion


	#region public method

	// ゲームオーバーを伝える  
	public void SendGameOver(){
		// この関数は一回だけ機能するようにする
		/* クリアと死亡がほぼ同時に起こったときに早い方を判定するため */
		if(!isExecutedSendGameOver && GameManager.Instance.game_mode == "TimeAttack"){
				
			//終了の合図を送る
			time_keeper.GameClear ();
			ui_ctrl.GameClear ();
			player_2dctrl.GameClear ();
			// SendGameOverの送り元からリタイアかクリアか判断する
			GameManager.Instance.isRetire = isGameClear ? false : true;
			StartCoroutine ("NextScene", "scTimeScore");

		} 
		else if(!isExecutedSendGameOver && GameManager.Instance.game_mode == "Scroll"){
			
			StartCoroutine ("NextScene", "scScrollScore");
		}
		isExecutedSendGameOver = true;
	}

	#endregion


	#region private method

	IEnumerator NextScene(string scene){
		finish.text = "FINISH!!";
		Time.timeScale = 0.3f;	// スローモーションにする
		yield return new WaitForSeconds (0.5f);
		Time.timeScale = 1.0f;

		SoundManager.Instance.PlayTitleBGM ();
		SceneManager.LoadScene(scene);
	}

	#endregion
		
}
