using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController_quickanswer : MonoBehaviour {
	public GameObject[] paper = new GameObject[5];
	public GameObject quesTextObj, label1, finishBtn, lineObj, selectAnsObj, feedbackPanel, startPanel;
	public Text[] selectAnsText = new Text[4];
	public Text QuesText;
	public Sprite circleSprite;
	public Sprite[] stampSprite = new Sprite[2];
	public SpriteRenderer[] mainren_circle = new SpriteRenderer[4];
	public SpriteRenderer mainren_stamp, mainren_wrong1, mainren_wrong2, mainren_wrong3;
	public Sprite[] wrong_fillred = new Sprite[3];
	public Animator feedbackAni;
	
	private string Ans, user_ans, ques_id, questions;
	private string[] wrongID_arr = new string[3];
	private string[] wrongQues_arr = new string[3];
	private bool[] isClickNum = new bool[4];

	public static bool rotate_paper = false, isAnsFiveTimes = false;
	public static int c = 0, k = 0;
	public static bool gamestate = true;
	public static bool isRight = false;
	public static string wrongID, wrongQues;
	//public Button finishBtn;
	//check boss or normal world
	public bool isboss = false;

	// Use this for initialization
	void Start () {
		if(GameObject.Find("BossSaveData"))
			isboss = true;
		c = 0;
		k = 0;
		gamestate = false;
		isRight = false;
		isAnsFiveTimes = false;
		wrongID = "";
		wrongQues = "";
		//finishBtn.onClick.AddListener(checkAnswer);
	}
	
	// Update is called once per frame
	void Update () {
		
		Ans = testbankDBHandler_quickanswer.Ans;
		ques_id = testbankDBHandler_quickanswer.ques_id;
		questions = testbankDBHandler_quickanswer.questions;

		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
		
		if (rotate_paper) {
			if (c == 1)
				paper[1].transform.Rotate(new Vector3(0,0,+3f));
			else if (c == 2)
				paper[2].transform.Rotate(new Vector3(0,0,-3f));
			else if (c == 3)
				paper[3].transform.Rotate(new Vector3(0,0,-6f));
			else if (c == 4)
				paper[4].transform.Rotate(new Vector3(0,0,+6f));
			rotate_paper = false;
		}

		if (gamestate == false) {
			lineObj.SetActive(false);
			label1.SetActive(false);
			finishBtn.SetActive(false);
			quesTextObj.SetActive(false);
			selectAnsObj.SetActive(false);
		} else {
			lineObj.SetActive(true);
			label1.SetActive(true);
			finishBtn.SetActive(true);
			quesTextObj.SetActive(true);
			selectAnsObj.SetActive(true);
		}

	}

	public void clickStartGame () {
		gamestate = true;
		startPanel.SetActive(false);
	}

/*rotate pater, stamp, checkAnswer, feedback*/
	public void checkAnswer () {
		
		c++;
		Debug.Log("Click times: " + c + " / ques_id: " + ques_id + " / questions: " + questions + " / user_ans: " + user_ans + " / Ans: " + Ans);

		gamestate = false;

		for (int i = 0; i < 4; i++) {
			selectAnsText[i].text = "";
			mainren_circle[i].sprite = null;
		}
		
		for (int i = 0; i < 5; i++) {
			if (i == (c-1)) {
				rotate_paper = true;
				paper[i].SetActive(false);
			}
		}

		if (user_ans == Ans) {
			isRight = true;
		} else {
			if (k == 0) {		
				mainren_wrong1.sprite = wrong_fillred[0];
				wrongID_arr[0] = ques_id;
				wrongQues_arr[0] = questions;
				// print(wrongID_arr[0]);
			} else if (k == 1) {
				mainren_wrong2.sprite = wrong_fillred[1];
				wrongID_arr[1] = ques_id;
				wrongQues_arr[1] = questions;
				// print(wrongID_arr[1]);
			} else {
				mainren_wrong3.sprite = wrong_fillred[2];
				wrongID_arr[2] = ques_id;
				wrongQues_arr[2] = questions;
				// print(wrongID_arr[2]);
				gamestate = false;
			}
			k++;
		}

		if (c == 5 && k < 3) {
			Map1_0.getclue();
			isAnsFiveTimes = true;
			gamestate = false;
			StartCoroutine(showStamp());
		}

		if (isRight) {	
			mainren_stamp.sprite = stampSprite[0];
			StartCoroutine(showStamp());
		} else {
			mainren_stamp.sprite = stampSprite[1];
			StartCoroutine(showStamp());
		}

	}

	IEnumerator showStamp () {
		yield return new WaitForSeconds(1f);


/*Answer five times*/		
		if (isAnsFiveTimes) {
			isRight = false;
			mainren_stamp.sprite = null;
			feedbackPanel.SetActive(true);
			feedbackAni.Play("quickanswer_right");
			StartCoroutine(showFeedbackPanel());
		}
/*If answer wrongly three times*/
		else {
			if (k < 3) {
				mainren_stamp.sprite = null;
				gamestate = true;
				isRight = false;
				testbankDBHandler_quickanswer.reRanQues();
			} else {
				mainren_stamp.sprite = stampSprite[1];
				gamestate = false;
				int r = Random.Range(0, 3);
				wrongID = wrongID_arr[r];
				wrongQues = wrongQues_arr[r];
				if(isboss)
					BossSave.setDamage();
				
				if (wrongID == "3" || wrongID == "5" || wrongID == "6" || wrongID == "12")
					SceneManager.LoadScene("num_TeacherScene_quickanswer");
				else if (wrongID == "29" || wrongID == "32" || wrongID == "37" || wrongID == "45")
					SceneManager.LoadScene("num_TeacherScene_quickanswer_2");
				else if (wrongID == "46" || wrongID == "47" || wrongID == "48" || wrongID == "66")
					SceneManager.LoadScene("num_TeacherScene_quickanswer_3");
				
				yield return new WaitForSeconds(0.5f);
			}
		}	
				
	}

	IEnumerator showFeedbackPanel () {
		gamestate = false;
		yield return new WaitForSeconds(1.8f);
		if(isboss){
			BossSave.setDamage();
			SceneManager.LoadScene("BossStage");
		}else
			SceneManager.LoadScene("Chapter_WorldOne");
	}

	public void clickNum (string btnName) {
		
		if (btnName == "selectAns1") {
			isClickNum[0] = true;
			user_ans = selectAnsText[0].text;
		}
		else if (btnName == "selectAns2") {
			isClickNum[1] = true;
			user_ans = selectAnsText[1].text;
		}
		else if (btnName == "selectAns3") {
			isClickNum[2] = true;
			user_ans = selectAnsText[2].text;
		}
		else if (btnName == "selectAns4") {
			isClickNum[3] = true;
			user_ans = selectAnsText[3].text;
		}

/*Only circle one number*/
		for (int i = 0; i < 4; i++) {
			if (isClickNum[i] == true) {
				mainren_circle[i].sprite = circleSprite;
				isClickNum[i] = false;
			}
			else {
				mainren_circle[i].sprite = null;
			}
		}

	}

}
