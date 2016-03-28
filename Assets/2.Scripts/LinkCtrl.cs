//using UnityEngine;
//using System.Collections;
//
//public class LinkCtrl : MonoBehaviour {
//	UICtrl ui_ctrl;
//	SoundMgr sound_mgr;
//	public AudioClip clip;
//
//	RaycastHit[] hits;	// レイに当たったオブジェクトを格納
//	private Ray worldPoint;	// レイをだすポイント
//
//	GameObject[] link_block = new GameObject[10];	// リンクさせるブロックを格納
//	GameObject player_a, player_b;
//	int link_count;
//
//	// Use this for initialization
//	void Start () {
//		ui_ctrl = GameObject.FindGameObjectWithTag ("Root").GetComponent<UICtrl> ();
//		sound_mgr = GameObject.FindGameObjectWithTag ("Root").GetComponent<SoundMgr> ();
//		player_a = GameObject.FindGameObjectWithTag ("PlayerA");
//		player_b = GameObject.FindGameObjectWithTag ("PlayerB");
//		link_count = 0;
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		// リンクモードかどうか
//		if (ui_ctrl.isLinkMode) {
//
//			// 画面のタップを検出
//			if (Input.GetMouseButtonDown (0)) {
//
//				worldPoint = Camera.main.ScreenPointToRay (Input.mousePosition);
//				hits = Physics.RaycastAll (worldPoint.origin, worldPoint.direction, 100);
//				// ブロックがタップされているか
//				foreach (RaycastHit hit in hits) {
//					if (hit.collider.gameObject.tag == "Block") {
//						// ブロックのリンク情報を更新
//						hit.collider.gameObject.SendMessage("LinkBlock");
//						// リンク候補として保存
//						link_block [link_count++] = hit.collider.gameObject;
//					}
//				}
//			}
//		}
//
//	}
//
//	//
//	public void Link_start(){
//		Time.timeScale = 0;
//		// 一定時間たったらリンクモード解除
//		//StartCoroutine ("LinkStop");
//	}
//
//
//	//リンクモード終了
//	public void Link_end(){
//		Time.timeScale = 1;
//		StartCoroutine ("LinkDestroy");
//
//	}
//
//	IEnumerator LinkDestroy(){
//		int j;
//		if(ui_ctrl.isMainPlayer){
//			print (link_count);
//			// このfor文ちょいおかしい
//			for(j=0; j<link_count; j++){
//				sound_mgr.PlayClip (clip);
//				// プレイヤーがブロックめがけて移動
//				iTween.MoveTo( player_a.gameObject, link_block[j].transform.position, 0.5f ); 
//				yield return new WaitForSeconds(0.8f);
//			}
//		} else {
//			for(j=0; j<link_count; j++){
//				sound_mgr.PlayClip (clip);
//				// プレイヤーがブロックめがけて移動
//				iTween.MoveTo( player_b.gameObject, link_block[j].transform.position, 0.5f ); 
//				yield return new WaitForSeconds(0.8f);
//			}
//		}
//		// リンクカウントをリセット
//		link_count = 0;
//	}
//}
