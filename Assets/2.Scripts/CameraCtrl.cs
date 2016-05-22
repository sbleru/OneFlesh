using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour {

	#region private property

	private GameObject scroll_keeper;
	private Vector3 position_offset = Vector3.zero;

	#endregion


	#region event

	// Use this for initialization
	void Start () {
		// カメラに写っていないオブジェクトがある場合
		while(isOutOfCamera ()){
			GetComponent<Camera> ().orthographicSize++;
		}

		if(GameManager.Instance.game_mode == "Scroll"){
			scroll_keeper = GameObject.FindGameObjectWithTag("Keeper");
			// カメラとプレイヤーの位置の差分を保管
			position_offset = this.transform.position - scroll_keeper.transform.position;
		}
	}


	void LateUpdate(){
		if (GameManager.Instance.game_mode == "Scroll") {
			
			// カメラの現在位置をnew_positionに取得
			Vector3 new_position = this.transform.position;

			// プレイヤーのX座標に差分を足して、変数new_positionのXに代入する
			new_position.x = scroll_keeper.transform.position.x + position_offset.x;

			// カメラの位置を、新しい位置に更新
			this.transform.position = new_position;	
		}
	}

	#endregion


	#region private method

	// オブジェクトがカメラの範囲から外れていないかチェック
	bool isOutOfCamera() 
	{ 
		Vector3 view_pos = GetComponent<Camera>().WorldToViewportPoint( new Vector3(0f,0f,0f) );
		if (view_pos.x < -0.0f ||
		    view_pos.x >  1.0f ||
		    view_pos.y < -0.0f ||
		    view_pos.y >  1.0f) { 
			// 範囲外 
			return true; 
		}

		return false;
	} 

	#endregion
}
