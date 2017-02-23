using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserDataSave : MonoBehaviour {
	public string strurl;

	public InputField accountField, passwordField;
	public GameObject warningPanel, loadingImage, accountObj, passwordObj, accountLabel, passwordLabel;
	public Slider loadingBar;

	private AsyncOperation async;
	private string user_name, user_passwd;

	public static string username = "";
	public static string password = "";
	public static string result = "";

	// public static bool account = false;

	// Use this for initialization
	void Start () {
		strurl = "http://163.21.245.190/graduationProject/user/register.php";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void clickSureBtn () {
		warningPanel.SetActive(false);
	}

	public void saveToUserDB () {
		if (accountField.text == "" || passwordField.text == "") {
			print("nothing");
			warningPanel.SetActive(true);
		} else {
			user_name = accountField.text;
			user_passwd = passwordField.text;
			accountObj.SetActive(false);
			passwordObj.SetActive(false);
			accountLabel.SetActive(false);
			passwordLabel.SetActive(false);
			loadingImage.SetActive (true);
			StartCoroutine(handlerEnterUser(user_name, user_passwd));
		}
		// print("[User] user_name: " + user_name + " / user_passwd: " + user_passwd);
	}

	IEnumerator handlerEnterUser (string user, string pass) {
		WWWForm form = new WWWForm();
		Dictionary<string, string> data = new Dictionary<string, string>();
		data.Add("username", user);
		data.Add("password", pass);

		foreach (KeyValuePair<string, string> post in data) {
			form.AddField(post.Key, post.Value);
		}

		WWW www = new WWW(strurl, form);
		yield return www;
		Debug.Log(www.text);
		StartCoroutine (LoadAndBar ("menu"));
    }

    
	IEnumerator LoadAndBar (string scene) {

		float sProgress = 0f;
		async = SceneManager.LoadSceneAsync(scene);
		async.allowSceneActivation = false;
		while(async.progress < 0.9f){
			while(sProgress < async.progress){
				sProgress += 0.01f;
				loadingBar.value = sProgress;
				yield return 0;
			}
		}
		while(sProgress < 1f){
				sProgress += 0.01f;
				loadingBar.value = sProgress;
				yield return 0;
		}
		async.allowSceneActivation = true;
	}

	
}
