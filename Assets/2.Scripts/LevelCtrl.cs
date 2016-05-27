using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class LevelData{

	public int score { get; set; } 			// スコア
	public float player_speed { get; set; }	// プレイヤーの速度
	public float interval { get; set; }		// ブロック出現間隔

	// コンストラクタ
	public LevelData(){
		this.score = 0;
		this.player_speed = 5.0f;
		this.interval = 18;
	}
}

public class LevelCtrl : MonoBehaviour {

	#region private property

	private List<LevelData> level_datas = new List<LevelData> ();
	private int level = 0;  // 難易度

	#endregion


	#region public method

	// 他スクリプトでLevelCtrlクラスを扱う時に使う初期化関数
	public void initialize(){
		this.level = 0;
	}

	// テキストデータを読み込んで解析
	public void loadLevelData(TextAsset level_data_txt){
		// テキストデータを、文字列として取り込む
		string level_txts = level_data_txt.text;

		// 改行コード'\n'ごとに分割し、文字列の配列に入れる
		string[] lines = level_txts.Split('\n');

		// lines内の各行に対して、順に処理していくループ
		foreach(var line in lines){
			if(line == ""){	// 行が空っぽなら
				continue;	// ループの先頭にジャンプ
			}
		
			string[] words = line.Split ();	// 行内のワードを配列に格納
			int n = 0;

			// LevelData型の変数を作成
			// ここに、現在処理している行のデータを入れていく
			LevelData level_data = new LevelData ();

			// words内の各ワードに対して、順に処理していくループ
			foreach(var word in words){
				if(word.StartsWith("#")){	// ワードの先頭文字が#なら
					break;
				}
				if(word == ""){
					continue;
				}

				// nの値で各項目を処理
				switch(n){
				case 0:
					level_data.score = int.Parse (word);
					break;
				case 1:
					level_data.player_speed = float.Parse (word);
					break;
				case 2:
					level_data.interval = float.Parse (word);
					break;
				default:
					break;
				}
				n++;
			}

			if(n >= 3){	// 3項目が処理されたなら
				// List構造のlevel_datasにlevel_dataを追加
				this.level_datas.Add (level_data);
			} else {	// エラーの可能性あり
				if(n==0){	// 1ワードも処理していない場合はコメント
					//何もしない
				} else {
					// データの個数があっていないことを示すエラーメッセージ
					Debug.LogError ("[LevelData] Out of parameter.\n");
				}
			}
		}

		// level_datasにデータが一つもないならば
		if(this.level_datas.Count == 0){
			// エラーメッセージ
			Debug.LogError ("[LevelData] Has no data.\n");
			// level_datasに、デフォルトのLevelDataを追加
			this.level_datas.Add (new LevelData ());
		}
	}

	// レベルを更新
	public float getPlayerInterval(int now_score){
		// 現在のレベルを求める
		int i;
		for(i=0; i<this.level_datas.Count-1; i++){
			if(now_score <= level_datas[i].score){
				break;
			}
		}
		this.level = i;

		return this.level_datas [this.level].interval;
	}

	//
	public float getPlayerSpeed(){
		return this.level_datas [this.level].player_speed;
	}

	#endregion
}
