using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class saveGameData_balance : MonoBehaviour {
	public string gamename, question, answer, useranswer, button, status, teaching, answertime, username, gamedata, gamesave_url;
	public DateTime Now;

	private bool isTimesup;

	private testbankDBHandler_balance script_balance_testbank;
	private GameController_balance script_balance_gc;
	private SecondText_balance script_balance_sec;	

	void Awake () {
		script_balance_testbank = GetComponent<testbankDBHandler_balance> ();
		script_balance_gc = GetComponent<GameController_balance> ();
		script_balance_sec = GetComponent<SecondText_balance> ();
	}

	// Use this for initialization
	void Start () {
		gamesave_url = "http://163.21.245.190/graduationProject/game/gamesave.php";

		gamename = "";
		question = "";
		answer = "";
		useranswer = "";
		button = "";
		status = "";
		teaching = "";
		answertime = "";
		username = "";
		gamedata = "";
	}
	
	// Update is called once per frame
	void Update () {
		isTimesup = script_balance_sec.isTimesup;
		// print(isTimesup);
		if (isTimesup)
			getGameData ();

		Now = DateTime.Now;
	}

/*get question, answer, useranswer, button, status, teaching, time ,answertime, username*/
	public void getGameData () {
		gamename = "大家來平衡";
		question = script_balance_testbank.question;
		answer = script_balance_testbank.answer;
		answertime = ""+script_balance_sec.answertime;
		username = UserDataSave.user_name;
		if (isTimesup) {
			useranswer = "-";
			button = "no click";
			status = "timesup";
			teaching = "yes";
		} else {
			useranswer = script_balance_gc.useranswer;
			button = script_balance_gc.buttonname;
			status = script_balance_gc.status;
			teaching = script_balance_gc.teaching;
		}

		gamedata = gamename + "@" + question + "@" + answer + "@" + useranswer + "@" + button + "@" + status + "@" + teaching + "@" + answertime + "@" + Now + "@" + username;		
		// print("[SaveGameData] " + gamedata);

		StartCoroutine(handlerGameData(gamedata));
	}

	IEnumerator handlerGameData (string gamedata) {
		WWWForm form = new WWWForm();
		Dictionary<string, string> data = new Dictionary<string, string>();
		data.Add("gamedata", gamedata);

		foreach (KeyValuePair<string, string> post in data) {
			form.AddField(post.Key, post.Value);
		}

		WWW www = new WWW(gamesave_url, form);
		yield return www;
		Debug.Log(www.text);
    }
}
