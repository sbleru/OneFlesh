using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OneFleshCtrl : MonoBehaviour {

	public Text starter;
	private float timer;

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
		if(Input.touchCount>0){
			Application.LoadLevel ("scTitle");
		}
		if(Input.GetMouseButtonDown(0)){
			Application.LoadLevel ("scTitle");
		}
	}
}
