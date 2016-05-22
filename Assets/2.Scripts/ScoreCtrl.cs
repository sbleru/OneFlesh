using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreCtrl : MonoBehaviour {

	[SerializeField]
	private GameObject point;
	[SerializeField]
	private GameObject total_score;
	[SerializeField]
	private GameObject center_obj;

	#region public method

	// 引数のスコアを加点する
	public void AddScore(int score, Vector2 pos){
		GameObject point_obj = (Instantiate (point, pos, Quaternion.identity) as GameObject);
		point_obj.GetComponent<TextMesh> ().text = score.ToString();
		StartCoroutine (ScoreAbsorbAnim (score, point_obj));
	}

	// スコアを返す
	public int Return(){
		return GameManager.Instance.total_score;
	}

	#endregion


	#region private method

	//加点アニメーション　//合計スコアに向かって吸収されるイメージ
	IEnumerator ScoreAbsorbAnim(int score, GameObject point_obj){
		yield return new WaitForSeconds (1.0f);

		Vector3 toward_pos = center_obj.transform.position;
		toward_pos.x -= 20f;
		toward_pos.y += 12f;
		iTween.MoveTo(point_obj, iTween.Hash("position", toward_pos,
			"islocal", true,
			"time", 3
		));
		iTween.ScaleTo(point_obj, iTween.Hash("scale", new Vector3(0.1f,0.1f,0.1f),
			"time", 1
		));

		yield return new WaitForSeconds (1.0f);

		// 合計スコアに加算してテキストobjを削除
		GameManager.Instance.total_score += score;
		Destroy (point_obj);
	}

	#endregion
}
