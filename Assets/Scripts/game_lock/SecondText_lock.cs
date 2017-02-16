using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SecondText_lock : MonoBehaviour {
	public Text sec;
	public GameObject secText, finishBtn, bomb, wrongsymbol, feedbackPanel, label2;
	public Animator bombAni, feedbackAni;
	public float ti = 0;
	public int a = 0;
	public bool isboss = false;

	// Use this for initialization
	void Start () {
		sec.color = Color.yellow;
		if(GameObject.Find("BossSaveData"))
			isboss = true;
	}
	
	// Update is called once per frame
	void Update () {
		bool gamestate = GameController_lock.gamestate;
		string ques_id = testbankDBHandler_lock.ques_id;

		if (isboss) {
			if (gamestate == true) {
				ti -= Time.deltaTime;
				a = (int)ti + 60;
				sec.text = ""+ a;
				bombAni.enabled = true;
				if (ti <= -51) {
					sec.text = "0" + ""+ a;
					bombAni.Play ("bomb_quick");
				}
				if (ti <= -60) {
					bombAni.enabled = false;
					wrongsymbol.SetActive(false);
					bomb.SetActive(false);
					finishBtn.SetActive(false);
					secText.SetActive(false);
					label2.SetActive(false);
					// bomb_bomb.SetActive(true);
					feedbackPanel.SetActive(true);
					feedbackAni.Play("lock_timesup");
					// feedbackText.color = Color.red;
					// feedbackText.text = "Time's Up !";
				}
				if (ti <= -62) {
					if (ques_id == "3" || ques_id == "5" || ques_id == "6" || ques_id == "12")
						SceneManager.LoadScene("TeacherScene_lock");
					else if (ques_id == "29" || ques_id == "32" || ques_id == "37" || ques_id == "45")
						SceneManager.LoadScene("TeacherScene_lock_2");
					else if (ques_id == "46" || ques_id == "47" || ques_id == "48" || ques_id == "66")
						SceneManager.LoadScene("TeacherScene_lock_3");
				}
			} else {
				if (ti <= -51)
					sec.text = "0" + ""+ a;
				else
					sec.text = ""+ a;
				
				bombAni.enabled = false;
			}
		} else {
			if (gamestate == true) {
				ti -= Time.deltaTime;
				a = (int)ti + 120;
				sec.text = ""+ a;
				bombAni.enabled = true;
				if (ti <= -111) {
					sec.text = "0" + ""+ a;
					bombAni.Play ("bomb_quick");
				}
				if (ti <= -120) {
					bombAni.enabled = false;
					wrongsymbol.SetActive(false);
					bomb.SetActive(false);
					finishBtn.SetActive(false);
					secText.SetActive(false);
					label2.SetActive(false);
					// bomb_bomb.SetActive(true);
					feedbackPanel.SetActive(true);
					feedbackAni.Play("lock_timesup");
					// feedbackText.color = Color.red;
					// feedbackText.text = "Time's Up !";
				}
				if (ti <= -122) {
					if (ques_id == "3" || ques_id == "5" || ques_id == "6" || ques_id == "12")
						SceneManager.LoadScene("TeacherScene_lock");
					else if (ques_id == "29" || ques_id == "32" || ques_id == "37" || ques_id == "45")
						SceneManager.LoadScene("TeacherScene_lock_2");
					else if (ques_id == "46" || ques_id == "47" || ques_id == "48" || ques_id == "66")
						SceneManager.LoadScene("TeacherScene_lock_3");
				}
			} else {
				if (ti <= -111)
					sec.text = "0" + ""+ a;
				else
					sec.text = ""+ a;
				
				bombAni.enabled = false;
			}
		}


	}
}
