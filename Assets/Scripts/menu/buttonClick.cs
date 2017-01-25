using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class buttonClick : MonoBehaviour {

	public Button story;
	public Button pk;

	// Use this for initialization
	void Start () {
		story.image.color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void storyClick(){
		story.image.color = Color.white;
		pk.image.color = Color.gray;
	}

	public void PkClick(){
		pk.image.color = Color.white;
		story.image.color = Color.gray;
	}
}
