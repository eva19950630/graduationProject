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
	public GameObject StartButton;
	public GameObject history;
	public bool openHistory = false;
	// Use this for initialization
	void Start () {
		// Story.image.color = new Color(204, 204, 204);
		history = GameObject.Find("course");
		history.SetActive(false);
		mode = 0;
		rocket_audio.Stop();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startGame(){
		StartButton.SetActive(false);
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

	public void clickHistory(){
		if(!openHistory)
			history.SetActive(true);
		else
			history.SetActive(false);

		openHistory = !openHistory;
	}
}
