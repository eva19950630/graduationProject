using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Butt : MonoBehaviour {

	public Slider loadingBar;
	public GameObject loadingImage, accountObj, passwordObj, accountLabel, passwordLabel;
	public InputField accountField, passwordField;

	private AsyncOperation async;

	// Use this for initialization
	void Start () {
		accountField.text = "admin";
		passwordField.text = "1234";
	}
	
	// Update is called once per frame
	void Update () {

	}

	// public void LoadNewSource () {
	// 	loadingImage.SetActive (true);
	// 	StartCoroutine (LoadAndBar ("menu"));
	// }

	public void loginCheck () {
		if ((accountField.text == "admin") && (passwordField.text == "1234")) {
			loadingImage.SetActive (true);
			StartCoroutine (LoadAndBar ("menu"));
		}	
	}


	IEnumerator LoadAndBar (string scene) {
		accountObj.SetActive(false);
		passwordObj.SetActive(false);
		accountLabel.SetActive(false);
		passwordLabel.SetActive(false);

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