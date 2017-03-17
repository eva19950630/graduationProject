using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fightAnim_player : MonoBehaviour {

	public GameObject playerChar;
	public Animator Boss;
	public Animator Player;
	//life of player
	public GameObject lifebar_player;
	public Slider life_player;
	public static float lifepoint_player;
	public GameObject lifebar_boss;
	public Slider life_boss;
	public static float lifepoint_boss;

	public float life_point;
	//if need save
	public static bool dosave;
	//panel for winner

	public GameObject losePanel;

	// Use this for initialization
	void Start () {

		if(!BossSave.back){
			lifebar_player.SetActive(false);
			lifebar_boss.SetActive(false);
			lifepoint_player = 1f;
			lifepoint_boss = 1f;
		}else{
			// print("in");
			Player.SetBool("back",true);
			lifebar_player.SetActive(true);
			lifebar_boss.SetActive(true);		
			lifepoint_player = BossSave.playerLife;
			lifepoint_boss = BossSave.bossLife;
			life_player.value = lifepoint_player;
			life_boss.value = lifepoint_boss;
			if(BossSave.isdamage){
				Boss.SetBool("isAttack",true);
				Player.SetBool("isAttack",true);
			}else{
				Boss.SetBool("isAttack",false);
				Player.SetBool("isAttack",false);
			}
		}
		// playerChar.SetActive(false);
		 dosave = false;
		// lifego();
	}
	
	void Awake(){
		if(!BossSave.back){
			Boss.SetBool("BossIn",false);
			Player.SetBool("back",false);
			Player.SetBool("standby",false);
		}
	} 

	// Update is called once per frame
	void Update () {
	
	}

	public void bossin(){
		Boss.SetBool("BossIn",true);
	}

	public void standby(){
		Player.SetBool("standby",true);
	}

	public void lifego(){
		life_point = 0f;
		lifebar_player.SetActive(true);		
		lifebar_boss.SetActive(true);
		StartCoroutine(life_anim());
		Boss.SetBool("prepareGame",true);
	}

	IEnumerator life_anim(){

		while(life_point <= 1f){
			life_point += 0.01f;
			life_player.value = life_point;
			life_boss.value = life_point;
			yield return 0;
		}
	}

	public void showPlayer(){
		playerChar.SetActive(true);
	}

	public void causeDamage(){
		
		if(BossSave.isdamage){
			StartCoroutine(reduceLife_boss());

		}else{
			// print(BossSave.damage);
			StartCoroutine(reduceLife_player());
			print("Damage!!!");
		}
	}

	IEnumerator reduceLife_player(){

		life_point = lifepoint_player;
		// print(BossSave.damage);
		lifepoint_player = (lifepoint_player-BossSave.damage);
		print(lifepoint_player);

		while(life_point > Mathf.Max(lifepoint_player,0f)){
			life_point -= 0.01f;
			life_player.value = life_point;
			yield return 0;
		}
		Player.SetBool("damage",true);
		Boss.SetBool("damage",true);
		BossSave.saved();
		if(lifepoint_player <= 0){
			BossSave.finish();
			print("gameover!");
			StartCoroutine(playerLose());
			Player.SetBool("finish",true);
		}
		Player.SetBool("damage",true);
		Boss.SetBool("damage",true);
	}

	IEnumerator reduceLife_boss(){
		print(BossSave.damage);
		life_point = lifepoint_boss;
		lifepoint_boss = lifepoint_boss-BossSave.damage;
		
		while(life_point > Mathf.Max(lifepoint_boss,0f)){
			life_point -= 0.01f;
			life_boss.value = life_point;
			yield return 0;
		}
		Player.SetBool("damage",true);
		Boss.SetBool("damage",true);
		dosave = true;
		if(lifepoint_boss <= 0){
			BossSave.finish();
			print("win!");
			Boss.SetBool("finish",true);
		}
	}

	IEnumerator playerLose(){
		// yield return new WaitForSeconds(5f);
		losePanel.SetActive(true);
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene("WorldMap");
		// losePanel.SetActive(false);
	}

	// public static void initial(){
	// 	Boss.SetBool("BossIn",false);
	// 	Player.SetBool("back",false);
	// 	Player.SetBool("standby",false);
	// }

}
