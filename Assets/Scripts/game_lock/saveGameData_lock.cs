using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class saveGameData_lock : MonoBehaviour {
	public string question, answer, useranswer, status, teaching, answertime, username, lockdata, locksave_url;
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
		locksave_url = "http://163.21.245.190/graduationProject/game/gamesave_lock.php";

		question = "";
		answer = "";
		useranswer = "";
		status = "";
		teaching = "";
		answertime = "";
		username = "";
		lockdata = "";

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

/*get question, answer, useranswer, status, teaching, answertime, username*/
	public void getGameData () {
		question = script_lock_testbank.question;
		answer = script_lock_testbank.answer;
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

		lockdata = question + "@" + answer + "@" + useranswer + "@" + status + "@" + teaching + "@" + Now + "@" + answertime + "@" + username;		
		// print("[SaveGameData] " + lockdata);

		StartCoroutine(handlerGameLockData(lockdata));
	}

	IEnumerator handlerGameLockData (string lockdata) {
		WWWForm form = new WWWForm();
		Dictionary<string, string> data = new Dictionary<string, string>();
		data.Add("lockdata", lockdata);

		foreach (KeyValuePair<string, string> post in data) {
			form.AddField(post.Key, post.Value);
		}

		WWW www = new WWW(locksave_url, form);
		yield return www;
		Debug.Log(www.text);
		
    }

}
