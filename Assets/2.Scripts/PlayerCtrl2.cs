using UnityEngine;
using System.Collections;

public class PlayerCtrl2 : MonoBehaviour {

	private bool touchStart;
	private Vector3 difference;

	// Use this for initialization
	void Start () {
		touchStart = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (!Application.isMobilePlatform) {
			if (Input.GetMouseButton (0)) {
				Vector3 vec = Input.mousePosition;
				vec.z = 0f;

				vec = Camera.main.ScreenToWorldPoint (vec);

				// 赤球とタップ位置の差をキャッシュしておく
				if(!touchStart){
					difference = transform.position - vec;
					touchStart = true;
				}

				transform.position = vec - difference;
			}
		}

		if(Input.GetMouseButtonUp(0)){
			touchStart = false;
		}

		// タップ検出
		if (Input.touchCount > 0) {

			Touch touch = Input.GetTouch (0);

			Vector3 vec = touch.position;
			vec.z = 10f;

			vec = Camera.main.ScreenToWorldPoint (vec);

			// 赤球とタップ位置の差をキャッシュしておく
			if(!touchStart){
				difference = transform.position - vec;
				touchStart = true;
			}

			transform.position = vec - difference;

		}

		if (Input.touchCount == 0) {
			touchStart = false;
		}
	}
}
