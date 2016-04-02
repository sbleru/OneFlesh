using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Title0Ctrl : MonoBehaviour {

	[SerializeField]
	private Text starter;
	private float timer;

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

	public iTween.EaseType playerEaseType;

	// Use this for initialization
	void Start () {
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		//タイマーの少数部分をアルファ値とすることで文字をフェードアウト
		starter.color = new Color (1, 1, 1, timer - Mathf.FloorToInt (timer));

		// タップされたら
		if(timer > 2.0f){

			if(Input.touchCount>0){				
				StartCoroutine (FadeOut());
			}
			if(Input.GetMouseButtonDown(0)){
				StartCoroutine (FadeOut ());
			}
		}

	}

	IEnumerator FadeOut(){
//		iTween.MoveTo(player_a, iTween.Hash("position", Vector3.down,
//			"islocal", true,
//			"time", 1,
//			"easetype", playerEaseType
//		));
		player_a.AddForce(new Vector2(1, -10) * 100, ForceMode2D.Impulse);
		player_b.AddForce(new Vector2(-1, -10) * 100, ForceMode2D.Impulse);



		FadeManager.Instance.LoadLevel ("scTitle", 1.0f);
		yield return null;
	}
}
