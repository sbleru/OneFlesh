using UnityEngine;
using System.Collections;

// タイトルのブロックの破壊の挙動
public class TitleBlock : MonoBehaviour {

	#region private property

	[SerializeField]
	private GameObject explosion;	// 爆発エフェクトのプレハブ
	[SerializeField]
	private AudioClip clip;	        // 爆発サウンド
	[SerializeField]
	private GameObject scroll_button, attack_button;

	#endregion


	#region private method
	
	void OnCollisionEnter2D(Collision2D collision){
		this.gameObject.GetComponent<Collider2D> ().enabled = false;
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;

		if (collision.gameObject.tag == "PlayerA") {
			scroll_button.SetActive (false);
		} else {
			attack_button.SetActive (false);
		}

		Instantiate (explosion, this.gameObject.transform.position, Quaternion.identity);

		SoundManager.Instance.PlaySoundEffect (SoundManager.Instance.sound_explosion);
	}

	#endregion

}
