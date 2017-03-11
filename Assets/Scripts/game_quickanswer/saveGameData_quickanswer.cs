using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class saveGameData_quickanswer : MonoBehaviour {
	public string question, answer, useranswer, status, teaching, answertime, username, quickanswerdata, quickanswersave_url;
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
		quickanswersave_url = "http://163.21.245.190/graduationProject/game/gamesave_quickanswer.php";

		question = "";
		answer = "";
		useranswer = "";
		status = "";
		teaching = "";
		answertime = "";
		username = "";
		quickanswerdata = "";
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
		question = script_quickanswer_testbank.question;
		answer = script_quickanswer_testbank.answer;
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

		quickanswerdata = question + "@" + answer + "@" + useranswer + "@" + status + "@" + teaching + "@" + Now + "@" + answertime + "@" + username;
		print("[SaveGameData] " + quickanswerdata);

		StartCoroutine(handlerGameQuickanswerData(quickanswerdata));
	}

	IEnumerator handlerGameQuickanswerData (string buyingdata) {
		WWWForm form = new WWWForm();
		Dictionary<string, string> data = new Dictionary<string, string>();
		data.Add("quickanswerdata", quickanswerdata);

		foreach (KeyValuePair<string, string> post in data) {
			form.AddField(post.Key, post.Value);
		}

		WWW www = new WWW(quickanswersave_url, form);
		yield return www;
		Debug.Log(www.text);
    }
}
