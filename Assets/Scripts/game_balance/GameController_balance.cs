using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_balance : MonoBehaviour {
	public Text weight100g_Text, weight50g_Text, weight10g_Text, weight1g_Text;

	private int weight_num1 = 0, weight_num2 = 0, weight_num3 = 0, weight_num4 = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}

/*Click weight and change number*/
	public void ChangeWeightNumber (string weightbtnName) {
		
		if (weightbtnName == "weightbtn_100g") {
			if (weight_num1 < 20)
				weight_num1++;
			else
				weight_num1 = 0;
			// print("weight_num1: " + weight_num1);
			weight100g_Text.text = ""+weight_num1;
		} else if (weightbtnName == "weightbtn_50g") {
			if (weight_num2 < 20)
				weight_num2++;
			else
				weight_num2 = 0;
			// print("weight_num2: " + weight_num2);
			weight50g_Text.text = ""+weight_num2;
		} else if (weightbtnName == "weightbtn_10g") {
			if (weight_num3 < 20)
				weight_num3++;
			else
				weight_num3 = 0;
			// print("weight_num3: " + weight_num3);
			weight10g_Text.text = ""+weight_num3;
		} else if (weightbtnName == "weightbtn_1g") {
			if (weight_num4 < 20)
				weight_num4++;
			else
				weight_num4 = 0;
			// print("weight_num4: " + weight_num4);
			weight1g_Text.text = ""+weight_num4;
		}

	}

}
