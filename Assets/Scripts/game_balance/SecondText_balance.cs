using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondText_balance : MonoBehaviour {
	public Text sec;
	public GameObject secText, label2, wrongsymbol, feedbackPanel, finishBtn;
	public Animator clockAni, feedbackAni;
	
	public float ti = 0;
	public int a = 0;
	public bool isboss = false;
	public bool isTimesup = false;

	private int gid;

/*Pass data*/
	public float ti2 = 0;
	public int answertime = 0;

	// Use this for initialization
	void Start () {
		if(GameObject.Find("BossSaveData"))
			isboss = true;
	}
	
	// Update is called once per frame
	void Update () {
		gid = GameController_balance.gID;
		bool gamestate = GameController_balance.gamestate;

		if (gamestate == true) {
			ti2 += Time.deltaTime;
			answertime = (int)ti2;
			// print(answertime);
		}

		if (isboss) {
			if (gamestate == true) {
				ti -= Time.deltaTime;
				a = (int)ti + 75;
				sec.text = ""+ a;
				clockAni.enabled = true;
				if (ti <= -66) {
					sec.text = "0" + ""+ a;
					sec.color = Color.red;
					clockAni.Play ("clock_quick");
				}
				if (ti <= -75) {
					isTimesup = true;
					BossSave.timesup();
					BossSave.setDamage();

					wrongsymbol.SetActive(false);
					secText.SetActive(false);
					label2.SetActive(false);
					finishBtn.SetActive(false);
					clockAni.enabled = false;
					feedbackPanel.SetActive(true);
					feedbackAni.Play("quickanswer_timesup");
				}
				if (ti <= -75.03f)
					isTimesup = false;
				if (ti <= -77) {
					game_mechanism.enterTeaching(gid);
				}
			} else {
				if (ti <= -66)
					sec.text = "0" + ""+ a;
				else
					sec.text = ""+ a;
				
				clockAni.enabled = false;
			}
		} else {
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
					isTimesup = true;

					wrongsymbol.SetActive(false);
					secText.SetActive(false);
					label2.SetActive(false);
					finishBtn.SetActive(false);
					clockAni.enabled = false;
					feedbackPanel.SetActive(true);
					feedbackAni.Play("quickanswer_timesup");
				}
				if (ti <= -150.03f)
					isTimesup = false;
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
		}

	}
}
