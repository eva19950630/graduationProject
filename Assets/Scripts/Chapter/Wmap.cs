using UnityEngine;
using System.Collections;

public class Wmap : MonoBehaviour {

	public static int player = 1;
	//vector3 for character 
	public static Vector3[] char_1 = new [] 
	{
		new Vector3(248,530,0),
		new Vector3(-100,165,0),
		new Vector3(100,-105,0),
		new Vector3(325,110,0),
	};

	//vector3 for the point-light
	// public static Vector3[] light_1 = new [] 
	// {
	// 	new Vector3(-2.5f,5.6f,-16.5f),
	// 	new Vector3(0.25f,5.6f,-11.75f),
	// 	new Vector3(5.25f,5.6f,-11.75f),
	// 	new Vector3(2.75f,5.6f,-16.5f),
	// 	new Vector3(8f,5.6f,-16.5f)
	// };

	//world info
	public static string[] info = new string[5]
	{
		"",
		"教學關卡\n◎關卡類型數量 : 3\n◎通關條件 : \"聯想\"",
		"◎關卡類型數量 : 4\n◎基本運算規則、能力◎通關條件 : \"邏輯\"\n",
		"◎關卡類型數量 : 5\n◎基本運算規則、能力\n◎擬題思考能力◎通關條件 : \"推理\"",
		// "本關卡需要基本計算能力\n加減乘除及四則運算概念\nSyuan is PIG 3",
		"BOSS STAGE\n◎進階挑戰\n◎通關條件 : \"快速計算、精準計算\""
	};

	// Use this for initialization
	void Start () {
		info[0] = "";
		info[1] = "◎ 教學關卡\n◎ 關卡類型數量 : 3\n◎ 通關條件 : \"聯想\"";
		info[2] = "◎ 關卡類型數量 : 4\n◎ 基本運算規則、能力\n◎ 通關條件 : \"邏輯\"\n";
		info[3] = "◎ 關卡類型數量 : 5\n◎ 基本運算規則、能力\n◎ 擬題思考能力\n◎ 通關條件 : \"推理\"";
		// info[3] = "本關卡需要基本計算能力\n加減乘除及四則運算概念\nSyuan is PIG 3";
		info[4] = "BOSS STAGE\n◎ 進階挑戰\n◎ 通關條件 : \"快速計算、精準計算\"";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
