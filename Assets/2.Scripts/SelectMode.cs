using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectMode : MonoBehaviour {

	#region private property

	float timer = 0.0f;

	private Rigidbody2D _player_a;
	private Rigidbody2D player_a
	{
		get { 
			_player_a = _player_a ?? (GameObject.FindGameObjectWithTag("PlayerA").GetComponent<Rigidbody2D>());
			return this._player_a; 
		}
	}

	private Rigidbody2D _player_b;
	private Rigidbody2D player_b
	{
		get { 
			_player_b = _player_b ?? (GameObject.FindGameObjectWithTag("PlayerB").GetComponent<Rigidbody2D>());
			return this._player_b; 
		}
	}

	[SerializeField]
	private GameObject time_attack_button, scroll_button;

	#endregion


	#region event

	void Awake(){
		GameMgr.initialize ();

		time_attack_button.GetComponent<Button> ().enabled = false;
		scroll_button.GetComponent<Button> ().enabled = false;
	}

	// すぐにはボタンを押せなくする 
	/* タイトル画面からモードセレクト画面への遷移直後にボタンを押すと不具合が生じる対処として */
	void Update () {
		timer += Time.deltaTime;

		// タップされたら
		if(timer > 1.0f){
			time_attack_button.GetComponent<Button> ().enabled = true;
			scroll_button.GetComponent<Button> ().enabled = true;
		}

	}

	#endregion


	#region public method

	// スクロールモードでゲーム開始
	public void Scroll(){
		player_a.AddForce(new Vector2(1, -1)*20, ForceMode2D.Impulse);
		GameMgr.game_mode = "Scroll";
		FadeManager.Instance.LoadLevel ("scScroll", 0.8f);
	}


	// タイムアタックモードでゲーム開始
	public void TimeAttack(){
		player_b.AddForce(new Vector2(-1, -1)*20, ForceMode2D.Impulse);
		GameMgr.game_mode = "TimeAttack";
		FadeManager.Instance.LoadLevel ("scStageSelect", 0.8f);
	}
		
	#endregion

}
