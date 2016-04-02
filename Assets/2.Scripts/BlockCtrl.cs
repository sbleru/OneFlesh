using UnityEngine;
using System.Collections;

// ブロック制御
public class BlockCtrl : MonoBehaviour {
	
	private MapCreator _map_creator;
	public MapCreator map_creator
	{
		get { 
			_map_creator = _map_creator ?? (GameObject.FindGameObjectWithTag ("Root").GetComponent<MapCreator> ());
			return this._map_creator; 
		}
	}

	private ScoreCtrl _score_ctrl;
	public ScoreCtrl score_ctrl
	{
		get { 
			_score_ctrl = _score_ctrl ?? (GameObject.FindGameObjectWithTag ("Root").GetComponent<ScoreCtrl> ());
			return this._score_ctrl; 
		}
	}

	private UICtrl _ui_ctrl;
	public UICtrl ui_ctrl
	{
		get { 
			_ui_ctrl = _ui_ctrl ?? (GameObject.FindGameObjectWithTag ("Root").GetComponent<UICtrl> ());
			return this._ui_ctrl; 
		}
	}

	private SoundMgr _sound_mgr;
	public SoundMgr sound_mgr
	{
		get { 
			_sound_mgr = _sound_mgr ?? (GameObject.FindGameObjectWithTag ("Root").GetComponent<SoundMgr> ());
			return this._sound_mgr; 
		}
	}

	public int block_type;
	public bool isBorder = false;
	bool is_create_new = false;

	[SerializeField]
	private GameObject explosion;	// 爆発エフェクトのプレハブ
	[SerializeField]
	private AudioClip clip;	// 爆発サウンド

//	private struct Block{
//		public bool is_link_block;
//		public bool is_last_link_block;
//	};
//
//	Block link_block;
	private float elapsed_time;	// ブロックが生成されてからの時間

	// Use this for initialization
	void Start () {
//		link_block.is_link_block = false;
//		link_block.is_last_link_block = false;
		elapsed_time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

		// スクロールモード
		if(GameMgr.game_mode == "Scroll"){
			// 時間計測
			elapsed_time += Time.deltaTime;
			// しきい値からブロックをでたブロックを削除
			if(map_creator.isDelete(this.gameObject)){
				Destroy (this.gameObject);
			}

			// ブロックタイプが境界線ブロック以外だったら
			if(!isBorder){
				// 一度ブロックを作ったブロックはもう作れない
				if(!is_create_new){
					// 生成されてからある一定の時間が経過したら次のブロック作成
					if(map_creator.isCreate(this.elapsed_time)){
						is_create_new = true;
					}
				}
			}
	
		}
	}

	// 衝突した時
	void OnCollisionEnter2D(Collision2D collision){
//	void OnCollisionEnter(Collision collision){

		// block_type 0:赤 1:青
		if (block_type == 0 && collision.gameObject.tag == "PlayerA") {
			// Destroyだとエネミー作成していない場合、作成してくれない
//			this.gameObject.GetComponent<Collider> ().enabled = false;
//			this.gameObject.GetComponent<Renderer> ().enabled = false;
			this.gameObject.GetComponent<Collider2D> ().enabled = false;
			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			// 爆発エフェクトのプレハブを呼び出す
			Instantiate (explosion, this.gameObject.transform.position, Quaternion.identity);
			sound_mgr.PlayClip(clip);

			// モードごとの処理
			if (GameMgr.game_mode == "TimeAttack") {
				GameMgr.left_block--;	// 残りブロック数を更新
			}
			else if(GameMgr.game_mode == "Scroll"){
				// 加点
				score_ctrl.Add (10);
			}

		} else if (block_type == 1 && collision.gameObject.tag == "PlayerB") {
//			this.gameObject.GetComponent<Collider> ().enabled = false;
//			this.gameObject.GetComponent<Renderer> ().enabled = false;
			this.gameObject.GetComponent<Collider2D> ().enabled = false;
			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
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

		} else if(block_type == 3 && collision.gameObject.tag == "PlayerA" || 
			block_type == 3 && collision.gameObject.tag == "PlayerB"){
				// プレイヤーの消滅関数呼び出し クラスPlayerCtrl
				GameObject.FindWithTag ("PlayerA").SendMessage ("Vanish");
		}
	}

	// リンクモード時にタップされたとき呼ばれる
//	public void LinkBlock(){
//		// 1度目のタップ
//		if (!link_block.is_link_block) {
//			link_block.is_link_block = true;
//		} else {
//			// 2度目のタップ
//			if (!link_block.is_last_link_block) {
//				ui_ctrl.LinkEnd ();
//				link_block.is_last_link_block = true;
//			}
//		}
//	}


}