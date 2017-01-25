using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SecondText_fishing : MonoBehaviour {
	public Text sec;
	public GameObject secText, clock_ori, label2, wrongsymbol, feedbackPanel, finishBtn, resetBtn;
	//finishBtn;
	public Animator clockAni, feedbackAni;
	public float ti = 0;
	public int a = 0;
	public bool isboss = false;
	// Use this for initialization
	void Start () {
		// clock_alarm.SetActive(false);
		//timesupText.SetActive(false);
		//sec.color = Color.blue;
		if(GameObject.Find("BossSaveData"))
			isboss = true;
	}
	
	// Update is called once per frame
	void Update () {
		bool gamestate = GameController_fishing.gamestate;
		string ques_id = testbankDBHandler_fishing.ques_id;
		
		if(isboss){
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
					// clock_alarm.SetActive(true);
					feedbackPanel.SetActive(true);
					feedbackAni.Play("fishing_timesup");
					// feedbackText.color = Color.red;
					// feedbackText.text = "Time's Up !";
				}
				if (ti <= -92) {
					if (ques_id == "3" || ques_id == "5" || ques_id == "6" || ques_id == "12")
						SceneManager.LoadScene("aq_TeacherScene_fishing");
					else if (ques_id == "29" || ques_id == "32" || ques_id == "37" || ques_id == "45")
						SceneManager.LoadScene("aq_TeacherScene_fishing_2");
					else if (ques_id == "46" || ques_id == "47" || ques_id == "48" || ques_id == "66")
						SceneManager.LoadScene("aq_TeacherScene_fishing_3");
				}
			} else {
				if (ti <= -81)
					sec.text = "0" + ""+ a;
				else
					sec.text = ""+ a;
				clockAni.enabled = false;
			}
		}else{
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
					// clock_alarm.SetActive(true);
					feedbackPanel.SetActive(true);
					feedbackAni.Play("fishing_timesup");
					// feedbackText.color = Color.red;
					// feedbackText.text = "Time's Up !";
				}
				if (ti <= -182) {
					if (ques_id == "3" || ques_id == "5" || ques_id == "6" || ques_id == "12")
						SceneManager.LoadScene("aq_TeacherScene_fishing");
					else if (ques_id == "29" || ques_id == "32" || ques_id == "37" || ques_id == "45")
						SceneManager.LoadScene("aq_TeacherScene_fishing_2");
					else if (ques_id == "46" || ques_id == "47" || ques_id == "48" || ques_id == "66")
						SceneManager.LoadScene("aq_TeacherScene_fishing_3");
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
