using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showRecords : MonoBehaviour {

	public string username;
	public Text data_block;
	public Text user_Ans;
	public Text corrext_wrong;
	public Text pageCount;

	public int RecordNumber;
	public int RecordCount  = 1;

	public List<string> questions = new List<string>();
	public List<string> result = new List<string>();
	public List<string> userAnswers = new List<string>();

	// Use this for initialization
	void Start () {
		username = "";
		username = UserDataSave.user_name;
		// print(username);
		// data_block.text = username;
		StartCoroutine(callRecord(username));
		// data_block.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator callRecord(string username){
		// string url = "http://163.21.245.189/test.php";
		string url = "http://163.21.245.190/graduationProject/game/gameRecord.php";
		WWWForm form = new WWWForm();
		form.AddField("username",username);
		WWW www = new WWW(url, form);
		yield return www;
		// Text = 
		// data_block.text = www.text;
		string data = www.text;
		print(data);
		string[] data_arr = data.Split('@');
		int i = 0;
		while(data_arr[i] != "" ){
			// print(data_arr[i]);
			// data_block.text += (data_arr[i] + "\n");
			questions.Add(data_arr[i]);
			result.Add(data_arr[i+1]);
			userAnswers.Add(data_arr[i+2]);
			i+=3; 
		}
		RecordNumber = ((i+1) / 3);
		pageCount.text = RecordCount + " / " + RecordNumber;
		data_block.text = questions[0];

		if(result[0] == "right")
			corrext_wrong.text = "O";
		else
			corrext_wrong.text = "X";
		// corrext_wrong.text = result[0];

		if(userAnswers[0] != "null")
			user_Ans.text = userAnswers[RecordCount-1];
		else
			user_Ans.text = "未作答";

		// print(www.text);
	}

	public void arrowClick(int clickConst){
		if(clickConst == 1){
			if(RecordCount < RecordNumber)
				RecordCount++;
		}else if(clickConst == 0){
			if(RecordCount > 1)
				RecordCount--;
		}
		//change data
		pageCount.text = RecordCount + " / " + RecordNumber;
		data_block.text = questions[RecordCount-1];
		
		if(result[RecordCount-1] == "right")
			corrext_wrong.text = "O";
		else
			corrext_wrong.text = "X";
		// corrext_wrong.text = result[RecordCount-1];
		// print()
		if(userAnswers[RecordCount-1] != "null")
			user_Ans.text = userAnswers[RecordCount-1];
		else
			user_Ans.text = "未作答";			
	}
}
