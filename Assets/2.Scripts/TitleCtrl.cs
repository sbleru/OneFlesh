using UnityEngine;
using System.Collections;

public class TitleCtrl : MonoBehaviour {

//	[SerializeField]
//	private GameObject player_a, player_b;

	private Rigidbody2D _player_a;
	public Rigidbody2D player_a
	{
		get { 
			_player_a = _player_a ?? (GameObject.FindGameObjectWithTag("PlayerA").GetComponent<Rigidbody2D>());
			return this._player_a; 
		}
	}

	private Rigidbody2D _player_b;
	public Rigidbody2D player_b
	{
		get { 
			_player_b = _player_b ?? (GameObject.FindGameObjectWithTag("PlayerB").GetComponent<Rigidbody2D>());
			return this._player_b; 
		}
	}

	void Awake(){
		GameMgr.initialize ();
	}

	// スクロールモードでゲーム開始
	public void Scroll(){
//		iTween.MoveTo( player_a.gameObject, new Vector2(1.0f,-1.0f), 0.5f );
		player_a.AddForce(new Vector2(1, -1)*20, ForceMode2D.Impulse);
		GameMgr.game_mode = "Scroll";
		FadeManager.Instance.LoadLevel ("scScrollStageSelect", 0.8f);
	}


	// タイムアタックモードでゲーム開始
	public void TimeAttack(){
//		iTween.MoveTo( player_b.gameObject, new Vector2(-1.0f,-1.0f), 0.5f ); 
		player_b.AddForce(new Vector2(-1, -1)*20, ForceMode2D.Impulse);
		GameMgr.game_mode = "TimeAttack";
		FadeManager.Instance.LoadLevel ("scStageSelect", 0.8f);
	}
		
}
