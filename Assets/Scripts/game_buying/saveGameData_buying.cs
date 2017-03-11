using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class saveGameData_buying : MonoBehaviour {
	public string question, answer, useranswer, button, status, teaching, answertime, username, buyingdata, buyingsave_url;
	public DateTime Now;

	private bool isTimesup;

	private testbankDBHandler_buying script_buying_testbank;
	private GameController_buying script_buying_gc;
	private SecondText_buying script_buying_sec;

	void Awake () {
		script_buying_testbank = GetComponent<testbankDBHandler_buying> ();
		script_buying_gc = GetComponent<GameController_buying> ();
		script_buying_sec = GetComponent<SecondText_buying> ();
	}

	// Use this for initialization
	void Start () {
		buyingsave_url = "http://163.21.245.190/graduationProject/game/gamesave_buying.php";

		question = "";
		answer = "";
		useranswer = "";
		button = "";
		status = "";
		teaching = "";
		answertime = "";
		username = "";
		buyingdata = "";
	}
	
	// Update is called once per frame
	void Update () {
		isTimesup = script_buying_sec.isTimesup;
		// print(isTimesup);
		if (isTimesup)
			getGameData ();

		Now = DateTime.Now;
	}

/*get question, answer, useranswer, button, status, teaching, time ,answertime, username*/
	public void getGameData () {
		question = script_buying_testbank.question;
		answer = script_buying_testbank.answer;
		answertime = ""+script_buying_sec.answertime;
		username = UserDataSave.user_name;
		if (isTimesup) {
			useranswer = "-";
			button = "no";
			status = "timesup";
			teaching = "yes";
		} else {
			useranswer = script_buying_gc.useranswer;
			button = script_buying_gc.buttonname;
			status = script_buying_gc.status;
			teaching = script_buying_gc.teaching;
		}

		buyingdata = question + "@" + answer + "@" + useranswer + "@" + button + "@" + status + "@" + teaching + "@" + Now + "@" + answertime + "@" + username;
		// print("[SaveGameData] " + buyingdata);

		StartCoroutine(handlerGameBuyingData(buyingdata));
	}

	IEnumerator handlerGameBuyingData (string buyingdata) {
		WWWForm form = new WWWForm();
		Dictionary<string, string> data = new Dictionary<string, string>();
		data.Add("buyingdata", buyingdata);

		foreach (KeyValuePair<string, string> post in data) {
			form.AddField(post.Key, post.Value);
		}

		WWW www = new WWW(buyingsave_url, form);
		yield return www;
		Debug.Log(www.text);
    }
}
