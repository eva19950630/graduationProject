﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SecondText_quickanswer : MonoBehaviour {
	public Text sec;
	public GameObject secText, label2, wrongsymbol, feedbackPanel, finishBtn;
	public Animator clockAni, feedbackAni;

	public float ti = 0;
	public int a = 0;
	public bool isboss = false;

	public static string ques_id;
	public static bool quick_isTimesup;

	private int gid;

	// Use this for initialization
	void Start () {
		quick_isTimesup = false;

		string[] ques_id_arr = new string[12] {"3", "5", "6", "12", "29", "32", "37", "45", "46", "47", "48", "66"};
		int r = Random.Range(0, 12);
		ques_id = ques_id_arr[r];

		if(GameObject.Find("BossSaveData"))
			isboss = true;		
	}
	
	// Update is called once per frame
	void Update () {
		gid = GameController_quickanswer.gID;
		// print(gid);

		bool gamestate = GameController_quickanswer.gamestate;

		if(isboss){
			if (gamestate == true) {
				ti -= Time.deltaTime;
				a = (int)ti + 60;
				sec.text = ""+ a;
				clockAni.enabled = true;
				if (ti <= -51) {
					sec.text = "0" + ""+ a;
					sec.color = Color.red;
					clockAni.Play ("clock_quick");
				}
				if (ti <= -60) {
					quick_isTimesup = true;

					wrongsymbol.SetActive(false);
					secText.SetActive(false);
					label2.SetActive(false);
					finishBtn.SetActive(false);
					clockAni.enabled = false;				
					feedbackPanel.SetActive(true);
					feedbackAni.Play("quickanswer_timesup");
				}
				if (ti <= -62) {
					game_mechanism.enterTeaching(gid);
				}
			} else {
				if (ti <= -51)
					sec.text = "0" + ""+ a;
				else
					sec.text = ""+ a;
				
				clockAni.enabled = false;
			}
		}else{
			if (gamestate == true) {
				ti -= Time.deltaTime;
				a = (int)ti + 100;
				sec.text = ""+ a;
				clockAni.enabled = true;
				if (ti <= -91) {
					sec.text = "0" + ""+ a;
					sec.color = Color.red;
					clockAni.Play ("clock_quick");
				}
				if (ti <= -100) {				
					quick_isTimesup = true;

					wrongsymbol.SetActive(false);
					secText.SetActive(false);
					label2.SetActive(false);
					finishBtn.SetActive(false);
					clockAni.enabled = false;
					feedbackPanel.SetActive(true);
					feedbackAni.Play("quickanswer_timesup");
				}
				if (ti <= -102) {
					game_mechanism.enterTeaching(gid);
				}
			} else {
				if (ti <= -91)
					sec.text = "0" + ""+ a;
				else
					sec.text = ""+ a;
				
				clockAni.enabled = false;
			}
		}
		
	}
}
