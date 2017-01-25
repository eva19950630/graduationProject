using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeachingController_quickanswer : MonoBehaviour {
	public Animator teachAni;
	public Text idText;
	public GameObject continueBtn, watchagainBtn;

	private string wrongID, ques_id;
	private bool isWait = false;

	// Use this for initialization
	void Start () {
		wrongID = GameController_quickanswer.wrongID;
		ques_id = SecondText_quickanswer.ques_id;

		showQuesInfo ();
		// Debug.Log(wrongID);
		// idText.text = GameController_quickanswer.wrongQues;

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
		
		if (isWait) {
			if (wrongID == "3" || ques_id == "3")
				idText.text = "A - B + C";
			else if (wrongID == "5" || ques_id == "5")
				idText.text = "A + B + C + D";
			else if (wrongID == "6" || ques_id == "6")
				idText.text = "A + B + C - D";
			else if (wrongID == "12" || ques_id == "12")
				idText.text = "A - B - C - D";
			// idText.text = GameController_quickanswer.wrongQues;
			continueBtn.SetActive(false);
			watchagainBtn.SetActive(false);
			StartCoroutine(waitingSec_4s());
		}

	}

	void showQuesInfo () {
		isWait = true;

/*3 5 6 12*/
		if (wrongID == "3" || ques_id == "3") {
			// idText.text = "A - B + C";
			teachAni.Play("ID_3");
			print("3");
			// idText.text = "happy";
		}
		else if (wrongID == "5" || ques_id == "5") {
			// idText.text = "A + B + C + D";
			teachAni.Play("ID_5");
			print("5");
		}
		else if (wrongID == "6" || ques_id == "6") {
			// idText.text = "A + B + C - D";
			teachAni.Play("ID_6");
			print("6");
		}
		else if (wrongID == "12" || ques_id == "12") {
			// idText.text = "A - B - C - D";
			teachAni.Play("ID_12");
			print("12");
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
