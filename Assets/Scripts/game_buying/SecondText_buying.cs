using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondText_buying : MonoBehaviour {
	public Text sec;
	public GameObject secText, label2, wrongsymbol, feedbackPanel, finishBtn;
	public Animator clockAni;

	public float ti = 0;
	public int a = 0;

	private int gid;

	// Use this for initialization
	void Start () {
		gid = GameController_buying.gID;

	}
	
	// Update is called once per frame
	void Update () {
		ti -= Time.deltaTime;
		a = (int)ti + 180;
		sec.text = ""+ a;
		clockAni.enabled = true;
		if (ti <= -171) {
			sec.text = "0" + ""+ a;
			sec.color = Color.yellow;
			clockAni.Play ("clock_quick");
		}
		if (ti <= -180) {
			wrongsymbol.SetActive(false);
			secText.SetActive(false);
			label2.SetActive(false);
			finishBtn.SetActive(false);
			clockAni.enabled = false;
			
			// feedbackPanel.SetActive(true);
			// feedbackAni.Play("quickanswer_timesup");
		}
		if (ti <= -182) {
			
		}
	}
}
