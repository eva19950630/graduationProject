﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testbankDBHandler_buying : MonoBehaviour {
	public string strurl;

	public Text QuesText;

	public string[] str_arr;

	public static string ques_id, ques_kind, Ans, hint;

	// Use this for initialization
	void Start () {
		strurl = "http://163.21.245.190/graduationProject/numberQues_normal.php";

		ques_id = "";
		ques_kind = "";
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
		ques_kind = str_arr[1];
		QuesText.text = str_arr[2];
		Ans = str_arr[3];
		hint = str_arr[4];

/*PRINT Buying info*/
		Debug.Log("Buying INFO: " + "ques_id: " + ques_id + " / ques_kind: " + ques_kind + " / Ans: " + Ans + " / hint: " + hint);

	}
}