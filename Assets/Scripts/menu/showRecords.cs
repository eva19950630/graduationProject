using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showRecords : MonoBehaviour {

	public string username;
	public Text data_block;
	// Use this for initialization
	void Start () {
		username = "";
		username = UserDataSave.user_name;
		// print(username);
		data_block.text = username;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
