using UnityEngine;
using System.Collections;

// タイトルのブロックの破壊の挙動
public class TitleBlock : MonoBehaviour {

	SoundMgr sound_mgr;	

	[SerializeField]
	private GameObject explosion;	// 爆発エフェクトのプレハブ
	[SerializeField]
	private AudioClip clip;	// 爆発サウンド
	[SerializeField]
	private GameObject scroll_button, attack_button;

	// Use this for initialization
	void Start () {
		this.sound_mgr = GameObject.FindGameObjectWithTag ("Root").GetComponent<SoundMgr> ();
	}
	
	void OnCollisionEnter2D(Collision2D collision){
		this.gameObject.GetComponent<Collider2D> ().enabled = false;
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;

		if (collision.gameObject.tag == "PlayerA") {
			scroll_button.SetActive (false);
		} else {
			attack_button.SetActive (false);
		}

		Instantiate (explosion, this.gameObject.transform.position, Quaternion.identity);
		sound_mgr.PlayClip (clip);
	}
}
