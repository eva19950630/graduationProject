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
	public bool isTimesup = false;

	private int gid;

/*Pass data*/
	public float ti2 = 0;
	public int answertime = 0;

	// Use this for initialization
	void Start () {
		sec.color = Color.yellow;
		
		if(GameObject.Find("BossSaveData"))
			isboss = true;
	}
	
	// Update is called once per frame
	void Update () {
		gid = GameController_lock.gID;
		bool gamestate = GameController_lock.gamestate;

		if (gamestate == true) {
			ti2 += Time.deltaTime;
			answertime = (int)ti2;
		}

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
					isTimesup = true;
					BossSave.timesup();
					BossSave.setDamage();

					bombAni.enabled = false;
					wrongsymbol.SetActive(false);
					bomb.SetActive(false);
					finishBtn.SetActive(false);
					secText.SetActive(false);
					label2.SetActive(false);
					feedbackPanel.SetActive(true);
					feedbackAni.Play("lock_timesup");
				}
				if (ti <= -60.03f)
					isTimesup = false;
				if (ti <= -62) {
					// BossSave.timesup();
					game_mechanism.enterTeaching(gid);
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
					isTimesup = true;

					bombAni.enabled = false;
					wrongsymbol.SetActive(false);
					bomb.SetActive(false);
					finishBtn.SetActive(false);
					secText.SetActive(false);
					label2.SetActive(false);
					feedbackPanel.SetActive(true);
					feedbackAni.Play("lock_timesup");
				}
				if (ti <= -120.03f)
					isTimesup = false;
				if (ti <= -122) {
					game_mechanism.enterTeaching(gid);
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
