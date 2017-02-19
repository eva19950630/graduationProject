using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class testbankDBHandler_quickanswer : MonoBehaviour {
	public string strurl;

	public Text QuesText;
	public Text[] selectAnsText = new Text[4];
	
	public string[] str_arr, str_arr2;
	public List<string> ques_list = new List<string>();
	public int c = 0;

	public static string ques_id, ques_kind, Ans;
	public static bool recallRanQues;

	// Use this for initialization
	void Start () {
		strurl = "http://163.21.245.190/graduationProject/numQues(quickanswer).php";

		ques_list.Clear();
		for (int i = 0; i < str_arr.Length; i++)
			str_arr[i] = "";
		for (int i = 0; i < str_arr2.Length; i++)
			str_arr2[i] = "";
		ques_id = "";
		Ans = "";
		recallRanQues = false;

		StartCoroutine("RanQuestion");
	}
	
	// Update is called once per frame
	void Update () {
		c = GameController_quickanswer.c;
		
		if (recallRanQues) {
			showQues ();
			recallRanQues = false;
		}
		
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
		str_arr = str.Split('#');

		//print(str_arr.Length);

		for (int i = 0; i < str_arr.Length-1; i++) {
			ques_list.Add(str_arr[i]);
			//print(ques_list[i]);
		}

		showQues();	

	}

	void showQues () {
		/*for (int i = 0; i < ques_list.Count; i++) {
			if (c == i)
				str_arr2 = ques_list[i].Split('@');
		}*/

		/*for (int i = 0; i < ques_list.Count; i++)
			print(ques_list[i]);*/

		switch(c) {
			case 0:
				str_arr2 = ques_list[0].Split('@');
			break;
			case 1:
				str_arr2 = ques_list[1].Split('@');
			break;
			case 2:
				str_arr2 = ques_list[2].Split('@');
			break;
			case 3:
				str_arr2 = ques_list[3].Split('@');
			break;
			case 4:
				str_arr2 = ques_list[4].Split('@');
			break;
		}

		ques_id = str_arr2[0];
		ques_kind = str_arr2[1];
		QuesText.text = str_arr2[2];
		Ans = str_arr2[3];
		
		ranSelectAnsNum ();
	}

	void ranSelectAnsNum () {
		int r = Random.Range(0, 4);
		for (int i = 0; i < 4; i++) {
			if (r == i)
				selectAnsText[i].text = Ans;
			else
				selectAnsText[i].text = ""+ Random.Range(0, 5000);
		}
	}


	public static void reRanQues () {
		recallRanQues = true;
		
	}

}
