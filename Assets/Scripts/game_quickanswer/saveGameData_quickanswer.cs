using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class saveGameData_quickanswer : MonoBehaviour {
	public string gamename, question, answer, useranswer, button, status, teaching, answertime, username, gamedata, gamesave_url;
	public DateTime Now;

	private bool isTimesup;

	private testbankDBHandler_quickanswer script_quickanswer_testbank;
	private GameController_quickanswer script_quickanswer_gc;
	private SecondText_quickanswer script_quickanswer_sec;

	void Awake () {
		script_quickanswer_testbank = GetComponent<testbankDBHandler_quickanswer> ();
		script_quickanswer_gc = GetComponent<GameController_quickanswer> ();
		script_quickanswer_sec = GetComponent<SecondText_quickanswer> ();
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
		isTimesup = script_quickanswer_sec.isTimesup;
		// print(isTimesup);
		if (isTimesup)
			getGameData ();

		Now = DateTime.Now;
	}

/*get question, answer, useranswer, status, teaching, time ,answertime, username*/
	public void getGameData () {
		gamename = "大家來蓋章";
		question = script_quickanswer_testbank.question;
		answer = script_quickanswer_testbank.answer;
		button = "-";
		answertime = ""+script_quickanswer_sec.answertime;
		username = UserDataSave.user_name;
		if (isTimesup) {
			useranswer = "-";
			status = "timesup";
			teaching = "yes";
		} else {
			useranswer = script_quickanswer_gc.useranswer;
			status = script_quickanswer_gc.status;
			teaching = script_quickanswer_gc.teaching;
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
