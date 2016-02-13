using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour {
	private GameObject player;
	private Vector3 position_offset = Vector3.zero;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Keeper");
		// カメラとプレイヤーの位置の差分を保管
		position_offset = this.transform.position - player.transform.position;
	}


	void LateUpdate(){
		// カメラの現在位置をnew_positionに取得
		Vector3 new_position = this.transform.position;
		// プレイヤーのX座標に差分を足して、変数new_positionのXに代入する
		new_position.x = player.transform.position.x + position_offset.x;
		// カメラの位置を、新しい位置に更新
		this.transform.position = new_position;		
	}
}
