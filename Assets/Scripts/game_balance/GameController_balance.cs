using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController_balance : MonoBehaviour {
	public Text weight1000g_Text, weight100g_Text, weight10g_Text, weight1g_Text, hintText;
	public Sprite[] wrong_fillred = new Sprite[3];
	public Sprite[] balanceSprite = new Sprite[3];
	public SpriteRenderer mainren_wrong1, mainren_wrong2, mainren_wrong3, mainren_balance;
	public GameObject feedbackPanel, wrongPanel, startPanel, finishBtn;
	public Animator feedbackAni;

	private bool showPanel = false;
	private int weight_num1 = 0, weight_num2 = 0, weight_num3 = 0, weight_num4 = 0, user_Ans, ans_int;
	private string Ans, hint;

	public static bool gamestate, isRight, isboss;
	public static int k, gID;

/*Pass data*/
	public string useranswer, buttonname, status, teaching;
	private saveGameData_balance script_balance_savedata;

	void Awake () {
		script_balance_savedata = GetComponent<saveGameData_balance> ();
	}

	// Use this for initialization
	void Start () {
		if(GameObject.Find("BossSaveData"))
			isboss = true;

		gamestate = false;
		isRight = false;
		k = 0;

		gID = 4;
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

/*Click weight and change number*/
	public void ChangeWeightNumber (string weightbtnName) {
		
		if (weightbtnName == "weightbtn_1000g") {
			if (weight_num1 < 10)
				weight_num1++;
			else
				weight_num1 = 0;
			// print("weight_num1: " + weight_num1);
			weight1000g_Text.text = ""+weight_num1;
		} else if (weightbtnName == "weightbtn_100g") {
			if (weight_num2 < 10)
				weight_num2++;
			else
				weight_num2 = 0;
			// print("weight_num2: " + weight_num2);
			weight100g_Text.text = ""+weight_num2;
		} else if (weightbtnName == "weightbtn_10g") {
			if (weight_num3 < 10)
				weight_num3++;
			else
				weight_num3 = 0;
			// print("weight_num3: " + weight_num3);
			weight10g_Text.text = ""+weight_num3;
		} else if (weightbtnName == "weightbtn_1g") {
			if (weight_num4 < 10)
				weight_num4++;
			else
				weight_num4 = 0;
			// print("weight_num4: " + weight_num4);
			weight1g_Text.text = ""+weight_num4;
		}

	}

/*Click reset btn and reset all weights number*/
	public void clickResetBtn () {
		useranswer = "0";
		buttonname = "reset";
		status = "-";
		teaching = "no";
		script_balance_savedata.getGameData ();

		weight_num1 = 0;
		weight_num2 = 0;
		weight_num3 = 0;
		weight_num4 = 0;
		weight1000g_Text.text = ""+weight_num1;
		weight100g_Text.text = ""+weight_num2;
		weight10g_Text.text = ""+weight_num3;
		weight1g_Text.text = ""+weight_num4;
	}

/*Check user answer and compare Ans*/
	public void checkAnswer () {
		gamestate = false;

		ans_int = System.Convert.ToInt32(Ans);
		user_Ans = weight_num1*1000 + weight_num2*100 + weight_num3*10+ weight_num4;
		
		print ("Ans: " + Ans + " / user_Ans: " + user_Ans);

		if (user_Ans == ans_int) {
			useranswer = ""+user_Ans;
			buttonname = "finish";
			status = "right";
			teaching = "no";
			script_balance_savedata.getGameData ();

			isRight = true;
		} else {
			if (k == 0) {
				useranswer = ""+user_Ans;	
				buttonname = "finish";
				status = "wrong";
				teaching = "no";
				script_balance_savedata.getGameData ();

				mainren_wrong1.sprite = wrong_fillred[0];
			} else if (k == 1) {
				useranswer = ""+user_Ans;
				buttonname = "finish";
				status = "wrong";
				teaching = "no";
				script_balance_savedata.getGameData ();

				mainren_wrong2.sprite = wrong_fillred[1];
			} else {
				useranswer = ""+user_Ans;
				buttonname = "finish";
				status = "wrong";
				teaching = "yes";
				script_balance_savedata.getGameData ();

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

		feedbackPanel.SetActive(true);
		feedbackAni.Play("balance_right");

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
			isBalance ();
		}
		else if (k == 2) {
			isBalance ();
			hintText.text = hint;
		}
	}

	void isBalance () {
		if (ans_int > user_Ans) {
			if (user_Ans == 0) 
				mainren_balance.sprite = balanceSprite[0];
			else
				mainren_balance.sprite = balanceSprite[1];
		}
		else {
			mainren_balance.sprite = balanceSprite[2];
		}
	}

}
