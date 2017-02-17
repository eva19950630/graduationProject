using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SecondText_fishing : MonoBehaviour {
	public Text sec;
	public GameObject secText, clock_ori, label2, wrongsymbol, feedbackPanel, finishBtn, resetBtn;
	public Animator clockAni, feedbackAni;

	public float ti = 0;
	public int a = 0;
	public bool isboss = false;
	
	private int gid;

	// Use this for initialization
	void Start () {
		gid = GameController_fishing.gID;

		if(GameObject.Find("BossSaveData"))
			isboss = true;
	}
	
	// Update is called once per frame
	void Update () {
		bool gamestate = GameController_fishing.gamestate;
		
		if (isboss) {
			if (gamestate == true) {
				ti -= Time.deltaTime;
				a = (int)ti + 90;
				sec.text = ""+ a;
				clockAni.enabled = true;
				if (ti <= -81) {
					sec.text = "0" + ""+ a;
					sec.color = Color.yellow;
					clockAni.Play ("clock_quick");
				}
				if (ti <= -90) {
					clockAni.enabled = false;
					clock_ori.SetActive(false);
					wrongsymbol.SetActive(false);
					secText.SetActive(false);
					label2.SetActive(false);
					finishBtn.SetActive(false);
					resetBtn.SetActive(false);
					feedbackPanel.SetActive(true);
					feedbackAni.Play("fishing_timesup");
				}
				if (ti <= -92) {
					game_mechanism.enterTeaching(gid);
				}
			} else {
				if (ti <= -81)
					sec.text = "0" + ""+ a;
				else
					sec.text = ""+ a;
				clockAni.enabled = false;
			}
		} else {
			if (gamestate == true) {
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
					clockAni.enabled = false;
					clock_ori.SetActive(false);
					wrongsymbol.SetActive(false);
					secText.SetActive(false);
					label2.SetActive(false);
					finishBtn.SetActive(false);
					resetBtn.SetActive(false);
					feedbackPanel.SetActive(true);
					feedbackAni.Play("fishing_timesup");
				}
				if (ti <= -182) {
					game_mechanism.enterTeaching(gid);
				}
			} else {
				if (ti <= -171)
					sec.text = "0" + ""+ a;
				else
					sec.text = ""+ a;
				clockAni.enabled = false;
			}
		}


		
	}
}
