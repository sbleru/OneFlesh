using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeManager : MonoBehaviour {

	#region private propety

	[SerializeField]
	private Text time;
	private float elapsed_secs; // クリアまでの経過時間
	string text;

	#endregion


	#region event

	void Start(){
		elapsed_secs = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		elapsed_secs += Time.deltaTime;
		//表示用のカウント
		//小数第2位まで
		elapsed_secs = Mathf.CeilToInt (elapsed_secs * 100);
		elapsed_secs /= 100;
		text = elapsed_secs.ToString ();
		time.color = new Color (1, 1, 1, 1);
		// 残り時間を更新
		time.text = text;

	}

	#endregion


	#region public method

	public void StartGame(){
		enabled = true;
	}

	public void GameClear(){
		enabled = false;
		GameManager.Instance.time_score = elapsed_secs;
	}

	#endregion

}
