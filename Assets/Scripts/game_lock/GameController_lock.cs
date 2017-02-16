using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController_lock : MonoBehaviour {
	public Sprite[] num_clickArray = new Sprite[10];
	public Sprite[] num_click_redArray = new Sprite[10];
	public Sprite[] wrong_fillred = new Sprite[3];
	public SpriteRenderer mainren_num1, mainren_num2, mainren_num3, mainren_num4, mainren_wrong1, mainren_wrong2, mainren_wrong3;
	public Text hintText;
	public GameObject feedbackPanel, wrongPanel, startPanel, finishBtn;
	public Animator feedbackAni;
	
	public int index1 = 0, index2 = 0, index3 = 0, index4 = 0;

	private bool showPanel = false;
	private int user_Ans;
	private int[] sepNum = new int[4];
	private string Ans, hint, ques_id;

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
		Ans = testbankDBHandler_lock.Ans;
		hint = testbankDBHandler_lock.hint;
		ques_id = testbankDBHandler_lock.ques_id;

		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}

	public void clickStartGame () {
		gamestate = true;
		startPanel.SetActive(false);
	}


/*Click arrow and change number up and down*/
	public void ChangeNumberUP (string arrowbtnName) {
		
		if (arrowbtnName == "arrow1") {
			if (index1 < 9)
				index1++;
			else
				index1 = 0;
			// print("a1 / index1: " + index1);
			mainren_num1.sprite = num_clickArray [index1];
		} else if (arrowbtnName == "arrow2") {
			if (index2 < 9)
				index2++;
			else
				index2 = 0;
			// print("a2 / index2: " + index2);
			mainren_num2.sprite = num_clickArray [index2];	
		} else if (arrowbtnName == "arrow3") {
			if (index3 < 9)
				index3++;
			else
				index3 = 0;
			// print("a3 / index3: " + index3);
			mainren_num3.sprite = num_clickArray [index3];	
		} else if (arrowbtnName == "arrow4") {
			if (index4 < 9)
				index4++;
			else
				index4 = 0;
			// print("a4 / index4: " + index4);
			mainren_num4.sprite = num_clickArray [index4];	
		}

	}

	public void ChangeNumberDOWN (string arrowbtnName) {

		if (arrowbtnName == "arrow1") {
			if (index1 > 0)
				index1--;
			else
				index1 = 9;
			// print("a1 / index1: " + index1);
			mainren_num1.sprite = num_clickArray [index1];	
		} else if (arrowbtnName == "arrow2") {
			if (index2 > 0)
				index2--;
			else
				index2 = 9;
			// print("a2 / index2: " + index2);
			mainren_num2.sprite = num_clickArray [index2];	
		} else if (arrowbtnName == "arrow3") {
			if (index3 > 0)
				index3--;
			else
				index3 = 9;
			// print("a3 / index3: " + index3);	
			mainren_num3.sprite = num_clickArray [index3];
		} else if (arrowbtnName == "arrow4") {
			if (index4 > 0)
				index4--;
			else
				index4 = 9;
			// print("a4 / index4: " + index4);
			mainren_num4.sprite = num_clickArray [index4];	
		}	

	}


/*Check user answer and compare Ans*/
	public void checkAnswer () {
		gamestate = false;

		int ans_int = System.Convert.ToInt32(Ans);
		SeparateNumber(ans_int);
		user_Ans = index1*1000 + index2*100 + index3*10+ index4;

		print ("user_Ans: " + user_Ans + " / Ans: " + Ans);

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

	void SeparateNumber (int tmp) {	
		for (int i = 3; i >= 0; i--) {
			sepNum[i] = tmp % 10;
			tmp = tmp / 10;
			//print("sepNum["+ i + "]: " + sepNum[i]);
		}
	}

	void answerRight () {
		gamestate = false;

		feedbackPanel.SetActive(true);
		feedbackAni.Play("lock_right");
		// feedbackText.text = "答對了!";
		showPanel = true;
		isRight = true;	

		if (showPanel) {		
			StartCoroutine(waitingPanel());		
			showPanel = false;
		}
	}

	void answerWrong () {

		// feedbackPanel.SetActive(true);
		// feedbackAni.Play("wrong");
		// feedbackText.text = "答錯了!";
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
		}
		else if (k == 3) {
			if (isboss)
				BossSave.setDamage();

			if (ques_id == "3" || ques_id == "5" || ques_id == "6" || ques_id == "12")
				SceneManager.LoadScene("TeacherScene_lock");
			else if (ques_id == "29" || ques_id == "32" || ques_id == "37" || ques_id == "45")
				SceneManager.LoadScene("TeacherScene_lock_2");
			else if (ques_id == "46" || ques_id == "47" || ques_id == "48" || ques_id == "66")
				SceneManager.LoadScene("TeacherScene_lock_3");
				
			yield return new WaitForSeconds(0.5f);	
		} else {
			yield return new WaitForSeconds(1f);
			stepHint ();
		}
		feedbackPanel.SetActive(false);
		wrongPanel.SetActive(false);
		showPanel = false;

		gamestate = true;
	}

	void stepHint () {
		if (k == 1)
			changeNumberToRed();
		else if (k == 2) {
			changeNumberToRed();
			hintText.text = hint;
		}
	}


/*Change number to red*/
	void changeNumberToRed () {
		if (index1 != sepNum[0])
			mainren_num1.sprite = num_click_redArray [index1];
		if (index2 != sepNum[1])
			mainren_num2.sprite = num_click_redArray [index2];
		if (index3 != sepNum[2])
			mainren_num3.sprite = num_click_redArray [index3];
		if (index4 != sepNum[3])
			mainren_num4.sprite = num_click_redArray [index4];
	}
	

}
