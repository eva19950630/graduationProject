using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeachingController_quickanswer_3 : MonoBehaviour {

	public Animator teachAni3;
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
			if (wrongID == "46" || ques_id == "46")
				idText.text = "A × B - C × D";
			else if (wrongID == "47" || ques_id == "47")
				idText.text = "A ÷ B + C ÷ D";
			else if (wrongID == "48" || ques_id == "48")
				idText.text = "A ÷ B - C ÷ D";
			else if (wrongID == "66" || ques_id == "66")
				idText.text = "A × ( B + C )";
			// idText.text = GameController_quickanswer.wrongQues;
			continueBtn.SetActive(false);
			watchagainBtn.SetActive(false);
			StartCoroutine(waitingSec_4s());
		}

	}

	void showQuesInfo () {
		isWait = true;

/*46 47 48 66*/
		if (wrongID == "46" || ques_id == "46") {
			// idText.text = "A × B - C × D";
			teachAni3.Play("ID_46");
			print("46");	
		}
		else if (wrongID == "47" || ques_id == "47") {
			// idText.text = "A ÷ B + C ÷ D";
			teachAni3.Play("ID_47");
			print("47");
		}
		else if (wrongID == "48" || ques_id == "48") {
			// idText.text = "A ÷ B - C ÷ D";
			teachAni3.Play("ID_48");
			print("48");
		}
		else if (wrongID == "66" || ques_id == "66") {
			// idText.text = "A × ( B + C )";
			teachAni3.Play("ID_66");
			print("66");
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
