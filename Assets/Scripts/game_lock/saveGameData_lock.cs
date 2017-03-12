using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class saveGameData_lock : MonoBehaviour {
	public string gamename, question, answer, useranswer, button, status, teaching, answertime, username, gamedata, gamesave_url;
	public DateTime Now;

	private bool isTimesup;

	private testbankDBHandler_lock script_lock_testbank;
	private GameController_lock script_lock_gc;
	private SecondText_lock script_lock_sec;

	void Awake () {
		script_lock_testbank = GetComponent<testbankDBHandler_lock> ();
		script_lock_gc = GetComponent<GameController_lock> ();
		script_lock_sec = GetComponent<SecondText_lock> ();
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

		// print(UserDataSave.user_name);
	}
	
	// Update is called once per frame
	void Update () {
		isTimesup = script_lock_sec.isTimesup;
		// print(isTimesup);
		if (isTimesup)
			getGameData ();

		Now = DateTime.Now;
		// print (Now);
	}

/*get gamename, question, answer, useranswer, button, status, teaching, answertime, time, username*/
	public void getGameData () {
		gamename = "大家來解鎖";
		question = script_lock_testbank.question;
		answer = script_lock_testbank.answer;
		button = "-";
		answertime = ""+script_lock_sec.answertime;
		username = UserDataSave.user_name;
		if (isTimesup) {
			useranswer = "-";
			status = "timesup";
			teaching = "yes";
		} else {
			useranswer = script_lock_gc.useranswer;
			status = script_lock_gc.status;
			teaching = script_lock_gc.teaching;
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
