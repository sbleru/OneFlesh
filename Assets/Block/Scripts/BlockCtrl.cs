using UnityEngine;
using System.Collections;

public class BlockCtrl : MonoBehaviour {

	MapCreator map_creator;
	ScoreCtrl score_ctrl;
	LinkCtrl link_ctrl;
	UICtrl ui_ctrl;
	public int block_type;
	bool is_create_new = false;

	public GameObject explosion;	// 爆発エフェクトのプレハブ

	SoundMgr sound_mgr;	
	public AudioClip clip;	// 爆発サウンド

	private struct Block{
		public bool is_link_block;
		public bool is_last_link_block;
	};

	Block link_block;

	// Use this for initialization
	void Start () {
		this.map_creator = GameObject.FindGameObjectWithTag ("Root").GetComponent<MapCreator> ();
		this.score_ctrl = GameObject.FindGameObjectWithTag ("Root").GetComponent<ScoreCtrl> ();
		this.sound_mgr = GameObject.FindGameObjectWithTag ("Root").GetComponent<SoundMgr> ();
		this.ui_ctrl = GameObject.FindGameObjectWithTag ("Root").GetComponent<UICtrl> ();
		link_block.is_link_block = false;
		link_block.is_last_link_block = false;
	}
	
	// Update is called once per frame
	void Update () {
		// スクロールモード
		if(GameMgr.game_mode == "Scroll"){
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
	}

	// 衝突した時
	void OnCollisionEnter(Collision collision){
		// block_type 0:赤 1:青
		if (block_type == 0 && collision.gameObject.tag == "PlayerA") {
			// Destroyだとエネミー作成していない場合、作成してくれない
			this.gameObject.GetComponent<Collider> ().enabled = false;
			this.gameObject.GetComponent<Renderer> ().enabled = false;
			// 爆発エフェクトのプレハブを呼び出す
			Instantiate (explosion, this.gameObject.transform.position, Quaternion.identity);
			sound_mgr.PlayClip (clip);

			// モードごとの処理
			if (GameMgr.game_mode == "TimeAttack") {
				GameMgr.left_block--;	// 残りブロック数を更新
			}
			else if(GameMgr.game_mode == "Scroll"){
				// 加点
				score_ctrl.Add (10);
			}

		} else if (block_type == 1 && collision.gameObject.tag == "PlayerB") {
			this.gameObject.GetComponent<Collider> ().enabled = false;
			this.gameObject.GetComponent<Renderer> ().enabled = false;
			// 爆発エフェクトのプレハブを呼び出す
			Instantiate (explosion, this.gameObject.transform.position, Quaternion.identity);
			sound_mgr.PlayClip (clip);

			// モードごとの処理
			if (GameMgr.game_mode == "TimeAttack") {
				GameMgr.left_block--;	// 残りブロック数を更新
			}
			else if(GameMgr.game_mode == "Scroll"){
				// 加点
				score_ctrl.Add (30);
			}
		}
	}

	// リンクモード時にタップされたとき呼ばれる
	public void LinkBlock(){
		// 1度目のタップ
		if(!link_block.is_link_block){
			link_block.is_link_block = true;
		} else {
			// 2度目のタップ
			if(!link_block.is_last_link_block){
				ui_ctrl.LinkEnd ();
				link_block.is_last_link_block = true;
			}
		}
	}
}