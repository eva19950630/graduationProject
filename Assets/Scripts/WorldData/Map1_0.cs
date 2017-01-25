using UnityEngine;
using System.Collections;
using System.Linq;
// using System.Collections.Generic;

public class Map1_0 : MonoBehaviour {
	//seat of character and the map
	public int xPos, yPos;
	public static int x_Save_Pos, y_Save_Pos;
	public int now_Pos; 
	public static int now_Save_Pos;
	public bool save;
	public int sign, count, i;
	public static bool undo;
	//map info
	public static int[,] world1 = new int[5,6]
	{
		// ↓ X ; → Y ;
		/*
			2	3	4	5
			1			6
		   '0' "9"	8	7
		*/
		{-1, -1, -1, -1, -1, -1},
		{-1,  0,  9,  8,  7, -1},
		{-1,  1, -1, -1,  6, -1},
		{-1,  2,  3,  4,  5, -1},
		{-1, -1, -1, -1, -1, -1}
	};

	//map game event 
	public static int[] world1_game = new int[10]
	{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1};

	//sign prefab for clone
	public GameObject sign_prefab;
	//cloned sign gameobject
	private GameObject signClone;
	//sprites for change
	public Sprite sprite_lock;
	public Sprite sprite_fish;
	public Sprite sprite_paper;
	//sign position (stop position with 2f in y)
	public static Vector3[] world1_stop = new [] 
	{
		new Vector3(-5f,2f,-5f),
		new Vector3(-5f,2f,0f),
		new Vector3(-5f,2f,5f),
		new Vector3(0f,2f,5f),
		new Vector3(5f,2f,5f),
		new Vector3(10f,2f,5f),
		new Vector3(10f,2f,0f),
		new Vector3(10f,2f,-5f),
		new Vector3(5f,2f,-5f),
		new Vector3(0f,2f,-5f),

	};
	//Material for change
	public Material material_lock;
	public Material material_fish;
	public Material material_paper;
	//floor array for change
	public static GameObject[] world1_floor;
	
	//character position
	public static Vector3[] character_position = new []
	{
		new Vector3(-5f, 2.375f, -5f),
		new Vector3(-5f, 2.375f, 0f),
		new Vector3(-5f, 2.375f, 5f),
		new Vector3(0f, 2.375f, 5f),
		new Vector3(5f, 2.375f, 5f),
		new Vector3(10f, 2.375f, 5f),
		new Vector3(10f, 2.375f, 0f),
		new Vector3(10f, 2.375f, -5f),
		new Vector3(5f, 2.375f, -5f),
		new Vector3(0f, 2.375f, -5f)
	};
	//camera position
	public static Vector3[] camera_position = new []
	{
		new Vector3(3f, 15f, -29f),
		new Vector3(3f, 15f, -24f),
		new Vector3(3f, 15f, -19f),
		new Vector3(8f, 15f, -19f),
		new Vector3(13f, 15f, -19f),
		new Vector3(18f, 15f, -19f),
		new Vector3(18f, 15f, -24f),
		new Vector3(18f, 15f, -29f),
		new Vector3(13f, 15f, -29f),
		new Vector3(8f, 15f, -29f),
	};
	//clue
	public static string[] clue = new string[10]
	{"\"4\"\n", "吃\n", "乾淨\n", "泥巴\n", "P\n", "捲", "食物\n", "粉紅\n", "生肖\n", "鼻子"};
	public static int clue_number = 0;
	public static string cluelist_L, cluelist_R, ans_cluenum;
	//W1-0 last ans
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


		for(i = 0; i < 10; i++){
			world1_game[i] = -1;
		}

		x_Save_Pos = 1;
		y_Save_Pos = 1;
		now_Save_Pos = 0;

		count = 3;
		while(count > 0){
			sign = Random.Range(1,10);
			if(world1_game[sign] == -1){
				world1_game[sign] = 1;
				count --;
			}
		}

		count = 3;
		while(count > 0){
			sign = Random.Range(1,10);
			if(world1_game[sign] == -1){
				world1_game[sign] = 2;
				count --;
			}
		}
		count = 3;
		while(count > 0){
			sign = Random.Range(1,10);
			if(world1_game[sign] == -1){
				world1_game[sign] = 3;
				count --;
			}
		}
		Clone();
	}
	
	// Update is called once per frame
	void Update () {

		//save for the start get next time
		if(gamevent.save == true){
			//get now state
			xPos = gamevent.xMap;
			yPos = gamevent.yMap;
			now_Pos = gamevent.now;
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
		for(i = 0; i <= 9; i++){
			//print(i + ":" + world1_game[i]);
			if(world1_game[i] != -1){
				signClone = Instantiate(sign_prefab, world1_stop[i], Quaternion.identity) as GameObject;
				if(world1_game[i] == 1 && signClone!= null){
					signClone.GetComponent<SpriteRenderer>().sprite = sprite_lock;
					world1_floor[i].GetComponent<Renderer>().material = material_lock;
				}else if(world1_game[i] == 2){
					signClone.GetComponentInChildren<SpriteRenderer>().sprite = sprite_fish;
					world1_floor[i].GetComponent<Renderer>().material = material_fish;
				}else if(world1_game[i] == 3){
					signClone.GetComponentInChildren<SpriteRenderer>().sprite = sprite_paper;
					world1_floor[i].GetComponent<Renderer>().material = material_paper;
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
