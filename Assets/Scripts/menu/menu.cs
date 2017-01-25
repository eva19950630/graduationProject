using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {

	public int mode;
	// public Button Story;
	// public Button PK;
	public Animator flyaway;
	public AudioSource rocket_audio;

	// Use this for initialization
	void Start () {
		// Story.image.color = new Color(204, 204, 204);
		mode = 0;
		rocket_audio.Stop();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startGame(){
		rocket_audio.Play();
		flyaway.SetBool("gameStart",true);
	}

	public void exit(){
		Application.Quit();
	}

	public void go(){
		if(mode == 0)
			SceneManager.LoadScene("Story");
		// else
	}

	public void setMode(int modeNum){
		mode = modeNum;
	}
}
