using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class w_audio : MonoBehaviour {
	
	public AudioSource worldBGM;
	public static bool play_music;
	public Button music_sign;
	public Sprite sprite_musicOff;
	public Sprite sprite_musicOn;
	// public Animator music;
	// Use this for initialization
	void Start () {
		//worldBGM.Play();
		if(GameObject.Find("datasaver"))
			play_music = Map1_0.music_stat;
		else if(GameObject.Find("datasaverII"))
			play_music = Map1_1.music_stat;
		else if(GameObject.Find("datasaverIII"))
			play_music = Map1_2.music_stat;
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
			// play_music = true;
			worldBGM.Play();
		}else{
			// music.SetBool("play", false);
			// music_sign.GetComponent<SpriteRenderer>().sprite = sprite_musicOff;
			music_sign.image.overrideSprite = sprite_musicOff;
			// play_music = false;
			worldBGM.Stop();
		}
	}
}
