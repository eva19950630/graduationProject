using UnityEngine;
using System.Collections;
using System.Linq;
// using System.Collections.Generic;

public class Map1_1 : MonoBehaviour {
	//seat of character and the map
	public int xPos, yPos;
	public static int x_Save_Pos, y_Save_Pos;
	public int now_Pos; 
	public static int now_Save_Pos;
	public bool save;
	public int sign, count, i;
	public static bool undo;
	//map info
	public static int[,] world1 = new int[7,7]
	{
		// ↓ X ; → Y ;
		/*
		6	7	8
		5		9	10	11	
		4	3	2		12	
				1		13	
			   '0' "15"	14	
		*/
		{-1, -1, -1, -1, -1, -1, -1},
		{-1, -1, -1,  0, 15, 14, -1},
		{-1, -1, -1,  1, -1, 13, -1},
		{-1,  4,  3,  2, -1, 12, -1},
		{-1,  5, -1,  9, 10, 11, -1},
		{-1,  6,  7,  8, -1, -1, -1},
		{-1, -1, -1, -1, -1, -1, -1}
	};

	//map game event 
	public static int[] world1_game = new int[16]
	{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1};

	//sign prefab for clone
	public GameObject sign_prefab;
	//cloned sign gameobject
	private GameObject signClone;
	//sprites for change
	// public Sprite sprite_lock;
	public Sprite sprite_lock;
	public Sprite sprite_fish;
	public Sprite sprite_paper;
	public Sprite sprite_balance;
	//sign position (stop position with 2f in y)
	public static Vector3[] world1_stop = new [] 
	{
		new Vector3(-5f,2f,-5f),
		new Vector3(-5f,2f,0f),
		new Vector3(-5f,2f,5f),
		new Vector3(-10f,2f,5f),
		new Vector3(-15f,2f,5f),
		new Vector3(-15f,2f,10f),
		new Vector3(-15f,2f,15f),
		new Vector3(-10f,2f,15f),
		new Vector3(-5f,2f,15f),
		new Vector3(-5f,2f,10f),
		new Vector3(0f,2f,10f),
		new Vector3(5f,2f,10f),
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
	//floor array for change
	public static GameObject[] world1_floor;
	
	//character position
	public static Vector3[] character_position = new []
	{
		new Vector3(-5f,2.375f,-5f),
		new Vector3(-5f,2.375f,0f),
		new Vector3(-5f,2.375f,5f),
		new Vector3(-10f,2.375f,5f),
		new Vector3(-15f,2.375f,5f),
		new Vector3(-15f,2.375f,10f),
		new Vector3(-15f,2.375f,15f),
		new Vector3(-10f,2.375f,15f),
		new Vector3(-5f,2.375f,15f),
		new Vector3(-5f,2.375f,10f),
		new Vector3(0f,2.375f,10f),
		new Vector3(5f,2.375f,10f),
		new Vector3(5f,2.375f,5f),
		new Vector3(5f,2.375f,0f),
		new Vector3(5f,2.375f,-5f),
		new Vector3(0f,2.375f,-5f),
	};
	//camera position
	public static Vector3[] camera_position = new []
	{
		new Vector3(3f, 15f, -29f),
		new Vector3(3f, 15f, -24f),
		new Vector3(3f, 15f, -19f),
		new Vector3(-2f, 15f, -19f),
		new Vector3(-7f, 15f, -19f),
		new Vector3(-7f, 15f, -14f),
		new Vector3(-7f, 15f, -9f),
		new Vector3(-2f, 15f, -9f),
		new Vector3(3f, 15f, -9f),
		new Vector3(3f, 15f, -14f),
		new Vector3(8f, 15f, -14f),
		new Vector3(13f, 15f, -14f),
		new Vector3(13f, 15f, -19f),
		new Vector3(13f, 15f, -24f),
		new Vector3(13f, 15f, -29f),
		new Vector3(8f, 15f, -29f),

	};
	//clue
	public static string[] clue = new string[10]
	{
		"藍 相鄰在 黑 的左邊",
		"黃 旁邊只有一個人",
		"綠 相鄰在 紫 的左邊",
		"紫 和 藍 是對稱的位置",
		"橘 在 紅 的右邊而且是離 黃 最遠的人",
		"總共有 7 個人",
		"黑 在 紅 跟 藍 的中間",
		"紫 相鄰在 橘 的左邊",
		"藍 在 黃 的旁邊",
		"紅 的左邊跟右邊人數相同",
	};
	public static int clue_number = 0;
	public static string cluelist_L, cluelist_R, ans_cluenum;
	//W1-1 last ans
	public static int answer = 4;
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
		clue[0] = "藍 相鄰在 黑 的左邊\n";
		clue[1] = "黃 旁邊只有一個人\n";	
		clue[2] = "綠 相鄰在 紫 的左邊\n";	
		clue[3] = "紫 和 藍 是對稱的位置\n";	
		clue[4] = "橘 是離 黃 最遠的人\n";	
		clue[5] = "總共有 7 個人\n";	
		clue[6] = "黑 在 紅 跟 藍 的中間\n";	
		clue[7] = "紫 相鄰在 橘 的左邊\n";	
		clue[8] = "藍 在 黃 的旁邊\n";	
		clue[9] = "紅 的左邊跟右邊人數相同\n";


		for(i = 0; i < 16; i++){
			world1_game[i] = -1;
		}

		x_Save_Pos = 1;
		y_Save_Pos = 3;
		now_Save_Pos = 0;

		count = 3;
		while(count > 0){
			sign = Random.Range(1,15);
			if(world1_game[sign] == -1){
				world1_game[sign] = 1;
				count --;
			}
		}

		count = 4;
		while(count > 0){
			sign = Random.Range(1,15);
			if(world1_game[sign] == -1){
				world1_game[sign] = 2;
				count --;
			}
		}

		count = 4;
		while(count > 0){
			sign = Random.Range(1,15);
			if(world1_game[sign] == -1){
				world1_game[sign] = 3;
				count --;
			}
		}

		for(i=1;i<16;i++){
			if(world1_game[i] == -1){
				world1_game[i] = 4;
				// world1_game[i] = 1;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		//save for the start get next time
		if(gameventII.save == true){
			//get now state
			xPos = gameventII.xMap;
			yPos = gameventII.yMap;
			now_Pos = gameventII.now;
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
		for(i = 0; i <= 15; i++){
			print("enter!");
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
