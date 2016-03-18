using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour {
	private GameObject player;
	private Vector3 position_offset = Vector3.zero;

	// Use this for initialization
	void Start () {
		// カメラに写っていない場合
		while(CheckScreenOut ()){
			GetComponent<Camera> ().orthographicSize++;
		}

		if(GameMgr.game_mode == "Scroll"){
			player = GameObject.FindGameObjectWithTag("Keeper");
			// カメラとプレイヤーの位置の差分を保管
			position_offset = this.transform.position - player.transform.position;
		}
	}


	void LateUpdate(){
		if (GameMgr.game_mode == "Scroll") {
			// カメラの現在位置をnew_positionに取得
			Vector3 new_position = this.transform.position;
			// プレイヤーのX座標に差分を足して、変数new_positionのXに代入する
			new_position.x = player.transform.position.x + position_offset.x;
			// カメラの位置を、新しい位置に更新
			this.transform.position = new_position;	
		}
	}
		

	// オブジェクトがカメラの範囲から外れていないかチェック
	bool CheckScreenOut() 
	{ 
		bool ret;
		Vector3 view_pos = GetComponent<Camera>().WorldToViewportPoint( new Vector3(0f,0f,0f) );
		if( view_pos.x < -0.0f || 
			view_pos.x > 1.0f || 
			view_pos.y < -0.0f || 
			view_pos.y > 1.0f ) 
		{ 
			// 範囲外 
			ret = true; 
		} else {
			// 範囲内 
			ret = false; 
		}
		return ret;
	} 
}
