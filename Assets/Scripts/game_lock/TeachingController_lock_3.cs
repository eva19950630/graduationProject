using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeachingController_lock_3 : MonoBehaviour {

	public Animator teachAni3;
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

/*46 47 48 66*/
		if (ques_id == "46") {
			teachAni3.Play("ID_46");
		}
		else if (ques_id == "47") {
			teachAni3.Play("ID_47");
		}
		else if (ques_id == "48") {
			teachAni3.Play("ID_48");
		}
		else if (ques_id == "66") {
			teachAni3.Play("ID_66");
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
		teachAni3.enabled = true;

	}

	public void stopAni () {
		teachAni3.enabled = false;
	}
}
