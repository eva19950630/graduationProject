using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class selectCh : MonoBehaviour {

	public Slider loadingBar;
	public GameObject loadingImage;
	private AsyncOperation async;

	//public Panel panelBoard;
	public GameObject panelImage;
	public GameObject panelhome;
	public Text txtPaneln;
	public Text txtPaneli;

	//move pig
	public GameObject player;

	// public bool mfinish;

	//world button
	public static Vector3[] button = new []
	{
		new Vector3(0,0,0),
		new Vector3(0,0,0),
		new Vector3(0,0,0),
		new Vector3(0,0,0),
		new Vector3(0,0,0)
	};

	public Button home;
	public Button button0;
	public Button button1;
	public Button button2;
	public Button button3;

	//selected world
	public int wSel;
	//distingush home and world
	public bool ishome;

	// Use this for initialization
	void Start () {
		//get panel
		panelImage = GameObject.Find("Panel_world");
		panelImage.SetActive(false);
		panelhome = GameObject.Find("Panel_home");
		panelhome.SetActive(false);

		//don't show panel
		// wait = false;
		//get button position
		// button = Button.FindGameObjectsWithTag("world").OrderBy( go => go.name ).ToArray();
		// button = GetComponentsInChildren<Button>();
		button[0] = (home.transform.position + new Vector3(0, 23f, 0));
		button[1] = (button0.transform.position + new Vector3(0, 23f, 0));
		button[2] = (button1.transform.position + new Vector3(0, 23f, 0));
		button[3] = (button2.transform.position + new Vector3(0, 23f, 0));
		button[4] = (button3.transform.position + new Vector3(0, 23f, 0));
		//initial character position
		player.transform.position = button[0];
		// player.transform.position += new Vector3(0f, 16.5f, 0f);

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void homeClick(){
		home.interactable = false;
		button0.interactable = false;
		button1.interactable = false;
		button2.interactable = false;
		button3.interactable = false;	
		ishome = true;
		player.transform.position = button[0];
		StartCoroutine (WAIT());
	}

	public void continueGame(){
		panelhome.SetActive(false);
		home.interactable = true;
		button0.interactable = true;
		button1.interactable = true;
		button2.interactable = true;
		button3.interactable = true;	
	}

	public void backtomenu(){
		 SceneManager.LoadScene("menu");
	}

	public void chooseWorld(string name){	
		txtPaneln.text = name;

	}

	public void chooseW(int wNum){
		wSel = wNum;
		txtPaneli.text = Wmap.info[wNum];

		home.interactable = false;
		button0.interactable = false;
		button1.interactable = false;
		button2.interactable = false;
		button3.interactable = false;	
		player.transform.position = button[wNum];
		// player.transform.position += new Vector3(0f, 16.5f, 0f);
		// wait = true;
		StartCoroutine (WAIT());
	}

	public void wYes(){
		panelImage.SetActive(false);
		loadingImage.SetActive (true);
		if(wSel == 4)
			StartCoroutine (LoadAndBar ("BossStage"));
		else if(wSel == 3)
			StartCoroutine (LoadAndBar ("Chapter_WorldThree"));
		else if(wSel == 2)
			StartCoroutine (LoadAndBar ("Chapter_WorldTwo"));
		else
			StartCoroutine (LoadAndBar ("Chapter_WorldOne"));
	}

	public void wNo(){
		panelImage.SetActive(false);
		home.interactable = true;
		button0.interactable = true;
		button1.interactable = true;
		button2.interactable = true;
		button3.interactable = true;	
		// for(int i = 0; i < 4; i++){
		// 	button[i].interactable = true;
		// }
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

	IEnumerator WAIT(){
		yield return new WaitForSeconds(1f);
		if(!ishome)
			panelImage.SetActive(true);			
		else
			panelhome.SetActive(true);
		ishome = false;
	}
}