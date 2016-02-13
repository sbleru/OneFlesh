using UnityEngine;
using System.Collections;

public class BlockCreator : MonoBehaviour {
	public GameObject[] blockPrefabs;  // ブロックを格納する配列
	private int block_count = 0;

	// ブロックを作成する関数
	public void createBlock(Vector3 block_pos){
		// 作成するブロックを決める
		int next_block_type = block_count % blockPrefabs.Length;

		// ブロックを作成する
		GameObject block_obj = Instantiate(blockPrefabs[next_block_type]) as GameObject;
		// ブロックの種類を保存する
		block_obj.GetComponent<BlockCtrl> ().block_type = next_block_type;

		block_obj.transform.position = block_pos;  // ブロックの位置を移動
		block_count++;
	}
}
