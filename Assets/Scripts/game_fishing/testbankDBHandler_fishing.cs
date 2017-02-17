using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class testbankDBHandler_fishing : MonoBehaviour {
	public string strurl = "http://163.21.245.190/graduationProject/aq_showques(fishing).php";
	public Text QuesText, hintText;
	public string[] str_arr, hints;
	public string a, hints2;
	public int k;
	public bool isWaitOneSec = true;
	public GameObject hintObj;

	public static string ques_id, ques_kind;
	public static string[] fishNum = new string[10];
	public static List<string> Anslist = new List<string>();

	
	// Use this for initialization
	void Start () {
		ques_id = "";
		ques_kind = "";
		for (int i = 0; i < 10; i++)
			fishNum[i] = "";
		Anslist.Clear();
		StartCoroutine("RanQuestion");
	}
	
	// Update is called once per frame
	void Update () {
		
		k = GameController_fishing.k;
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();

	}

	public IEnumerator RanQuestion () {
		WWWForm form = new WWWForm();
		Dictionary<string, string> data = new Dictionary<string, string>();
		data.Add("download", "1");
		foreach (KeyValuePair<string, string> post in data) {
			form.AddField(post.Key, post.Value);
		}
		WWW www = new WWW(strurl, form);
		yield return www;
		Debug.Log("testbank: " + www.text);

		string str = www.text;
		string[] str_arr2 = str.Split('%');
		str_arr = str_arr2[0].Split(' ');

		ques_id = str_arr[0];
		ques_kind = str_arr2[1];
		// print(ques_kind);
		a = str_arr[1];	

		QuesText.text = str_arr[2];
		hints = str_arr[3].Split('@');
		//Debug.Log("hints: " + hints[0]);

		hints2 = str_arr[4];


		for (int i = 5; i < 15; i++)
			fishNum[i-5] = str_arr[i];
		
		//Debug.Log(str_arr[15]);

		for (int i = 15; i < str_arr.Length; i++) {
			string str1 = str_arr[i];
			Anslist.Add(str1);
			Debug.Log("Ans[" + (i-15) + "]: " + Anslist[i-15]);
		}


/*PRINT Fishing info*/		
		// Debug.Log("Fishing INFO: " + "ques_id: " + ques_id + " / a: " + a + " / hints.Length: " + hints.Length + " / hints2: " + hints2 + " / Anslist.Count: " + Anslist.Count);


	}


	public void StepHints () {
		if(isWaitOneSec) {
			StartCoroutine(waitingOneSec());
			isWaitOneSec = false;
		}

		/*int i = "字串測試字串測試".IndexOf("黑");
		print(i);*/

	}

	IEnumerator waitingOneSec () {
		yield return new WaitForSeconds(1f);
		isWaitOneSec = true;
		

		if (k == 1) {
			for (int i = 0; i < hints.Length; i++) {
				int scan = str_arr[2].IndexOf(hints[i]);
				if (scan != -1)
					str_arr[2] = str_arr[2].Replace(hints[i], "<color=red>"+hints[i]+"</color>");
			}

			QuesText.text = str_arr[2];

		} else if (k == 2) {
			hintObj.SetActive(true);
			hintText.text = hints2;
		}
		
		
	}


}
