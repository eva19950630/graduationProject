using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class game_audio : MonoBehaviour {
	public AudioSource gameBGM, wrongSound, rightSound;
	// public Animator bgmAni;
	public Button gameBgmBtn;
	public Sprite[] bgmSprite = new Sprite[2];
	
	private bool isPlay = true;


	// Use this for initialization
	void Start () {
		wrongSound.Stop();
		rightSound.Stop();	
		// bgmAni.Play("audio_run");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void gameBGM_on_off () {
		if (isPlay) {
			// bgmAni.SetBool("play", false);
			isPlay = false;
			gameBGM.Stop();
			gameBgmBtn.image.overrideSprite = bgmSprite[0];
		} else {
			// bgmAni.SetBool("play", true);
			isPlay = true;
			gameBGM.Play();
			gameBgmBtn.image.overrideSprite = bgmSprite[1];
		}
	}
	
	public void playWrongSound () {
		if(!(GameController_lock.isRight || GameController_fishing.isRight || GameController_quickanswer.isRight ||
			 GameController_balance.isRight))
			wrongSound.Play();
		else
			rightSound.Play();

		// if(GameController_quickanswer.isAnsFiveTimes)
		// 	rightSound.Play();
	}


}
