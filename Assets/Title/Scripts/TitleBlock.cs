using UnityEngine;
using System.Collections;

// タイトルのブロックの破壊の挙動
public class TitleBlock : MonoBehaviour {

	public GameObject explosion;	// 爆発エフェクトのプレハブ
	SoundMgr sound_mgr;	
	public AudioClip clip;	// 爆発サウンド

	public GameObject scroll_button, attack_button;

	// Use this for initialization
	void Start () {
		this.sound_mgr = GameObject.FindGameObjectWithTag ("Root").GetComponent<SoundMgr> ();
	}
	
	void OnCollisionEnter(Collision collision){
		this.gameObject.GetComponent<Collider> ().enabled = false;
		this.gameObject.GetComponent<Renderer> ().enabled = false;

		if (collision.gameObject.tag == "PlayerA") {
			scroll_button.SetActive (false);
		} else {
			attack_button.SetActive (false);
		}

		Instantiate (explosion, this.gameObject.transform.position, Quaternion.identity);
		sound_mgr.PlayClip (clip);
	}
}
