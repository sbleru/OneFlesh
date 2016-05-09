using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleCtrl : MonoBehaviour {

	#region private property

	[SerializeField]
	private Text starter_txt;
	private float timer;

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

	#endregion


	#region event

	// Use this for initialization
	void Start () {
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		//タイマーの少数部分をアルファ値とすることで文字をフェードアウト
		starter_txt.color = new Color (1, 1, 1, timer - Mathf.FloorToInt (timer));

		if(timer > 2.0f){

			if(Input.touchCount>0){				
				StartCoroutine (FadeOut());
			}
			if(Input.GetMouseButtonDown(0)){
				StartCoroutine (FadeOut ());
			}
		}
	}

	#endregion


	#region private method

	IEnumerator FadeOut(){
		player_a.AddForce(new Vector2(1, -10) * 10, ForceMode2D.Impulse);
		player_b.AddForce(new Vector2(-1, -10) * 10, ForceMode2D.Impulse);
		FadeManager.Instance.LoadLevel ("scTitle", 1.0f);
		yield return null;
	}

	#endregion
}
	

/* コーディング規約
 * 
 * 命名規則
 * 	クラス名　　　　　：CamelCase
 * 	変数名　　　　　　：lower_separated
 * 	定数　　　　　　　：kConstantName
 * 	ローカル変数　　　：_offset
 * 	クラスのメンバ変数：offset
 * 
 * オブジェクトなどを使用時に取得する
 * 順序問題で取得していないものを参照してしまうことをなくす
 * 	private notset notset;
 * 	public notset notset
 * 	{
 * 		get { 
 * 			notset = notset ?? (notset);
 * 			return this.notset; 
 * 		}
 * 	}
 * 
 * publicな変数はプロパティを使用
 * inspectorに表示させたいときは, バッキングフィールドに[SerializeField]をつける
 * spropに割り当てられたフォーマットを使用
 * 
 */