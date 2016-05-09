using UnityEngine;
using System.Collections;

public class BlockCreator : MonoBehaviour {

	#region private property

	[SerializeField]
	private GameObject[] blockPrefabs;  // ブロックを格納する配列
	private int block_count = 0;

	#endregion


	#region public method

	// ブロックを作成する関数
	// タイプを引数で指定 境界線としてのブロックかどうか指定
	public void createBlock2(Vector2 block_pos, int block_type, bool isWall){

		GameObject block_obj = Instantiate(blockPrefabs[block_type]) as GameObject;

		block_obj.GetComponent<BlockCtrl> ().block_type = block_type;
		block_obj.GetComponent<BlockCtrl> ().isWall     = isWall;
		block_obj.transform.position = block_pos;

		block_count++;
	}
		

	public void createBlock(Vector2 block_pos, int block_type){

		// ブロックを作成する
		GameObject block_obj = Instantiate(blockPrefabs[block_type]) as GameObject;
		// ブロックの種類を保存する
		block_obj.GetComponent<BlockCtrl> ().block_type = block_type;		

		block_obj.transform.position = block_pos;  // ブロックの位置を移動
		block_count++;
	}

	#endregion
}
