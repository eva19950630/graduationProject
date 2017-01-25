using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SecondText_quickanswer : MonoBehaviour {
	public Text sec;
	public GameObject secText, label2, wrongsymbol, feedbackPanel, finishBtn;
	public float ti = 0;
	public int a = 0;
	public Animator clockAni, feedbackAni;

	public static string ques_id;
	public bool isboss = false;

	// Use this for initialization
	void Start () {
		string[] ques_id_arr = new string[12] {"3", "5", "6", "12", "29", "32", "37", "45", "46", "47", "48", "66"};
		int r = Random.Range(0, 12);
		ques_id = ques_id_arr[r];
		if(GameObject.Find("BossSaveData"))
			isboss = true;		
	}
	
	// Update is called once per frame
	void Update () {
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
					wrongsymbol.SetActive(false);
					secText.SetActive(false);
					label2.SetActive(false);
					finishBtn.SetActive(false);
					clockAni.enabled = false;
					
					feedbackPanel.SetActive(true);
					feedbackAni.Play("quickanswer_timesup");
					// feedbackText.color = Color.red;
					// feedbackText.text = "Time's Up !";
				}
				if (ti <= -62) {
					if (ques_id == "3" || ques_id == "5" || ques_id == "6" || ques_id == "12")
						SceneManager.LoadScene("num_TeacherScene_quickanswer");
					else if (ques_id == "29" || ques_id == "32" || ques_id == "37" || ques_id == "45")
						SceneManager.LoadScene("num_TeacherScene_quickanswer_2");
					else if (ques_id == "46" || ques_id == "47" || ques_id == "48" || ques_id == "66")
						SceneManager.LoadScene("num_TeacherScene_quickanswer_3");
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
					wrongsymbol.SetActive(false);
					secText.SetActive(false);
					label2.SetActive(false);
					finishBtn.SetActive(false);
					clockAni.enabled = false;
					
					feedbackPanel.SetActive(true);
					feedbackAni.Play("quickanswer_timesup");
					// feedbackText.color = Color.red;
					// feedbackText.text = "Time's Up !";
				}
				if (ti <= -102) {
					if (ques_id == "3" || ques_id == "5" || ques_id == "6" || ques_id == "12")
						SceneManager.LoadScene("num_TeacherScene_quickanswer");
					else if (ques_id == "29" || ques_id == "32" || ques_id == "37" || ques_id == "45")
						SceneManager.LoadScene("num_TeacherScene_quickanswer_2");
					else if (ques_id == "46" || ques_id == "47" || ques_id == "48" || ques_id == "66")
						SceneManager.LoadScene("num_TeacherScene_quickanswer_3");
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
