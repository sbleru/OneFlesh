using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System.IO;

// TimeAttack:テキストデータからステージを作成する
[RequireComponent (typeof(BlockCreator))]
public class StageCreator : MonoBehaviour {
	public static float BLOCK_WIDTH = 1.0f;
	public static float BLOCK_HEIGHT = 0.2f;
	public static int BLOCK_NUM_IN_SCREEN = 40;
	public static int SCREEN_HEIGHT = 24;

	private BlockCreator _block_creator;
	public BlockCreator block_creator
	{
		get { 
			_block_creator = _block_creator ?? (GameObject.FindGameObjectWithTag ("Root").GetComponent<BlockCreator> ());
			return this._block_creator; 
		}
	}

	private Timekeeper _time_keeper;
	public Timekeeper time_keeper
	{
		get { 
			_time_keeper = _time_keeper ?? (GameObject.FindGameObjectWithTag ("TimeMgr").GetComponent<Timekeeper> ());
			return this._time_keeper; 
		}
	}

	private TextAsset stage_asset;  // ステージテキストを取り込む
	string stage_txt;
	[SerializeField]
	private int[,] stage_data = new int[BLOCK_NUM_IN_SCREEN, SCREEN_HEIGHT];
	[SerializeField]
	private GameObject player;

	[SerializeField]
	private GameObject[] player2d;
	private GameObject tempObj;

	private bool isDestroyPlayer1, isDestroyPlayer2;
	private float time_mode_change;
	private bool isModeChange;


	// Use this for initialization
	void Start () {
		isDestroyPlayer1 = false;
		isDestroyPlayer2 = false;
		time_mode_change = 5.0f;
		isModeChange = false;

		// ステージ番号に応じたステージテキストを取り込む
		stage_asset = Resources.Load ("stage" + GameMgr.stage_num) as TextAsset;
		stage_txt = stage_asset.text;
		get_stage_data ();

		// ステージを作成する
		create_stage ();
	}


//	void Update(){
//
//		if(isModeChange){
//			time_mode_change -= Time.deltaTime;
//			if(time_mode_change < 0.0f){
//				float i = tempObj.transform.position.x;
//				float j = tempObj.transform.position.y;
//				Destroy (tempObj);
//				tempObj = Instantiate(player2d[0],new Vector2((float)i, (float)j), Quaternion.Euler(new Vector2(0f,0f))) as GameObject;
//
//				tempObj.GetComponent<Player2DCtrl> ().enabled = true;
//
//				isModeChange = false;
//			}
//		}
//
//	}


//	public void ModeChange(){
//
//		float i = tempObj.transform.position.x;
//		float j = tempObj.transform.position.y;
//		Destroy (tempObj);
//		tempObj = Instantiate(player2d[1],new Vector2((float)i, (float)j), Quaternion.Euler(new Vector2(0f,0f))) as GameObject;
//		isModeChange = true;
//	}


	// ステージ作成
	private void get_stage_data(){

		string[] lines = stage_txt.Split ('\n');
		int i=0, j=0;

		//lines内の各行に対して、順番に処理していくループ
		foreach(var line in lines){
			if(line == ""){ //行がなければ
				continue;  
			}
				
			string[] words = line.Split ();

			//words内の各ワードに対して、順番に処理していくループ
			foreach(var word in words){
				if(word == ""){
					continue;
				}
				stage_data [i, j] = int.Parse (word);
				
				j++;
				if (j > SCREEN_HEIGHT-1){
					break;
				}
			}
			j = 0;
			i++;
			if (i > BLOCK_NUM_IN_SCREEN-1){
				break;
			}
		}
	}

	//
	private void create_stage(){
		int i, j;

		for(i=0; i<BLOCK_NUM_IN_SCREEN; i++){

			for(j=0; j<SCREEN_HEIGHT; j++){
				do {
					switch(stage_data[i,j]){
		
					case 1:	// 中身が1なら赤作成
//						block_creator.createBlock(new Vector3((float)i, (float)j, 0.0f), 0);
						block_creator.createBlock(new Vector2((float)i, (float)j), 0);
						GameMgr.left_block++;	// 残りブロックとして登録
						break;
					case 2:	// 中身が2なら青作成
//						block_creator.createBlock(new Vector3((float)i, (float)j, 0.0f), 1);
						block_creator.createBlock(new Vector2((float)i, (float)j), 1);
						GameMgr.left_block++;	// 残りブロックとして登録
						break;
					case 3:	// 破壊不可ブロック
//						block_creator.createBlock(new Vector3((float)i, (float)j, 0.0f), 2);
						block_creator.createBlock(new Vector2((float)i, (float)j), 2);
						break;
					case 4:	// ニードル
//						block_creator.createBlock(new Vector3((float)i, (float)j, 0.0f), 3);
						block_creator.createBlock(new Vector2((float)i, (float)j), 3);
						break;
					case 9: // プレイヤー
//						Instantiate(player,new Vector3((float)i, (float)j, 0.0f), Quaternion.Euler(new Vector3(0f,90f,0f)));
						tempObj = Instantiate(player2d[0],new Vector2((float)i, (float)j), Quaternion.Euler(new Vector2(0f,0f))) as GameObject;
						break;
					default:
						break;
					}
				} while(false);
			}
		}
	}
		

	// プレイヤーがステージ外へ出ているか
	public bool isOut(GameObject player){
		bool ret = false;

		do {
			// 左のしきい値の計算
			float limit_left = this.transform.position.x - (((float)BLOCK_NUM_IN_SCREEN + 8) / 2.0f);
			// ブロックがしきい値をでていたら削除の指示
			if(player.transform.position.x < limit_left){
				ret = true;
				break;
			}
			// 上のしきい値の計算
			float limit_up = this.transform.position.y + (float)SCREEN_HEIGHT;
			if(player.transform.position.y > limit_up){
				ret = true;
				break;
			}
			// 上のしきい値の計算
			float limit_down = this.transform.position.y - (float)SCREEN_HEIGHT;
			if(player.transform.position.y < limit_down){
				ret = true;
				break;
			}
		} while(false);

		return ret;
	}

}
