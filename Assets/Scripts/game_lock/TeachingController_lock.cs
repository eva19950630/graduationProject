using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeachingController_lock : MonoBehaviour {
	public Animator teachAni;
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

/*3 5 6 12*/
		if (ques_id == "3") {
			teachAni.Play("ID_3");
			// idText.text = "happy";
		}
		else if (ques_id == "5") {
			teachAni.Play("ID_5");
		}
		else if (ques_id == "6") {
			teachAni.Play("ID_6");
		}
		else if (ques_id == "12") {
			teachAni.Play("ID_12");
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
		teachAni.enabled = true;

	}

	public void stopAni () {
		teachAni.enabled = false;
	}

	
}
