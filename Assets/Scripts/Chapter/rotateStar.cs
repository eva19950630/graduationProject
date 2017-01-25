using UnityEngine;
using System.Collections;

public class rotateStar : MonoBehaviour {

	public GameObject star;
	public float degree;
	public float speed; 
	public bool wait;
	// Use this for initialization
	void Start () {
		degree = 0f;
		speed = 0.25f;
		wait = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(wait){
			StartCoroutine(WAIT());
			wait = false;
		}
	}

	IEnumerator WAIT(){
		yield return new WaitForSeconds(0.5f);
		degree+=45f;

		Vector3 temp = transform.rotation.eulerAngles;
		temp.z = degree;
		star.transform.rotation = Quaternion.Euler(temp);
		wait = true;
	}

}
