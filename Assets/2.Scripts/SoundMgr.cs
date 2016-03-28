using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundMgr : MonoBehaviour {
	private static SoundMgr instance;
	private AudioSource audioSource;

	// Use this for initialization
	void Awake () {
		instance = this;
		audioSource = GetComponent<AudioSource>();
	}
		

	// Singletonインスタンスを取得する
	public static SoundMgr GetInstance() {
		return instance;
	}

	// 指定されたクリップを再生する
	public void PlayClip(AudioClip clip) {
		audioSource.PlayOneShot(clip);
	}

	// 指定した時間遅らせて指定されたクリップを再生する
	public void PlayClip(AudioClip clip, float time) {
		StartCoroutine (delayPlay (clip, time));
	}

	// 指定されたクリップを再生する
	public static void Play(AudioClip clip) {
		GetInstance().PlayClip(clip);
	}

	IEnumerator delayPlay(AudioClip clip, float time){
		yield return new WaitForSeconds (time);
		GetInstance().PlayClip(clip);
	}
}