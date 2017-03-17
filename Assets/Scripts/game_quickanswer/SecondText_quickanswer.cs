using UnityEngine;
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
	public bool isTimesup = false;

	public static string ques_id;
	public static bool quick_isTimesup;

	private int gid;

/*Pass data*/
	public float ti2 = 0;
	public int answertime = 0;

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

		if (gamestate == true) {
			ti2 += Time.deltaTime;
			answertime = (int)ti2;
			// print(answertime);
		}

		if(isboss){
			if (gamestate == true) {
				ti -= Time.deltaTime;
				a = (int)ti + 90;
				sec.text = ""+ a;
				clockAni.enabled = true;
				if (ti <= -81) {
					sec.text = "0" + ""+ a;
					sec.color = Color.red;
					clockAni.Play ("clock_quick");
				}
				if (ti <= -90) {
					isTimesup = true;
					quick_isTimesup = true;
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
				if (ti <= -90.03f)
					isTimesup = false;
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
		}else{
			if (gamestate == true) {
				ti -= Time.deltaTime;
				a = (int)ti + 180;
				sec.text = ""+ a;
				clockAni.enabled = true;
				if (ti <= -171) {
					sec.text = "0" + ""+ a;
					sec.color = Color.red;
					clockAni.Play ("clock_quick");
				}
				if (ti <= -180) {
					isTimesup = true;			
					quick_isTimesup = true;

					wrongsymbol.SetActive(false);
					secText.SetActive(false);
					label2.SetActive(false);
					finishBtn.SetActive(false);
					clockAni.enabled = false;
					feedbackPanel.SetActive(true);
					feedbackAni.Play("quickanswer_timesup");
				}
				if (ti <= -180.03f)
					isTimesup = false;
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
