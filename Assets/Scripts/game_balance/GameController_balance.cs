using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_balance : MonoBehaviour {
	public Text weight1000g_Text, weight100g_Text, weight10g_Text, weight1g_Text, hintText;
	public GameObject feedbackPanel, wrongPanel, startPanel, finishBtn;
	public Sprite[] wrong_fillred = new Sprite[3];
	public Sprite[] balanceSprite = new Sprite[2];
	public SpriteRenderer mainren_wrong1, mainren_wrong2, mainren_wrong3, mainren_balance;

	private bool showPanel = false;
	private int weight_num1 = 0, weight_num2 = 0, weight_num3 = 0, weight_num4 = 0, user_Ans, ans_int;
	private string ques_id, Ans, hint;

	public static bool gamestate, isRight, isboss;
	public static int k;

	// Use this for initialization
	void Start () {
		if(GameObject.Find("BossSaveData"))
			isboss = true;

		gamestate = false;
		isRight = false;
		k = 0;
	}
	
	// Update is called once per frame
	void Update () {
		// ques_id = testbankDBHandler_balance.ques_id;
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
		} else if (weightbtnName == "weightbtn_500g") {
			if (weight_num2 < 10)
				weight_num2++;
			else
				weight_num2 = 0;
			// print("weight_num2: " + weight_num2);
			weight100g_Text.text = ""+weight_num2;
		} else if (weightbtnName == "weightbtn_10g") {
			if (weight_num3 < 50)
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

/*Check user answer and compare Ans*/
	public void checkAnswer () {
		gamestate = false;

		ans_int = System.Convert.ToInt32(Ans);
		user_Ans = weight_num1*1000 + weight_num2*500 + weight_num3*10+ weight_num4;
		
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
			// answerRight ();
			print("right");
		else
			answerWrong ();

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
			// yield return new WaitForSeconds(3f);
			// isRight = false;
			// if(GameObject.Find("datasaver"))
			// 	Map1_0.getclue();
			// else if(GameObject.Find("datasaverII"))
			// 	Map1_1.getclue();
			// else
			// 	Map1_2.getclue();
			// if(isboss){
			// 	BossSave.setDamage();
			// 	SceneManager.LoadScene("BossStage");
			// }else{
			// 	if(GameObject.Find("datasaver"))
			// 		SceneManager.LoadScene("Chapter_WorldOne");
			// 	else if(GameObject.Find("datasaverII"))
			// 		SceneManager.LoadScene("Chapter_WorldTwo");
			// 	else
			// 		SceneManager.LoadScene("Chapter_WorldThree");		
			// }
		}
		else if (k == 3) {
			// if (isboss)
			// 	BossSave.setDamage();

			// if (ques_id == "3" || ques_id == "5" || ques_id == "6" || ques_id == "12")
			// 	SceneManager.LoadScene("TeacherScene_lock");
			// else if (ques_id == "29" || ques_id == "32" || ques_id == "37" || ques_id == "45")
			// 	SceneManager.LoadScene("TeacherScene_lock_2");
			// else if (ques_id == "46" || ques_id == "47" || ques_id == "48" || ques_id == "66")
			// 	SceneManager.LoadScene("TeacherScene_lock_3");
				
			// yield return new WaitForSeconds(0.5f);	
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
		if (ans_int > user_Ans)
			mainren_balance.sprite = balanceSprite[0];
		else
			mainren_balance.sprite = balanceSprite[1];
	}

}
