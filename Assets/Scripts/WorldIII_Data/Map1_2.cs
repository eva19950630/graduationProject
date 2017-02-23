using UnityEngine;
using System.Collections;
using System.Linq;
// using System.Collections.Generic;

public class Map1_2 : MonoBehaviour {
	//seat of character and the map
	public int xPos, yPos;
	public static int x_Save_Pos, y_Save_Pos;
	public int now_Pos; 
	public static int now_Save_Pos;
	public bool save;
	public int sign, count, i;
	public static bool undo;
	//map info
	public static int[,] world1 = new int[8,9]
	{
		// ↓ X ; → Y ;
		/*
		6	7	8
		5		9	10	11	
		4	3	2		12	
				1		13	
			   '0' "15"	14	
		*/
		{-1, -1, -1, -1, -1, -1, -1, -1, -1},
		{-1, -1,  2,  1,  0, 25, 24, -1, -1},
		{-1, -1,  3, -1, -1, -1, 23, -1, -1},
		{-1,  5,  4, -1, -1, -1, 22, 21, -1},
		{-1,  6, -1, 12, 13, 14, -1, 20, -1},
		{-1,  7, -1, 11, -1, 15, -1, 19, -1},
		{-1,  8,  9, 10, -1, 16, 17, 18, -1},
		{-1, -1, -1, -1, -1, -1, -1, -1, -1}
	};

	//map game event 
	public static int[] world1_game = new int[26]
	{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1};

	//sign prefab for clone
	public GameObject sign_prefab;
	//cloned sign gameobject
	private GameObject signClone;
	//sprites for change
	public Sprite sprite_lock;
	public Sprite sprite_fish;
	public Sprite sprite_paper;
	public Sprite sprite_balance;
	public Sprite sprite_candy;
	//sign position (stop position with 2f in y)
	public static Vector3[] world1_stop = new [] 
	{
		new Vector3(-5f,2f,-5f),
		new Vector3(-10f,2f,-5f),
		new Vector3(-15f,2f,-5f),
		new Vector3(-15f,2f,0f),
		new Vector3(-15f,2f,5f),
		new Vector3(-20f,2f,5f),
		new Vector3(-20f,2f,10f),
		new Vector3(-20f,2f,15f),
		new Vector3(-20f,2f,20f),
		new Vector3(-15f,2f,20f),
		new Vector3(-10f,2f,20f),
		new Vector3(-10f,2f,15f),
		new Vector3(-10f,2f,10f),
		new Vector3(-5f,2f,10f),
		new Vector3(0f,2f,10f),
		new Vector3(0f,2f,15f),
		new Vector3(0f,2f,20f),
		new Vector3(5f,2f,20f),
		new Vector3(10f,2f,20f),
		new Vector3(10f,2f,15f),
		new Vector3(10f,2f,10f),
		new Vector3(10f,2f,5f),
		new Vector3(5f,2f,5f),
		new Vector3(5f,2f,0f),
		new Vector3(5f,2f,-5f),
		new Vector3(0f,2f,-5f),

	};
	//Material for change
	public Material material_lock;
	public Material material_fish;
	public Material material_paper;
	public Material material_balance;
	public Material material_candy;
	//floor array for change
	public static GameObject[] world1_floor;
	
