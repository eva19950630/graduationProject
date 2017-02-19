using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController_fishing : MonoBehaviour {
	public Text[] fishText = new Text[10];
	public Text checkText;
	public Button[] fishBtn = new Button[10];
	public GameObject[] fish = new GameObject[10];
	public GameObject label3, finishBtn, resetBtn, feedbackPanel, wrongPanel, startPanel;
	public Animator[] fishAni = new Animator[10];
	public Sprite[] wrong_fillred = new Sprite[3];
	public SpriteRenderer mainren_wrong1, mainren_wrong2, mainren_wrong3;
	public Animator feedbackAni;

	private bool catchfish = true, showPanel = false;
	private string buttonName, ans, user_ans;
	private string[] fishNum = new string[10];
	private List<string> Anslist = new List<string>();
	
	public static int k, gID;
	public static bool gamestate = true, isRight = false;
	public static bool[] catching = new bool[10];
	//check boss or normal world
	public bool isboss = false;

	// Use this for initialization
	void Start () {
		if(GameObject.Find("BossSaveData"))
			isboss = true;
		
		gamestate = false;
		isRight = false;
		k = 0;	
		for (int i = 0; i < 10; i++)
			catching[i] = false;

		gID = 2;
	}
	
	// Update is called once per frame
	void Update () {
		fishNum = testbankDBHandler_fishing.fishNum;
		Anslist = testbankDBHandler_fishing.Anslist;

		for (int i = 0; i < 10; i++)
			fishText[i].text = fishNum[i];
	}

	public void clickStartGame () {
		gamestate = true;
		startPanel.SetActive(false);
	}

/*Check user_ans and compare Ans*/
	public void checkAns () {
		gamestate = false;

		//print("Anslist.Count: " + Anslist.Count);
		/*for (int i = 0; i < Anslist.Count; i++)
			Debug.Log("Ans[" + i + "]: " + Anslist[i]);*/
		
		if (Anslist.Count == 2) {
			var user_ans_trans = "";
			if(user_ans != null)
				user_ans_trans = user_ans.Replace("×", "*").Replace("÷","/");
			double user_ans_compute = Evaluate(user_ans_trans);
			int ans_compute = System.Convert.ToInt32(Anslist[1]);
			print("user_ans_compute: " + user_ans_compute + " / ans_compute: " + ans_compute);
			if (user_ans_compute == ans_compute)
				isRight = true;
		} else {
			for (int i = 0; i < Anslist.Count; i++) {
				if (user_ans == Anslist[i])
					isRight = true;
			}
		}
		
		if (isRight) {	
			finishBtn.SetActive(false);
			resetBtn.SetActive(false);
			label3.SetActive(false);
			

			feedbackPanel.SetActive(true);
			// feedbackText.text = "答對了!";
			feedbackAni.Play("fishing_right");

			showPanel = true;

			Debug.Log("User Ans: " + user_ans + " / Wrong times: " + k);
		} else {
			//audio.playWrongSound();
			// gamestate = true;

			// feedbackPanel.SetActive(true);
			// feedbackText.text = "答錯了!";
			// feedbackAni.Play("wrong");
			wrongPanel.SetActive(true);
			showPanel = true;

			if (k == 0) {		
				mainren_wrong1.sprite = wrong_fillred[0];	
			} else if (k == 1) {
				mainren_wrong2.sprite = wrong_fillred[1];
			} else {
				mainren_wrong3.sprite = wrong_fillred[2];
				finishBtn.SetActive(false);
				resetBtn.SetActive(false);
				label3.SetActive(false);
				gamestate = false;
			}

			k++;
			Debug.Log("User Ans: " + user_ans + " / Wrong times: " + k);

			clickReset ();

		}

		if (showPanel) {			
			StartCoroutine(waitingPanel());		
			showPanel = false;
		}

	}

	IEnumerator waitingPanel () {		
		if (isRight) {
			yield return new WaitForSeconds(3f);
			isRight = false;
			if(GameObject.Find("datasaver"))
				Map1_0.getclue();
			else if(GameObject.Find("datasaverII"))
				Map1_1.getclue();
			else
				Map1_2.getclue();
			if(isboss){
				BossSave.setDamage();
				SceneManager.LoadScene("BossStage");
			}else{
				if(GameObject.Find("datasaver"))
					SceneManager.LoadScene("Chapter_WorldOne");
				else if(GameObject.Find("datasaverII"))
					SceneManager.LoadScene("Chapter_WorldTwo");
				else
					SceneManager.LoadScene("Chapter_WorldThree");		
			}
		} else if (k == 3) {
			if(isboss)
				BossSave.setDamage();
			
			yield return new WaitForSeconds(1f);
			game_mechanism.enterTeaching(gID);

		} else {
			yield return new WaitForSeconds(1f);
		}
		feedbackPanel.SetActive(false);
		wrongPanel.SetActive(false);
		showPanel = false;	

		gamestate = true;
	}

	//Compute
	public static double Evaluate(string expression) {
		try {
			var doc = new System.Xml.XPath.XPathDocument(new System.IO.StringReader("<r/>"));
			var nav = doc.CreateNavigator();
			var newString = expression;
			newString = (new System.Text.RegularExpressions.Regex(@"([\+\-\*])")).Replace(newString, " ${1} ");
			newString = newString.Replace("/", " div ").Replace("%", " mod ");
			return (double)nav.Evaluate("number(" + newString + ")");
		} catch {
			return -1;
		}
	}

/*Click FishBtn*/
	public void clickNum (string btnName) {

		buttonName = btnName;
		//print("ButtonClicked: "+ buttonName);
		label3.SetActive(false);
		if (catchfish) {
			StartCoroutine(waiting());
			catchfish = false;
		}

		if (buttonName == "fish1") {
			catching[0] = true;
			fishText[0].color = Color.white;		
			fishAni[0].Play("fish_catch");
		}
		else if (buttonName == "fish2") {
			catching[1] = true;
			fishText[1].color = Color.white;
			fishAni[1].Play("fish_catch2");
		}
		else if (buttonName == "fish3") {
			catching[2] = true;
			fishText[2].color = Color.white;
			fishAni[2].Play("fish_catch");
		}
		else if (buttonName == "fish4") {
			catching[3] = true;
			fishText[3].color = Color.white;
			fishAni[3].Play("fish_catch2");
		}
		else if (buttonName == "fish5") {
			catching[4] = true;
			fishText[4].color = Color.white;
			fishAni[4].Play("fish_catch");
		}
		else if (buttonName == "fish6") {
			catching[5] = true;
			fishText[5].color = Color.white;
			fishAni[5].Play("fish_catch2");
		}
		else if (buttonName == "fish7") {
			catching[6] = true;
			fishText[6].color = Color.white;
			fishAni[6].Play("fish_catch");
		}
		else if (buttonName == "fish8") {
			catching[7] = true;
			fishText[7].color = Color.white;
			fishAni[7].Play("fish_catch2");
		}
		else if (buttonName == "fish9") {
			catching[8] = true;
			fishText[8].color = Color.white;
			fishAni[8].Play("fish_catch");
		}
		else if (buttonName == "fish10") {
			catching[9] = true;
			fishText[9].color = Color.white;
			fishAni[9].Play("fish_catch2");
		}

		for (int i = 0; i < 10; i++) {
			if (catching[i] == false)
				fishBtn[i].interactable = false;
		}
	}

	IEnumerator waiting () {		
		yield return new WaitForSeconds(0.4f);
		
		for (int i = 0; i < 10; i++) {
			if (catching[i] == true)
				fish[i].SetActive(false);
		}

		if (buttonName == "fish1") {
			checkText.text += fishText[0].text + ' ';
			user_ans += fishText[0].text;
		} else if (buttonName == "fish2") {
			checkText.text += fishText[1].text + ' ';
			user_ans += fishText[1].text;
		} else if (buttonName == "fish3") {
			checkText.text += fishText[2].text + ' ';
			user_ans += fishText[2].text;
		} else if (buttonName == "fish4") {
			checkText.text += fishText[3].text + ' ';
			user_ans += fishText[3].text;
		} else if (buttonName == "fish5") {
			checkText.text += fishText[4].text + ' ';
			user_ans += fishText[4].text;
		} else if (buttonName == "fish6") {
			checkText.text += fishText[5].text + ' ';
			user_ans += fishText[5].text;
		} else if (buttonName == "fish7") {
			checkText.text += fishText[6].text + ' ';
			user_ans += fishText[6].text;
		} else if (buttonName == "fish8") {
			checkText.text += fishText[7].text + ' ';
			user_ans += fishText[7].text;
		} else if (buttonName == "fish9") {
			checkText.text += fishText[8].text + ' ';
			user_ans += fishText[8].text;
		} else if (buttonName == "fish10") {
			checkText.text += fishText[9].text + ' ';
			user_ans += fishText[9].text;
		}
		
		catchfish = true;

		for (int i = 0; i < 10; i++)
			if (catching[i] == false)
				fishBtn[i].interactable = true;
	}

/*Click ResetBtn*/
	public void clickReset () {
		for (int i = 0; i < 10; i++) {
			catching[i] = false;
			fish[i].SetActive(true);
			fishText[i].color = new Color(131.0f/255.0f, 255.0f/255.0f, 244.0f/255.0f);
		}
		user_ans = "";
		checkText.text = "";		
		label3.SetActive(true);
		Debug.Log("Reset user_ans: " + user_ans);
	}

	

}
