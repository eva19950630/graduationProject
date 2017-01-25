using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeachingController_lock_2 : MonoBehaviour {
	public Animator teachAni2;
	public Text idText;
	public GameObject continueBtn, watchagainBtn;

	private string ques_id;
	private bool isWait = false;

	// Use this for initialization
	void Start () {
		ques_id = testbankDBHandler_lock.ques_id;

		showQuesInfo ();
		// Debug.Log(ques_id);
		idText.text = testbankDBHandler_lock.questions;

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
		
		if (isWait) {
			idText.text = testbankDBHandler_lock.questions;
			continueBtn.SetActive(false);
			watchagainBtn.SetActive(false);
			StartCoroutine(waitingSec_4s());
		}

	}

	void showQuesInfo () {
		isWait = true;

/*29 32 37 45*/
		if (ques_id == "29") {
			teachAni2.Play("ID_29");
		}
		else if (ques_id == "32") {
			teachAni2.Play("ID_32");
		}
		else if (ques_id == "37") {
			teachAni2.Play("ID_37");
		}
		else if (ques_id == "45") {
			teachAni2.Play("ID_45");
		}
		

	}

	IEnumerator waitingSec_4s () {
		yield return new WaitForSeconds(4f);
		idText.text = "";
		isWait = false;
	}

	public void showWatchingAgainBtn () {
		watchagainBtn.SetActive(true);
	}

	public void showcontinueBtn () {
		continueBtn.SetActive(true);
	}


	public void cilckShowWatchingAgain () {
		isWait = true; 
		teachAni2.enabled = true;

	}

	public void stopAni () {
		teachAni2.enabled = false;
	}
}
