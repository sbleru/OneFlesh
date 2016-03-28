using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Title0Ctrl : MonoBehaviour {

	[SerializeField]
	private Text starter;
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
		if(timer > 2.0f){
			if(Input.touchCount>0){
				StartCoroutine (FadeOut());
			}
			if(Input.GetMouseButtonDown(0)){
				FadeManager.Instance.LoadLevel ("scTitle", 1.0f);
			}
		}

	}

	IEnumerator FadeOut(){
		FadeManager.Instance.LoadLevel ("scTitle", 0.5f);
		yield return null;
	}
}
