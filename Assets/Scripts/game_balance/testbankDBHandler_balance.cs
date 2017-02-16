using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testbankDBHandler_balance : MonoBehaviour {
public string strurl = "http://163.21.245.190/graduationProject/numberQues.php";
	public Text QuesText;
	public string[] str_arr;

	public static string ques_id, Ans, hint;

	// Use this for initialization
	void Start () {
		ques_id = "";
		Ans = "";
		hint = "";
		for (int i = 0; i < str_arr.Length; i++)
			str_arr[i] = "";
		StartCoroutine("RanQuestion");
	}
	
	// Update is called once per frame
	void Update () {
	
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
		str_arr = str.Split('@');

		ques_id = str_arr[0];
		QuesText.text = str_arr[1];
		Ans = str_arr[2];
		hint = str_arr[3];

/*PRINT Balance info*/
		Debug.Log("Balance INFO: " + "ques_id: " + ques_id + " / Ans: " + Ans + " / hint: " + hint);

	}
}
