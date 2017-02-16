using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondText_balance : MonoBehaviour {
	public Text sec;
	public GameObject secText, label2, wrongsymbol, feedbackPanel, finishBtn;
	public float ti = 0;
	public int a = 0;
	public Animator clockAni;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		bool gamestate = GameController_balance.gamestate;

		if (gamestate == true) {
			ti -= Time.deltaTime;
			a = (int)ti + 150;
			sec.text = ""+ a;
			clockAni.enabled = true;
			if (ti <= -141) {
				sec.text = "0" + ""+ a;
				sec.color = Color.red;
				clockAni.Play ("clock_quick");
			}
			if (ti <= -150) {
				wrongsymbol.SetActive(false);
				secText.SetActive(false);
				label2.SetActive(false);
				finishBtn.SetActive(false);
				clockAni.enabled = false;
				
				// feedbackPanel.SetActive(true);
				// feedbackAni.Play("quickanswer_timesup");
			}
			if (ti <= -152) {
				
			}
		} else {
			if (ti <= -141)
				sec.text = "0" + ""+ a;
			else
				sec.text = ""+ a;
			
			clockAni.enabled = false;
		}
	}
}
