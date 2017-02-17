﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController_buying : MonoBehaviour {
	public Text lollipig_Text, pigero_Text, pigdy_Text, hintText;
	public Sprite[] wrong_fillred = new Sprite[3];
	public SpriteRenderer mainren_wrong1, mainren_wrong2, mainren_wrong3;
	public GameObject feedbackPanel, wrongPanel, startPanel, finishBtn;
	public Animator feedbackAni;

	private bool showPanel = false;
	private int user_Ans, ans_int;
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

/*Check user answer and compare Ans*/
	public void checkAnswer () {
		gamestate = false;

		ans_int = System.Convert.ToInt32(Ans);
		// user_Ans = weight_num1*1000 + weight_num2*500 + weight_num3*10+ weight_num4;
		
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
