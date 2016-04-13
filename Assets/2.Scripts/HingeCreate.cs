#region Namespaces
using System.Collections;
using UnityEngine;
#endregion

public class HingeCreate : MonoBehaviour {
	static float DELAYED_TIME = 7f;

	[SerializeField]
	private GameObject follow_obj;	// ついていくオブジェクト

	private Transform _follow_transform;
	public Transform follow_transform
	{
		get { 
			_follow_transform = _follow_transform ?? (follow_obj.GetComponent<Transform>());
			return this._follow_transform; 
		}
	}
	private Transform _this_transform;
	public Transform this_transform
	{
		get {
			_this_transform = _this_transform ?? (this.GetComponent<Transform>());
			return this._this_transform; 
		}
	}

	// 移動アニメーションの関数
	private delegate float EasingFunction(float start, float end, float value);
	private EasingFunction ease;
	private float percentage;
	private Vector3[] vector3s = new Vector3[3];	//[0]:移動対象 [1]:目標 [2]:[0]と[1]のpercentageに応じた移動量

	// Use this for initialization
	void Start () {
		ease = new EasingFunction (easeInQuad);
		percentage = 0f;
		vector3s [0] = this_transform.position;
		vector3s [1] = follow_transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		vector3s[2].x = ease(vector3s[0].x, vector3s[1].x, percentage);
		vector3s[2].y = ease(vector3s[0].y, vector3s[1].y, percentage);
		vector3s[2].z = ease(vector3s[0].z, vector3s[1].z, percentage);

		this_transform.position = vector3s [2];

		if(percentage < 1f){
			percentage += Time.deltaTime * DELAYED_TIME;
		} else {
			vector3s [0] = this_transform.position;
			vector3s [1] = follow_transform.position;
			percentage = 0f;
		}

	}

	private float easeInQuad(float start, float end, float value){
		end -= start;
		return end * value * value + start;
	}
}
