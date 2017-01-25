using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class fightAnim_boss : MonoBehaviour {

	public GameObject BossChar;
	public GameObject gamesign;
	public Animator Boss;
	public Animator Player;
	public Animator sign;
	public static int gameNum;

	public bool isfirst;

	public GameObject tip_panel;
	public GameObject winPanel;
	//if need save
	// public static bool dosave;

	// Use this for initialization
	void Start () {
		if(!BossSave.back){
			Boss.SetBool("back",false);
			isfirst = true;
		}else{
			Boss.SetBool("back",true);	
			isfirst = false;
		}
		// BossChar.SetActive(false);
		gamesign.SetActive(false);
		// dosave = false;
		gameNum = -1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void getgame(){
		gameNum = Random.Range(1,4);
		BossSave.saved();
		print("gameNum = " + gameNum);
		if(!BossSave.gamefinish){
			//print(BossSave.back);
			if(isfirst){
				tip_panel.SetActive(true);
				StartCoroutine(Show());
			}else{
				gameanin();
				StartCoroutine(WAIT());
			}
		}else{
			sign.SetBool("finish",true);
			gamesign.SetActive(false);
		}

	}

	public void entergame(){
		switch(gameNum){
			case 1:
				SceneManager.LoadScene("lockpassword");
				break;
			case 2:
				SceneManager.LoadScene("FishingScene");
				break;
			case 3:
				SceneManager.LoadScene("QuickanswerScene");
				break;
			case -1:
				print("error: gameNum = -1");
				break;
		}
	}

	IEnumerator WAIT(){

		yield return new WaitForSeconds(3f);
		
		entergame();

	}

	public void showBoss(){
		BossChar.SetActive(true);
	}

	public void endGame(){
		BossSave.finish();
		StartCoroutine(Leave());
	}

	IEnumerator Leave(){

		yield return new WaitForSeconds(1f);
		Boss.SetBool("BossIn",false);
		Player.SetBool("back",false);
		Player.SetBool("standby",false);
		Boss.SetBool("back",false);
		StartCoroutine(playerwin());
	}

	IEnumerator Show(){

		yield return new WaitForSeconds(3f);
		
		tip_panel.SetActive(false);
		StartCoroutine(WAIT());
		gameanin();
	}
	
	IEnumerator playerwin(){
		// yield return new WaitForSeconds(5f);
		winPanel.SetActive(true);
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene("WorldMap");
		// winPanel.SetActive(false);
	}

	public void gameanin(){
		gamesign.SetActive(true);
		switch(gameNum){
			case 1:
				sign.SetInteger("number",1);
				break;
			case 2:
				sign.SetInteger("number",2);
				break;
			case 3:
				sign.SetInteger("number",3);
				break;
		}
	}
}
