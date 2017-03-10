using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserDataSave : MonoBehaviour {
	public string strurl;

	public InputField accountField, passwordField;
	public GameObject warningPanel, loadingImage, accountObj, passwordObj, accountLabel, passwordLabel;
	public Text warningText;
	public Slider loadingBar;

	private AsyncOperation async;

/*Pass data*/
	public static string user_name, user_passwd;

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
		user_name = accountField.text;
		user_passwd = passwordField.text;
		StartCoroutine(handlerEnterUser(user_name, user_passwd));
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

		if (www.text == "login failed") {
			warningPanel.SetActive(true);
		} else {
			if (www.text == user + " login" || www.text == user + " registered") {
				accountObj.SetActive(false);
				passwordObj.SetActive(false);
				accountLabel.SetActive(false);
				passwordLabel.SetActive(false);
				loadingImage.SetActive (true);
				StartCoroutine (LoadAndBar ("menu"));
			} else if (www.text == user + " wrong password") {
				warningPanel.SetActive(true);
				warningText.text = "密碼輸入錯誤哦！";
			}
		}
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
