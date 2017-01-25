using UnityEngine;
using System.Collections;

public class infinitybg : MonoBehaviour {

	private float speed;

	// Use this for initialization
	void Start () {
		speed = 0.001f;
	}
	
	// Update is called once per frame
	void Update () {
		var x = GetComponent<Renderer>().material.mainTextureOffset.x;
		GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(x-speed, 0));
	}
}
