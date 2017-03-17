using UnityEngine;
using System.Collections;

public class BossSave : MonoBehaviour {

	public static BossSave saver;
	//distinguish back form game
	public static bool back = false;
	//save data
	public static float playerLife;
	public static float bossLife;
	public static int gamenum;

	// public bool saved = false;
	public static float damage = 0;
	public static bool isdamage = false;
	//change state to back
	public Animator Boss;
	public Animator Player;
	//check for time
	public static bool time = false;

	public static bool gamefinish = false;

	// Use this for initialization
	void Start () {
		// back = false;
		// gamefinish = false;
		damage = 0;
		isdamage = false;
	}
	
	// Update is called once per frame
	void Update () {
		// if(fightAnim_boss.dosave && !saved){
		// 	gamenum = fightAnim_boss.gameNum;
		// 	playerLife = fightAnim_player.lifepoint_player;
		// 	bossLife = fightAnim_player.lifepoint_boss;
		// 	back = true;
		// 	saved = true;
		// 	print(saved);
		// }

		if(gamefinish){
			Destroy(this.gameObject);
		}
	}

	void Awake(){

		if(saver){
			DestroyImmediate(gameObject);
			// saved = false;
			gamenum = -1;
		}else{
		 	DontDestroyOnLoad(transform.gameObject);
		 	saver = this;
		 	back = false;
		 	gamefinish = false;
		 	// time = false;
		}

	}

	public static void setDamage(){
		print(time);
		if(gamenum == 1){
			if(GameController_lock.k < 3 && time == false){
				// print(isdamage);
				isdamage = true;
				damage = 30-(GameController_lock.k*10);
			}else{
				isdamage = false;
				damage = 40;
			}
		}else if(gamenum == 2){
			if(GameController_fishing.k < 3 && !time){
				// print(isdamage);
				isdamage = true;
				damage = 30-(GameController_fishing.k*10);
			}else{
				isdamage = false;
				damage = 40;
			}
		}else if(gamenum == 3){
			if(GameController_quickanswer.k < 3 && !time){
				// print(isdamage);
				isdamage = true;
				damage = 30-(GameController_quickanswer.k*10);
			}else{
				isdamage = false;
				damage = 40;
			}
		}else if(gamenum == 4){
			if(GameController_balance.k < 3 && !time){
				// print(isdamage);
				isdamage = true;
				damage = 30-(GameController_balance.k*10);
			}else{
				isdamage = false;
				damage = 40;
			}
		}else if(gamenum == 5){
			if(GameController_buying.k < 3 && !time){
				// print(isdamage);
				isdamage = true;
				damage = 30-(GameController_buying.k*10);
			}else{
				isdamage = false;
				damage = 40;
			}
		}
		damage /= 100;
		time = false;
	}

	public static void saved(){
		gamenum = fightAnim_boss.gameNum;
		playerLife = fightAnim_player.lifepoint_player;
		bossLife = fightAnim_player.lifepoint_boss;
		back = true;
		// saved = true;
	}
	public static void finish(){
		gamefinish = true;

	}

	public static void timesup(){
		time = true;
		// print("timesupup");
	}
}
