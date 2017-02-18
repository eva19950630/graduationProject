using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameController_buying : MonoBehaviour {
	public Text pigero_Text, pigdy_Text, lollipig_Text, hintText;
	public Sprite[] wrong_fillred = new Sprite[3];
	public SpriteRenderer mainren_wrong1, mainren_wrong2, mainren_wrong3;
	public GameObject feedbackPanel, wrongPanel, startPanel, finishBtn;
	public GameObject[] pigeroSprite = new GameObject[3];
	public GameObject[] pigdySprite = new GameObject[4];
	public GameObject[] lollipigSprite = new GameObject[3];
	public Animator feedbackAni;

	private bool showPanel = false;
	private int user_Ans, ans_int, pigero_count, pigdy_count, lollipig_count;
	private double pigero_price, pigdy_price, lollipig_price;
	private string Ans, hint;

	public static bool gamestate, isRight, isboss;
	public static int k, gID;

	// Use this for initialization
	void Start () {
		if(GameObject.Find("BossSaveData"))
			isboss = true;

		gamestate = false;
		isRight = false;
		k = 0;

		gID = 5;
	}
	
	// Update is called once per frame
	void Update () {
		Ans = testbankDBHandler_balance.Ans;
		hint = testbankDBHandler_balance.hint;

		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}

	public void clickStartGame () {
		gamestate = true;
		startPanel.SetActive(false);
	}

/*Click commodities , change counts and calculate the price*/
	public void clickCommoditiesBtn (string cbtn_name) {
		if (cbtn_name == "pigerobtn") {
			pigero_count++;
			if (pigero_count < 4) {
				for (int i = 0; i < 3; i++)
					pigeroSprite[pigero_count-1].SetActive(true);
			}
			pigero_Text.text = ""+pigero_count;
			pigero_price = 3 * pigero_count;
			if (pigero_price > 100) {
				pigero_price*=0.9;
				pigero_price = Math.Round(pigero_price, MidpointRounding.AwayFromZero);
			}
			print("pigero_count:" + pigero_count + " / pigero_price:" + pigero_price);
		} else if (cbtn_name == "pigdybtn") {
			pigdy_count++;
			if (pigdy_count < 5) {
				for (int i = 0; i < 4; i++)
					pigdySprite[pigdy_count-1].SetActive(true);
			}
			pigdy_Text.text = ""+pigdy_count;
			pigdy_price = 1 * pigdy_count;
			print("pigdy_count:" + pigdy_count + " / pigdy_price:" + pigdy_price);
		} else if (cbtn_name == "lollipigbtn") {
			lollipig_count++;
			if (lollipig_count < 4) {
				for (int i = 0; i < 3; i++)
					lollipigSprite[lollipig_count-1].SetActive(true);
			}
			lollipig_Text.text = ""+lollipig_count;
			if (lollipig_count >= 2) {
				if (lollipig_count % 2 == 0)
					lollipig_price = 40 * (lollipig_count/2);
				else
					lollipig_price = 25 + 40 * (lollipig_count/2);
			} else {
				lollipig_price = 25;
			}
			print("lollipig_count:" + lollipig_count + " / lollipig_price:" + lollipig_price);
		}
	}

/*Click reset btn and reset all weights number*/
	public void clickResetBtn () {
		pigero_count = 0;
		pigdy_count = 0;
		lollipig_count = 0;
		pigero_Text.text = ""+pigero_count;
		pigdy_Text.text = ""+pigdy_count;
		lollipig_Text.text = ""+lollipig_count;
		print("reset");
	}

/*Check user answer and compare Ans*/
	public void checkAnswer () {
		gamestate = false;

		ans_int = System.Convert.ToInt32(Ans);
		user_Ans = System.Convert.ToInt32(pigero_price + pigdy_price + lollipig_price);
		
		print ("Ans: " + Ans + " / user_Ans: " + user_Ans);

		if (user_Ans == ans_int) {
			isRight = true;
		} else {
			if (k == 0) {		
				mainren_wrong1.sprite = wrong_fillred[0];
			} else if (k == 1) {
				mainren_wrong2.sprite = wrong_fillred[1];
			} else {
				mainren_wrong3.sprite = wrong_fillred[2];
				finishBtn.SetActive(false);
				gamestate = false;
			}
			k++;
		}

		if (isRight)
			answerRight ();
		else
			answerWrong ();

	}

	void answerRight () {
		gamestate = false;

		// feedbackPanel.SetActive(true);
		// feedbackAni.Play("balance_right");

		showPanel = true;
		isRight = true;	

		if (showPanel) {		
			StartCoroutine(waitingPanel());		
			showPanel = false;
		}
	}

	void answerWrong () {
		wrongPanel.SetActive(true);
		showPanel = true;

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
			if (isboss)
				BossSave.setDamage();

			yield return new WaitForSeconds(1f);
			game_mechanism.enterTeaching(gID);

		} else {
			yield return new WaitForSeconds(1f);
			stepHint ();
		}
		// feedbackPanel.SetActive(false);
		wrongPanel.SetActive(false);
		showPanel = false;

		gamestate = true;
	}

	void stepHint () {
		if (k == 1) {
			
		}
		else if (k == 2) {
			hintText.text = hint;
		}
	}
	

}
