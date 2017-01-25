using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeachingController_quickanswer_2 : MonoBehaviour {

	public Animator teachAni2;
	public Text idText;
	public GameObject continueBtn, watchagainBtn;

	private string wrongID, ques_id;
	private bool isWait = false;

	// Use this for initialization
	void Start () {
		wrongID = GameController_quickanswer.wrongID;
		ques_id = SecondText_quickanswer.ques_id;

		showQuesInfo ();
		// Debug.Log(ques_id);
		// idText.text = GameController_quickanswer.wrongQues;

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
		
		if (isWait) {
			if (wrongID == "29" || ques_id == "29")
				idText.text = "A × B × C";
			else if (wrongID == "32" || ques_id == "32")
				idText.text = "A ÷ B ÷ C";
			else if (wrongID == "37" || ques_id == "37")
				idText.text = "A × B + C";
			else if (wrongID == "45" || ques_id == "45")
				idText.text = "A × B + C × D";
			// idText.text = GameController_quickanswer.wrongQues;
			continueBtn.SetActive(false);
			watchagainBtn.SetActive(false);
			StartCoroutine(waitingSec_4s());
		}

	}

	void showQuesInfo () {
		isWait = true;

/*29 32 37 45*/
		if (wrongID == "29" || ques_id == "29") {
			// idText.text = "A × B × C";
			teachAni2.Play("ID_29");
			print("29");
		}
		else if (wrongID == "32" || ques_id == "32") {
			// idText.text = "A ÷ B ÷ C";
			teachAni2.Play("ID_32");
			print("32");
		}
		else if (wrongID == "37" || ques_id == "37") {
			// idText.text = "A × B + C";
			teachAni2.Play("ID_37");
			print("37");
		}
		else if (wrongID == "45" || ques_id == "45") {
			// idText.text = "A × B + C × D";
			teachAni2.Play("ID_45");
			print("45");
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
