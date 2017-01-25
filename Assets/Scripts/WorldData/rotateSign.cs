using UnityEngine;
using System.Collections;

public class rotateSign : MonoBehaviour {

	public GameObject sign;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		sign.transform.Rotate(0f,100f * Time.deltaTime,0f);
	}
}
