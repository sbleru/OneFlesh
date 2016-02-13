using UnityEngine;
using System.Collections;

public class BlockCtrl : MonoBehaviour {

	MapCreator map_creator;
	ScoreCtrl score_ctrl;
	public int block_type;
	bool is_create_new = false;

	public GameObject explosion;	// 爆発エフェクトのプレハブ

	SoundMgr sound_mgr;	
	public AudioClip clip;	// 爆発サウンド

	// Use this for initialization
	void Start () {
		map_creator = GameObject.FindGameObjectWithTag ("Root").GetComponent<MapCreator> ();
		this.score_ctrl = new ScoreCtrl ();
		sound_mgr = GameObject.FindGameObjectWithTag ("Root").GetComponent<SoundMgr> ();
	}
	
	// Update is called once per frame
	void Update () {
		// しきい値からブロックをでたブロックを削除
		if(map_creator.isDelete(this.gameObject)){
			Destroy (this.gameObject);
		}

		// 一度エネミーを作ったエネミーはもう作れない
		if(!is_create_new){
			// ある一定のしきい値をエネミーが超えたら次のエネミー作成
			if(map_creator.isCreate(this.gameObject)){
				is_create_new = true;
			}
		}

	}

	// 衝突した時
	void OnCollisionEnter(Collision collision){
		// block_type 0:赤 1:青
		if(block_type == 0 && collision.gameObject.tag == "PlayerA"){
			// Destroyだとエネミー作成していない場合、作成してくれない
			this.gameObject.GetComponent<Collider> ().enabled = false;
			this.gameObject.GetComponent<Renderer> ().enabled = false;
			// 爆発エフェクトのプレハブを呼び出す
			Instantiate (explosion, this.gameObject.transform.position, Quaternion.identity);
			sound_mgr.PlayClip (clip);
			// 加点
			score_ctrl.Add (10);
		}
		if(block_type == 1 && collision.gameObject.tag == "PlayerB"){
			this.gameObject.GetComponent<Collider> ().enabled = false;
			this.gameObject.GetComponent<Renderer> ().enabled = false;
			// 爆発エフェクトのプレハブを呼び出す
			Instantiate (explosion, this.gameObject.transform.position, Quaternion.identity);
			sound_mgr.PlayClip (clip);
			// 加点
			score_ctrl.Add (30);
		}
	}
}