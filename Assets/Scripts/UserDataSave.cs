using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserDataSave : MonoBehaviour {
	public string strurl;

	public InputField accountField, passwordField;

	private string user_name, user_passwd;

	public static string username = "";
	public static string password = "";
	public static string result = "";

	// Use this for initialization
	void Start () {
		strurl = "http://163.21.245.190/graduationProject/user/register.php";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void saveToUserDB () {
		user_name = accountField.text;
		user_passwd = passwordField.text;
		// print("[User] user_name: " + user_name + " / user_passwd: " + user_passwd);
		StartCoroutine(handlerEnterUser(user_name, user_passwd));
	}

	IEnumerator handlerEnterUser (string user, string pass) {
		string register_url = strurl + "?username=" + user + "&password=" + pass;
		WWW entereddata = new WWW (register_url);
		yield return entereddata;
		if(entereddata.text == "registered") {
			result = " correctly";
		} else {
			result = "incorrectly";
		}
		username = "";
		password= "";
    }

	
}
