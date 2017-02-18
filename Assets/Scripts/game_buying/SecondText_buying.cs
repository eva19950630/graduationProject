﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondText_buying : MonoBehaviour {
	public Text sec;
	public GameObject secText, label2, wrongsymbol, feedbackPanel, finishBtn;
	public Animator clockAni, feedbackAni;

	public float ti = 0;
	public int a = 0;
	public bool isboss = false;

	private int gid;

	// Use this for initialization
	void Start () {
		if(GameObject.Find("BossSaveData"))
			isboss = true;
	}
	
	// Update is called once per frame
	void Update () {
		gid = GameController_buying.gID;
		bool gamestate = GameController_buying.gamestate;
		
		if (isboss) {
			if (gamestate == true) {
				ti -= Time.deltaTime;
				a = (int)ti + 150;
				sec.text = ""+ a;
				clockAni.enabled = true;
				if (ti <= -141) {
					sec.text = "0" + ""+ a;
					sec.color = Color.yellow;
					clockAni.Play ("clock_quick");
				}
				if (ti <= -150) {
					wrongsymbol.SetActive(false);
					secText.SetActive(false);
					label2.SetActive(false);
					finishBtn.SetActive(false);
					clockAni.enabled = false;
					feedbackPanel.SetActive(true);
					feedbackAni.Play("fishing_timesup");
				}
				if (ti <= -152) {
					game_mechanism.enterTeaching(gid);
				}
			} else {
				if (ti <= -141)
					sec.text = "0" + ""+ a;
				else
					sec.text = ""+ a;
				
				clockAni.enabled = false;
			}
		} else {
			if (gamestate == true) {
				ti -= Time.deltaTime;
				a = (int)ti + 300;
				sec.text = ""+ a;
				clockAni.enabled = true;
				if (ti <= -291) {
					sec.text = "0" + ""+ a;
					sec.color = Color.yellow;
					clockAni.Play ("clock_quick");
				}
				if (ti <= -300) {
					wrongsymbol.SetActive(false);
					secText.SetActive(false);
					label2.SetActive(false);
					finishBtn.SetActive(false);
					clockAni.enabled = false;
					feedbackPanel.SetActive(true);
					feedbackAni.Play("fishing_timesup");
				}
				if (ti <= -302) {
					game_mechanism.enterTeaching(gid);
				}
			} else {
				if (ti <= -291)
					sec.text = "0" + ""+ a;
				else
					sec.text = ""+ a;
				
				clockAni.enabled = false;
			}
		}

	}
}