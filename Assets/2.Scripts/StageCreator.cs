using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System.IO;

// TimeAttack:テキストデータからステージを作成する
[RequireComponent (typeof(BlockCreator))]
public class StageCreator : MonoBehaviour {

	#region const

	private const float BLOCK_WIDTH = 1.0f;
	private const int BLOCK_NUM_IN_SCREEN = 40, SCREEN_HEIGHT = 24;

	// 生成するブロックの種類に番号を割り当てる
	private const int RED = 0, BLUE = 1, METAL = 2, NEEDLE = 3;

	#endregion


	#region private property

	private BlockCreator _block_creator;
	private BlockCreator block_creator
	{
		get { 
			_block_creator = _block_creator ?? (GameObject.FindGameObjectWithTag ("Root").GetComponent<BlockCreator> ());
			return this._block_creator; 
		}
	}

	[SerializeField]
	private int[,] stage_data = new int[BLOCK_NUM_IN_SCREEN, SCREEN_HEIGHT];
	[SerializeField]
	private GameObject[] player;

	#endregion


	#region event

	// Use this for initialization
	void Start () {
		
		capture_stage_data ();
		create_stage ();
	}

	#endregion


	#region public method

	// 渡されたオブジェクトがステージ外へ出ているか 左->右->上->下の順に調べる
	/* ブロックは左にのみ外れるため左の閾値から調べてはやく返す */
	public bool isOutOfScreen(GameObject _player){

		// 左のしきい値の計算
		float limit_left = 0;
		// ブロックがしきい値をでていたら削除の指示
		if(_player.transform.position.x < limit_left){
			return true;
		}
		// 右のしきい値の計算
		float limit_right = (float)BLOCK_NUM_IN_SCREEN;
		if(_player.transform.position.x > limit_right){
			return true;
		}
		// 上のしきい値の計算
		float limit_up = (float)SCREEN_HEIGHT;
		if(_player.transform.position.y > limit_up){
			return true;
		}
		// 上のしきい値の計算
		float limit_bottom = 0;
		if(_player.transform.position.y < limit_bottom){
			return true;
		}

		return false;
	}

	#endregion


	#region private method

	// ステージ作成
	private void capture_stage_data(){

		// ステージ番号に応じたステージテキストを取り込む
		TextAsset _stage_asset = Resources.Load ("stage" + GameMgr.stage_num) as TextAsset;

		string _stage_txt = _stage_asset.text;
		string[] _lines = _stage_txt.Split ('\n');
		int i=0, j=0;

		// lines内の各行に対して、順番に処理していくループ
		foreach(var line in _lines){
			if(line == ""){ // 行がなければ
				continue;  
			}
				
			string[] _words = line.Split ();

			// words内の各ワードに対して、順番に処理していくループ
			foreach(var word in _words){
				if(word == ""){
					continue;
				}
				stage_data [i, j] = int.Parse (word);
				
				j++;
				if (j > SCREEN_HEIGHT-1){
					break;
				}
			}

			j=0;
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

				// stage_data[i,j] = 1:赤, 2:青, 3:ブロック, 4:ニードル, 9:プレイヤー
				// 赤と青ブロックは目標残りブロックとしてカウントする
				switch(stage_data[i,j]){
		
				case 1:
					block_creator.createBlock (new Vector2 ((float)i, (float)j), RED);
					GameMgr.left_block++;
					break;
				case 2:
					block_creator.createBlock (new Vector2 ((float)i, (float)j), BLUE);
					GameMgr.left_block++;
					break;
				case 3:
					block_creator.createBlock (new Vector2 ((float)i, (float)j), METAL);
					break;
				case 4:
					block_creator.createBlock (new Vector2 ((float)i, (float)j), NEEDLE);
					break;
				case 9: 
					Instantiate (player [0], new Vector2 ((float)i, (float)j), Quaternion.Euler (new Vector2 (0f, 0f)));
					break;
				default:
					break;
				}
			}
		}
	}

	#endregion

}
