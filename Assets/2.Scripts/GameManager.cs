using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
//	static public string game_mode;
//	static public int stage_num;
//	static public int left_block;
//	static public float time_score;
//	static public bool isRetire;	// ゲームリタイアかどうか
//	static public int total_score;
//	static public int count_for_ads;	// Adsをどれくらいの頻度で表示するか
//	
//	//
//	static public void initialize(){
//		game_mode = "TimeAttack";
//		stage_num = 1;
//		left_block = 0;
//		isRetire = false;
//		total_score = 0;
//		count_for_ads = 3;
//	}

	#region Singleton
	private static GameManager instance;

	public static GameManager Instance {
		get {
			if (instance == null) {
				instance = (GameManager)FindObjectOfType (typeof(GameManager));

				if (instance == null) {
					Debug.LogError (typeof(GameManager) + "is nothing");
				}
			}

			return instance;
		}
	}
	#endregion Singleton


	#region public property

	[SerializeField]
	private string _game_mode;
	public string game_mode
	{
		get { return this._game_mode; }
		set { this._game_mode = value; }
	}

	[SerializeField]
	private int _stage_num;
	public int stage_num
	{
		get { return this._stage_num; }
		set { this._stage_num = value; }
	}

	[SerializeField]
	private int _left_block;
	public int left_block
	{
		get { return this._left_block; }
		set { this._left_block = value; }
	}

	[SerializeField]
	private float _time_score;
	public float time_score
	{
		get { return this._time_score; }
		set { this._time_score = value; }
	}

	[SerializeField]
	private bool _isRetire;
	public bool isRetire
	{
		get { return this._isRetire; }
		set { this._isRetire = value; }
	}

	[SerializeField]
	private int _total_score;
	public int total_score
	{
		get { return this._total_score; }
		set { this._total_score = value; }
	}

	[SerializeField]
	private int _count_for_ads;
	public int count_for_ads
	{
		get { return this._count_for_ads; }
		set { this._count_for_ads = value; }
	}

	#endregion


	#region event

	// Use this for initialization
	void Awake () {

		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}

		DontDestroyOnLoad (this.gameObject);
	}

	#endregion
}
