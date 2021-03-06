﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bgmusic : MonoBehaviour {

	public AudioSource worldBGM;
	public static bool play_music;
	public Button music_sign;
	public Sprite sprite_musicOff;
	public Sprite sprite_musicOn;

	// Use this for initialization
	void Start () {
		play_music = true;
		audio_play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void on_off_music(){
		if(play_music)
			play_music = false;
		else
			play_music = true;
			
		audio_play();	
	}

	public void audio_play(){
		if(play_music){
			// music.SetBool("play", true);
			// music_sign.GetComponent<SpriteRenderer>().sprite = sprite_musicOn;
			music_sign.image.overrideSprite = sprite_musicOn;
			play_music = true;
			worldBGM.Play();
		}else{
			// music.SetBool("play", false);
			// music_sign.GetComponent<SpriteRenderer>().sprite = sprite_musicOff;
			music_sign.image.overrideSprite = sprite_musicOff;
			play_music = false;
			worldBGM.Stop();
		}
	}
}
