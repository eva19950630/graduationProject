using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameventIII : MonoBehaviour {

//character & camera
	public GameObject cam;
	public GameObject cam_place;
	public GameObject character;
	public Animator D, diceAni;
	
	public float xC, yC, zC;     //////character
	public float xCam, yCam, zCam;  //////cam_place

	public Vector3 target, target_cam;
	public float speed ,fspeed;
	public bool goNext = true;
//dice
	public static int p = 0;
	public GameObject throwButton;
	public GameObject Dice;
	private bool throwdice;
//MAP
	public static int xMap, yMap;
	public static int now; 
	// private AsyncOperation async_lock,  async_fish, empty;
	public static bool save;
//clue
	public GameObject clue_panel, pass_panel, choose_panel, exit_panel;
	public bool open_clue = false;
	public Text prompt, history;
	// public Text clue_L, clue_R, clue_num;
//saver
	public GameObject savedata;
//gamefinish
	public static bool world_finish = false;
//answer selected
	public int ans_choosed;
//feature button
	public Button button_quit;
	public Button button_clue;
// public Sprite sprite_quit;
	public Sprite sprite_clue1;
	public Sprite sprite_clue2;
// Panel for get clue
	public GameObject GetCluePrompt;
// Panel for the end question
	public GameObject correctPanel;
	public GameObject wrongPanel;
// Number click button
	public Sprite[] num_clickArray = new Sprite[10];
	public int index1 = 0, index2 = 0, index3 = 0, index4 = 0;
	public Button mainren_num1, mainren_num2, mainren_num3, mainren_num4;
// Prompt while the answer is wrong!
	public Text prompt_for_wrong;	

	// Use this for initialization
	void Start () {

		//dice.SetActive(false);
		throwButton = GameObject.Find("throwdice");
		//get and hide the clue, pass question and choose panel
		clue_panel = GameObject.Find("Panel_clue");
		clue_panel.SetActive(false);
		pass_panel = GameObject.Find("pass_question");
		pass_panel.SetActive(false);
		choose_panel = GameObject.Find("choosetodo");
		choose_panel.SetActive(false);
		exit_panel = GameObject.Find("Panel_exit");
		exit_panel.SetActive(false);
		GetCluePrompt = GameObject.Find("panel_prompt");
		correctPanel = GameObject.Find("panel_correct");
		correctPanel.SetActive(false);
		wrongPanel = GameObject.Find("panel_wrong");
		wrongPanel.SetActive(false);

		//get save data
		xMap = Map1_2.x_Save_Pos;
		yMap = Map1_2.y_Save_Pos;
		now = Map1_2.now_Save_Pos;
		speed = 0f;
		//place character and camera
		xC = Map1_2.character_position[now].x;
		xCam = Map1_2.camera_position[now].x;
		zC = Map1_2.character_position[now].z;
		zCam = Map1_2.camera_position[now].z;
		//initial target as now position
		character.transform.position = Map1_2.character_position[now];
		cam_place.transform.position = Map1_2.camera_position[now];		
		
		target = character.transform.position;
		target_cam = cam_place.transform.position;

		//world_finish initial
		world_finish = false;
		Map1_2.undo_change();
		//comeback
		//load the little game
		// async_lock = SceneManager.LoadSceneAsync("lockpassword");
		// async_lock.allowSceneActivation = false;
		//async_fish = SceneManager.LoadSceneAsync("FishingScene");
		//async_fish.allowSceneActivation = false;
		save = false;
		// Map1_2.reClone();
		// print("xC = " + xC + "zC = " + zC + "now = " + now);
		Dice.SetActive(false);

		if(Map1_2.promptShow)
			GetCluePrompt.SetActive(true);
			// Map1_2.showPrompt_clue();
		else
			GetCluePrompt.SetActive(false);


	}
	
	// Update is called once per frame
	void Update () {
		//if(throwdice){
			cam.transform.position = cam_place.transform.position;
			character.transform.position = Vector3.Lerp(character.transform.position, target, speed);
			cam_place.transform.position = Vector3.Lerp(cam_place.transform.position, target_cam, speed);
			speed = calculateNewSpeed(); 			
		//}

		if(goNext && throwdice){
			print("move");
			move();
			StartCoroutine(WAIT());
   			goNext = false;
		
		}
	}

	void move(){
		// print("xMap = " + xMap);
		// print("yMap = " + yMap);
		if(p == 0){
			//D.SetInteger("direction", 0);
			D.Play("DiGoReady");
			//async.allowSceneActivation = true;
			if(Map1_2.world1_game[now]!= -1)
				entergame();
			else{
				throwButton.SetActive(true);
				throwdice = false;
			}
		}else{
			if(Map1_2.world1[xMap,yMap+1] == now+1){
				//D.SetInteger("direction", 2);
				D.Play("DiGoRight");
	   			xC+=5f;
				xCam+=5f;

				target = new Vector3 (xC, yC, zC);
				target_cam = new Vector3 (xCam, yCam, zCam);
				speed = 0.1f;
				fspeed = Vector3.Distance(character.transform.position, target) * speed;
   								
				yMap++;	
				now++;
	   			p--;
	   		}else if(Map1_2.world1[xMap,yMap-1] == now+1){
 				//D.SetInteger("direction", 4);
 				D.Play("DiGoLeft");
 				xC-=5f;
				xCam-=5f;

				target = new Vector3 (xC, yC, zC);
				target_cam = new Vector3 (xCam, yCam, zCam);
				speed = 0.1f;
				fspeed = Vector3.Distance(character.transform.position, target) * speed;
 
    			yMap--;
    			now++;
  	  			p--;
    		}else if(Map1_2.world1[xMap+1,yMap] == now+1){
    			//D.SetInteger("direction", 1);
    			D.Play("DiGoUp");
   				zC +=5f;
				zCam +=5f;	

				target = new Vector3 (xC, yC, zC);
				target_cam = new Vector3 (xCam, yCam, zCam);
				speed = 0.1f;
				fspeed = Vector3.Distance(character.transform.position, target) * speed;
 
   		 		xMap++;
    			now++;
    			p--;
		   	}else if(Map1_2.world1[xMap-1,yMap] == now+1){
   				//D.SetInteger("direction", 3);
   				D.Play("DiGoDown");
   				zC -=5f;
				zCam -=5f;

				target = new Vector3 (xC, yC, zC);
				target_cam = new Vector3 (xCam, yCam, zCam);
				speed = 0.1f;
				fspeed = Vector3.Distance(character.transform.position, target) * speed;
 
  	 			xMap--;
   				now++;
   				p--;
  		  	}else{
    			/* 呼叫 答題or再一圈(回歸地圖起點)畫面*/
    			throwdice = false;
    			choose_panel.SetActive(true);	
    		}

		}
	}

	public void tryAns(){
		choose_panel.SetActive(false);	
		pass_panel.SetActive(true);
		// clue_num.text = Map1_2.ans_cluenum;
	}

	public void getClue(){
		choose_panel.SetActive(false);	
		initial();
		goNext = true;
	}

	public void selectans(int selected){
		ans_choosed = selected;
		// checkAns();
	}

	public void checkAns(){
		string ans;
		ans = Map1_2.checkAns(index1,index2,index3,index4);
		if(ans == "4 A 0 B"){
			Debug.Log("correct ans!");
			world_finish = true;
			correctPanel.SetActive(true);
		}else{
			if(Map1_2.chance_times < 0){
				Map1_2.getclue();
				//show wrong image
				Debug.Log("wrong ans!");
				prompt_for_wrong.text = "E‧R‧R‧O‧R\n答案錯誤\n尋找更多機會次數再次挑戰吧!!";
				pass_panel.SetActive(false);
				wrongPanel.SetActive(true);
				initial();
				goNext = true;
			}else{
				// get ?A?B prompt
				prompt_for_wrong.text = ans + "\n答案已經記錄到\"提示\"之中\n點選燈泡圖示可以查看。";
				wrongPanel.SetActive(true);
			}
		}
	}

	private void initial(){

		character.transform.position = new Vector3 (-5f, 2.375f, -5f);
		cam_place.transform.position = new Vector3 (3f, 15f, -29f);
		target = character.transform.position;
		target_cam = cam_place.transform.position;
		xC = character.transform.position.x;
		xCam = cam_place.transform.position.x;
		zC = character.transform.position.z;
		zCam = cam_place.transform.position.z;

		xMap = 1;
		yMap = 4;
		now = 0;
		p = 0;

		throwButton.SetActive(true);
		D.Play("DiGoReady");
		diceAni.SetBool("setPosi",false);
	}

	IEnumerator WAIT(){

		yield return new WaitForSeconds(0.8f);
		goNext = true;

	}

	private float calculateNewSpeed(){

        float tmp = Vector3.Distance(character.transform.position,target);

        if (tmp == 0){

			return tmp;
		} 
        else
            return (fspeed / tmp);
    }

    public void turn(){

    	//gamevent.p = 2;
    	p = Random.Range(1,7);
		Dice.SetActive(true);
		Dice.transform.position = character.transform.position + new  Vector3(0f,-1f,-2.5f);//(-2.7f,1f,-7.5f);
		diceAni.SetBool("setPosi",true);

    	StartCoroutine(waitingDice());
    	throwButton.SetActive(false);
    	if (p == 1) {
    		diceAni.Play("roll-1");
    	}else if (p == 2) {
    		diceAni.Play("roll-2");
    	}else if (p == 3) {
    		diceAni.Play("roll-3");
    	}else if (p == 4) {
    		diceAni.Play("roll-4");
    	}else if (p == 5) {
    		diceAni.Play("roll-5");
    	}else if (p == 6) {
    		diceAni.Play("roll-6");
    	}
    	//entergame(now + p);
    	Debug.Log(gameventIII.p);
    }

    IEnumerator waitingDice () {
    	yield return new WaitForSeconds(2f);
    	
    	savedataIII.newround();
    	throwdice = true;

    }

    private void entergame(){
    	//Application.runInBackground = true;
    	save = true;
    	if(!savedataIII.gameEntered){
	    	savedataIII.entergame();
	    	switch(Map1_2.world1_game[now]){
	    		case 1:
	    			// async_lock.allowSceneActivation = true;
	    			SceneManager.LoadScene("lockpassword");
	    			break;
	    		case 2:
	    			//async_fish.allowSceneActivation = true;
	    			SceneManager.LoadScene("FishingScene");
	    			break;
	    		case 3:
	    			SceneManager.LoadScene("QuickanswerScene");
	    			break;
	    		case 4:
	    			SceneManager.LoadScene("BalanceScene");
	    			break;
	    		case 5:
	    			SceneManager.LoadScene("BuyingScene");
	    			break;
	    	}
    	}
    	//async.allowSceneActivation = false;
    	//yield return 1;
    }

    public void showclue(){
    	// clue_L.text = Map1_2.cluelist_L;
    	// clue_R.text = Map1_2.cluelist_R;
    	int c = Map1_2.chance_times + 1;
    	prompt.text = ("" + c);
    	history.text = Map1_2.history;
    	if(open_clue){
    		clue_panel.SetActive(false);
    		open_clue = false;	
    		button_clue.image.overrideSprite = sprite_clue1;
		}else{
			scroll_record.initial();
			clue_panel.SetActive(true);
			open_clue = true;
    		button_clue.image.overrideSprite = sprite_clue2;			
		}

    }

    public void clickexit(){
    	exit_panel.SetActive(true);
    }

    public void exitYes(){
    	world_finish = true;
    	SceneManager.LoadScene("WorldMap");
    }

    public void exitNo(){
    	exit_panel.SetActive(false);    	
    }

    public void closePrompt(){
    	GetCluePrompt.SetActive(false);
    	Map1_2.showPrompt_clue();
    }

    public void GameEnd(int ans){
    	if(ans == 1)
    		// correctPanel.SetActive(false);
    		SceneManager.LoadScene("WorldMap");
    	else{
    		wrongPanel.SetActive(false);
    		// if(Map1_2.chance_times < 0){
	    	// 	initial();
	    	// 	goNext = true;    			
    		// }
    	}

    }

    /*Click arrow and change number up and down*/
	public void ChangeNumberUP (string arrowbtnName) {
		
		if (arrowbtnName == "arrow1") {
			if (index1 < 9)
				index1++;
			else
				index1 = 0;
			// print("a1 / index1: " + index1);
			mainren_num1.image.overrideSprite = num_clickArray [index1];
		} else if (arrowbtnName == "arrow2") {
			if (index2 < 9)
				index2++;
			else
				index2 = 0;
			// print("a2 / index2: " + index2);
			mainren_num2.image.overrideSprite = num_clickArray [index2];	
		} else if (arrowbtnName == "arrow3") {
			if (index3 < 9)
				index3++;
			else
				index3 = 0;
			// print("a3 / index3: " + index3);
			mainren_num3.image.overrideSprite = num_clickArray [index3];	
		} else if (arrowbtnName == "arrow4") {
			if (index4 < 9)
				index4++;
			else
				index4 = 0;
			// print("a4 / index4: " + index4);
			mainren_num4.image.overrideSprite = num_clickArray [index4];	
		}

	}

	public void ChangeNumberDOWN (string arrowbtnName) {

		if (arrowbtnName == "arrow1") {
			if (index1 > 0)
				index1--;
			else
				index1 = 9;
			// print("a1 / index1: " + index1);
			mainren_num1.image.overrideSprite = num_clickArray [index1];	
		} else if (arrowbtnName == "arrow2") {
			if (index2 > 0)
				index2--;
			else
				index2 = 9;
			// print("a2 / index2: " + index2);
			mainren_num2.image.overrideSprite = num_clickArray [index2];	
		} else if (arrowbtnName == "arrow3") {
			if (index3 > 0)
				index3--;
			else
				index3 = 9;
			// print("a3 / index3: " + index3);	
			mainren_num3.image.overrideSprite = num_clickArray [index3];
		} else if (arrowbtnName == "arrow4") {
			if (index4 > 0)
				index4--;
			else
				index4 = 9;
			// print("a4 / index4: " + index4);
			mainren_num4.image.overrideSprite = num_clickArray [index4];	
		}	

	}
}