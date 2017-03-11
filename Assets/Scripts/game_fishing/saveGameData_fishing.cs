using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class saveGameData_fishing : MonoBehaviour {
	public string question, answer, useranswer, button, status, teaching, answertime, username, fishingdata, fishingsave_url;
	public DateTime Now;

	private bool isTimesup;

	private testbankDBHandler_fishing script_fishing_testbank;
	private GameController_fishing script_fishing_gc;
	private SecondText_fishing script_fishing_sec;

	void Awake () {
		script_fishing_testbank = GetComponent<testbankDBHandler_fishing> ();
		script_fishing_gc = GetComponent<GameController_fishing> ();
		script_fishing_sec = GetComponent<SecondText_fishing> ();
	}

	// Use this for initialization
	void Start () {
		fishingsave_url = "http://163.21.245.190/graduationProject/game/gamesave_fishing.php";

		question = "";
		answer = "";
		useranswer = "";
		button = "";
		status = "";
		teaching = "";
		answertime = "";
		username = "";
		fishingdata = "";
	}
	
	// Update is called once per frame
	void Update () {
		isTimesup = script_fishing_sec.isTimesup;
		if (isTimesup)
			getGameData ();

		Now = DateTime.Now;
		// print (Now);
	}

/*get question, answer, useranswer, button, status, teaching, time ,answertime, username*/
	public void getGameData () {
		question = script_fishing_testbank.question;
		answer = script_fishing_testbank.answer;
		answertime = ""+script_fishing_sec.answertime;
		username = UserDataSave.user_name;
		if (isTimesup) {
			useranswer = "-";
			button = "no";
			status = "timesup";
			teaching = "yes";
		} else {
			useranswer = script_fishing_gc.useranswer;
			button = script_fishing_gc.buttonname;
			status = script_fishing_gc.status;
			teaching = script_fishing_gc.teaching;
		}

		fishingdata = question + "@" + answer + "@" + useranswer + "@" + button + "@" + status + "@" + teaching + "@" + Now + "@" + answertime + "@" + username;
		// print("[SaveGameData] " + fishingdata);

		StartCoroutine(handlerGameFishingData(fishingdata));
	}

	IEnumerator handlerGameFishingData (string fishingdata) {
		WWWForm form = new WWWForm();
		Dictionary<string, string> data = new Dictionary<string, string>();
		data.Add("fishingdata", fishingdata);

		foreach (KeyValuePair<string, string> post in data) {
			form.AddField(post.Key, post.Value);
		}

		WWW www = new WWW(fishingsave_url, form);
		yield return www;
		Debug.Log(www.text);
		
    }
}
