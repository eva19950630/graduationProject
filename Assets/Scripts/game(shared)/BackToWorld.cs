using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BackToWorld : MonoBehaviour {
	//public bool gamestate;
	//public List<string> Anslist = new List<string>();

	//check boss or normal world
	public bool isboss = false;
	
	// Use this for initialization
	void Start () {
		if(GameObject.Find("BossSaveData"))
			isboss = true;
	}
	
	// Update is called once per frame
	void Update () {
		//Anslist = testbankDBHandler_fishing.Anslist;
		//gamestate = GameController_fishing.gamestate;
	}

	public void clickBack () {
		//gamestate = true;
		//Anslist.Clear();
		if(isboss)
			SceneManager.LoadScene("BossStage");
		else
			SceneManager.LoadScene("Chapter_WorldOne");
	}

}
