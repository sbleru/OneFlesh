using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// シングルトン化してサウンドの管理を任せる
public class SoundManager : MonoBehaviour {

	#region Singleton
	private static SoundManager instance;

	public static SoundManager Instance {
		get {
			if (instance == null) {
				instance = (SoundManager)FindObjectOfType (typeof(SoundManager));

				if (instance == null) {
					Debug.LogError (typeof(SoundManager) + "is nothing");
				}
			}

			return instance;
		}
	}
	#endregion Singleton


	#region public property

	[SerializeField]
	private AudioClip _bgm_title;
	public AudioClip bgm_title
	{
		get { return this._bgm_title; }
		private set { this._bgm_title = value; }
	}
	[SerializeField]
	private AudioClip _bgm_play;
	public AudioClip bgm_play
	{
		get { return this._bgm_play; }
		private set { this._bgm_play = value; }
	}
	[SerializeField]
	private AudioClip _sound_explosion;
	public AudioClip sound_explosion
	{
		get { return this._sound_explosion; }
		private set { this._sound_explosion = value; }
	}
	[SerializeField]
	private AudioClip _sound_vanish;
	public AudioClip sound_vanish
	{
		get { return this._sound_vanish; }
		private set { this._sound_vanish = value; }
	}
	[SerializeField]
	private AudioClip _sound_stamp;
	public AudioClip sound_stamp
	{
		get { return this._sound_stamp; }
		private set { this._sound_stamp = value; }
	}
	[SerializeField]
	private AudioClip _sound_retire;
	public AudioClip sound_retire
	{
		get { return this._sound_retire; }
		private set { this._sound_retire = value; }
	}
	#endregion


	#region private property

	private AudioSource audioSource_bgm;
	private AudioSource audioSource_effect;

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
		
	void Start () {
		//AudioSourceコンポーネントを取得し、変数に格納
		AudioSource[] audioSources = GetComponents<AudioSource>();
		audioSource_bgm    = audioSources[0];
		audioSource_effect = audioSources[1];
	}

	#endregion


	#region public method

	// Singletonインスタンスを取得する
	public static SoundManager GetInstance() {
		return instance;
	}

	// ゲームプレイ中のbgmを呼び出す
	public void PlayBGM() {
		audioSource_bgm.clip = bgm_play;
		audioSource_bgm.priority = 128;
		audioSource_bgm.volume = 0.1f;
		audioSource_bgm.pitch = 1.5f;
		audioSource_bgm.Play();
	}

	// ゲームプレイ以外のbgm
	public void PlayTitleBGM(){
		audioSource_bgm.clip = bgm_title;
		audioSource_bgm.priority = 128;
		audioSource_bgm.volume = 0.1f;
		audioSource_bgm.pitch = 1.3f;
		audioSource_bgm.Play ();
	}

	// 効果音を呼び出す
	public void PlaySoundEffect(AudioClip clip) {
		audioSource_effect.PlayOneShot(clip);
	}

	// 指定した時間遅らせて指定されたクリップを再生する
	public void PlaySoundEffect(AudioClip clip, float time_secs) {
		StartCoroutine (delayPlay (clip, time_secs));
	}
		
	// 指定されたクリップを再生する
	public static void Play(AudioClip clip) {
		GetInstance().PlaySoundEffect(clip);
	}

	#endregion


	#region private method

	IEnumerator delayPlay(AudioClip clip, float time_secs){
		yield return new WaitForSeconds (time_secs);
		GetInstance().PlaySoundEffect(clip);
	}

	#endregion
}