	//character position
	public static Vector3[] character_position = new []
	{
		new Vector3(-5f,2.375f,-5f),
		new Vector3(-10f,2.375f,-5f),
		new Vector3(-15f,2.375f,-5f),
		new Vector3(-15f,2.375f,0f),
		new Vector3(-15f,2.375f,5f),
		new Vector3(-20f,2.375f,5f),
		new Vector3(-20f,2.375f,10f),
		new Vector3(-20f,2.375f,15f),
		new Vector3(-20f,2.375f,20f),
		new Vector3(-15f,2.375f,20f),
		new Vector3(-10f,2.375f,20f),
		new Vector3(-10f,2.375f,15f),
		new Vector3(-10f,2.375f,10f),
		new Vector3(-5f,2.375f,10f),
		new Vector3(0f,2.375f,10f),
		new Vector3(0f,2.375f,15f),
		new Vector3(0f,2.375f,20f),
		new Vector3(5f,2.375f,20f),
		new Vector3(10f,2.375f,20f),
		new Vector3(10f,2.375f,15f),
		new Vector3(10f,2.375f,10f),
		new Vector3(10f,2.375f,5f),
		new Vector3(5f,2.375f,5f),
		new Vector3(5f,2.375f,0f),
		new Vector3(5f,2.375f,-5f),
		new Vector3(0f,2.375f,-5f),

	};
	//camera position
	public static Vector3[] camera_position = new []
	{
		new Vector3(3f, 15f, -29f),
		new Vector3(-2f, 15f, -29f),
		new Vector3(-7f, 15f, -29f),
		new Vector3(-7f, 15f, -24f),
		new Vector3(-7f, 15f, -19f),
		new Vector3(-12f, 15f, -19f),
		new Vector3(-12f, 15f, -14f),
		new Vector3(-12f, 15f, -9f),
		new Vector3(-12f, 15f, -4f),
		new Vector3(-7f, 15f, -4f),
		new Vector3(-2f, 15f, -4f),
		new Vector3(-2f, 15f, -9f),
		new Vector3(-2f, 15f, -14f),
		new Vector3(3f, 15f, -14f),
		new Vector3(8f, 15f, -14f),
		new Vector3(8f, 15f, -9f),
		new Vector3(8f, 15f, -4f),
		new Vector3(13f, 15f, -4f),
		new Vector3(18f, 15f, -4f),
		new Vector3(18f, 15f, -9f),
		new Vector3(18f, 15f, -14f),
		new Vector3(18f, 15f, -19f),
		new Vector3(13f, 15f, -19f),
		new Vector3(13f, 15f, -24f),
		new Vector3(13f, 15f, -29f),
		new Vector3(8f, 15f, -29f)
		
	};
	//clue
	public static string[] clue = new string[10]
	{"\"4\"\n", "吃\n", "乾淨\n", "泥巴\n", "P\n", "捲", "食物\n", "粉紅\n", "生肖\n", "鼻子"};
	public static int clue_number = 0;
	public static string cluelist_L, cluelist_R, ans_cluenum;
	//W1-1 last ans
	public static int answer = 7;
	//music status
	public static bool music_stat;
	// Use this for initialization
	public static bool promptShow;
	void Start () {

		//initial clues
		clue_number = 0;
		cluelist_L = "";
		cluelist_R = "";
		ans_cluenum = "";

		//initial music status
		music_stat = true;

		//initial prompt for clue
		promptShow = false;

		//world1_floor = GameObject.FindGameObjectsWithTag("floor").OrderBy( go => go.name ).ToArray();
		undo = false;
		cluelist_L = "";
		cluelist_R = "";
		ans_cluenum = "獲得的提示數量\n" + clue_number + "/10";
		clue[0] = "\"4\"\n";//蓮花
		clue[1] = "吃\n";	//桃子
		clue[2] = "乾淨\n";	//海藻
		clue[3] = "泥巴\n";	//耙子
		clue[4] = "\"P\"";	//筷子
		clue[5] = "捲\n";	//兔子
		clue[6] = "食物\n";	//施萱
		clue[7] = "粉紅\n";	//蝦子
		clue[8] = "生肖\n";	//蝸牛
		clue[9] = "鼻子";


		for(i = 0; i < 26; i++){
			world1_game[i] = -1;
		}

		x_Save_Pos = 1;
		y_Save_Pos = 4;
		now_Save_Pos = 0;

		count = 5;
		while(count > 0){
			sign = Random.Range(1,25);
			if(world1_game[sign] == -1){
				world1_game[sign] = 1;
				count --;
			}
		}

		count = 5;
		while(count > 0){
			sign = Random.Range(1,25);
			if(world1_game[sign] == -1){
				world1_game[sign] = 2;
				count --;
			}
		}

		count = 5;
		while(count > 0){
			sign = Random.Range(1,25);
			if(world1_game[sign] == -1){
				world1_game[sign] = 3;
				count --;
			}
		}

		count = 5;
		while(count > 0){
			sign = Random.Range(1,25);
			if(world1_game[sign] == -1){
				world1_game[sign] = 4;
				count --;
			}
		}
		// count = 0;
		// while(count > 0){
		for(i=1; i<26; i++){
			// sign = Random.Range(1,25);
			if(world1_game[i] == -1){
				world1_game[i] = 5;
				// count --;
			}
		}
		// Clone();
	}
	
	// Update is called once per frame
	void Update () {

		//save for the start get next time
		if(gameventIII.save == true){
			//get now state
			xPos = gameventIII.xMap;
			yPos = gameventIII.yMap;
			now_Pos = gameventIII.now;
			//change the data source
			x_Save_Pos = xPos;
			y_Save_Pos = yPos;
			now_Save_Pos = now_Pos;

			//save music status
			music_stat = w_audio.play_music;
			//Debug.Log("save : " + now_Save_Pos);
		}

		if(undo){
			Clone();
		}

	}

	public void Clone(){
		world1_floor = GameObject.FindGameObjectsWithTag("floor").OrderBy( go => go.name ).ToArray();
		undo = false;
		for(i = 0; i <= 25; i++){
			//print(i + ":" + world1_game[i]);
			if(world1_game[i] != -1){
				signClone = Instantiate(sign_prefab, world1_stop[i], Quaternion.identity) as GameObject;
				if(world1_game[i] == 1 && signClone!= null){
					signClone.GetComponent<SpriteRenderer>().sprite = sprite_lock;
					world1_floor[i].GetComponent<Renderer>().material = material_lock;
				}else if(world1_game[i] == 2 && signClone!= null){
					signClone.GetComponentInChildren<SpriteRenderer>().sprite = sprite_fish;
					world1_floor[i].GetComponent<Renderer>().material = material_fish;
				}else if(world1_game[i] == 3 && signClone!= null){
					signClone.GetComponentInChildren<SpriteRenderer>().sprite = sprite_paper;
					world1_floor[i].GetComponent<Renderer>().material = material_paper;
				}else if(world1_game[i] == 4 && signClone!= null){
					signClone.GetComponent<SpriteRenderer>().sprite = sprite_balance;
					world1_floor[i].GetComponent<Renderer>().material = material_balance;
				}else if(world1_game[i] == 5 && signClone!= null){
					signClone.GetComponentInChildren<SpriteRenderer>().sprite = sprite_candy;
					world1_floor[i].GetComponent<Renderer>().material = material_candy;
				}else{
					Debug.Log("error in clone sign");
				}
			}
		}	

	}

	public static void undo_change(){
		undo = true;
	}

	public static string getclue(){
		
		string clue_str;

		if(clue_number == 10){
			clue_str = "你已經得到所有提示囉!";
		}else{
			clue_number++;
			promptShow = true;
			clue_str = "獲得新提示\n  " + clue[clue_number];
			
			if(clue_number < 6){
				cluelist_L += clue[clue_number-1];				
			}else{
				cluelist_R += clue[clue_number-1];				
			}

			ans_cluenum = "獲得的提示數量\n" + clue_number + "/10";
		}
		return clue_str;
	}

	public static void showPrompt_clue(){
		Debug.Log("show get Clue Prompt.");
		promptShow = false;
	}

}
