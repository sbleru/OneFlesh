using UnityEngine;
using System.Collections;

// ブロック制御
public class BlockCtrl : MonoBehaviour {

	#region public property

	// block information
	[SerializeField]
	private int _block_type;
	public int block_type
	{
		get { return this._block_type; }
		set { this._block_type = value; }
	}
	[SerializeField]
	private bool _isWall;
	public bool isWall
	{
		get { return this._isWall; }
		set { this._isWall = value; }
	}

	#endregion

	#region private property

	private MapCreator _map_creator;
	private MapCreator map_creator
	{
		get { 
			_map_creator = _map_creator ?? (GameObject.FindGameObjectWithTag ("Root").GetComponent<MapCreator> ());
			return this._map_creator; 
		}
	}

	private ScoreCtrl _score_ctrl;
	private ScoreCtrl score_ctrl
	{
		get { 
			_score_ctrl = _score_ctrl ?? (GameObject.FindGameObjectWithTag ("Root").GetComponent<ScoreCtrl> ());
			return this._score_ctrl; 
		}
	}

	private Player2DCtrl _player_2dctrl;
	private Player2DCtrl player_2dctrl
	{
		get { 
			_player_2dctrl = _player_2dctrl ?? (GameObject.FindGameObjectWithTag("PlayerA").GetComponent<Player2DCtrl>());
			return this._player_2dctrl; 
		}
	}

	private ModeChanger _mode_changer;
	public ModeChanger mode_changer
	{
		get { 
			_mode_changer = _mode_changer ?? (GameObject.FindWithTag("Root").GetComponent<ModeChanger>());
			return this._mode_changer; 
		}
	}

	bool isCreateNewBlock = false;

	[SerializeField]
	private GameObject explosion;	// 爆発エフェクトのプレハブ
	[SerializeField]
	private AudioClip clip;	        // 爆発サウンド
	private float elapsed_time;	    // ブロックが生成されてからの時間

	#endregion


	#region event
	// Use this for initialization
	void Start () {
		elapsed_time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

		// スクロールモード
		if(GameManager.Instance.game_mode == "Scroll"){
			
			elapsed_time += Time.deltaTime;
			
			if(map_creator.isOutOfScreen(this.gameObject)){
				Destroy (this.gameObject);
			}

			// 壁とならないブロックは生成されてからある一定の時間が経過したらブロックを一度だけ作成
			if(isWall || isCreateNewBlock){
				// 何もしない
			} else {
				if(map_creator.isBlockCreated(this.elapsed_time)){
					isCreateNewBlock = true;
				}
			}
		}
	}

	#endregion


	#region private method
	// red_player  : red_block  -> Destroy block
	// blue_player : blue_block -> Destroy block
	// player	   : needle     -> Game over
	void OnCollisionEnter2D(Collision2D collision){

		// block_type = 0:赤, 1:青, 3:ニードル, 4:変身アイテム
		if (block_type == 0 && collision.gameObject.tag == "PlayerA") {
			
			// Destroyだとエネミー作成していない場合、作成してくれない
			this.gameObject.GetComponent<Collider2D> ().enabled = false;
			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;

			// 爆発エフェクトのプレハブを呼び出す
			Instantiate (explosion, this.gameObject.transform.position, Quaternion.identity);
			SoundManager.Instance.PlaySoundEffect (SoundManager.Instance.sound_explosion);

			if (GameManager.Instance.game_mode == "TimeAttack") {
				GameManager.Instance.left_block--;	// 残りブロック数を減らす
			} else {
				score_ctrl.AddScore (10, this.transform.position);
			}

		} else if (block_type == 1 && collision.gameObject.tag == "PlayerB") {
			
			this.gameObject.GetComponent<Collider2D> ().enabled = false;
			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;

			// 爆発エフェクトのプレハブを呼び出す
			Instantiate (explosion, this.gameObject.transform.position, Quaternion.identity);
			SoundManager.Instance.PlaySoundEffect (SoundManager.Instance.sound_explosion);

			if (GameManager.Instance.game_mode == "TimeAttack"){
				GameManager.Instance.left_block--;	// 残りブロック数を更新
			} else {
				// 青ブロックの方が得点が高い	
				score_ctrl.AddScore (30, this.transform.position);
			}

		} else if (block_type == 3 && collision.gameObject.tag == "PlayerA" || 
			       block_type == 3 && collision.gameObject.tag == "PlayerB"){

			// プレイヤーの消滅関数呼び出し クラスPlayerCtrl
			player_2dctrl.KillPlayer ();

		} else if (block_type == 4){ // プレイヤーのどの部分にぶつかっても手に入る
			this.gameObject.GetComponent<Collider2D> ().enabled = false;
			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;

			mode_changer.ModeChange ();
		}
	}

	#endregion

}

