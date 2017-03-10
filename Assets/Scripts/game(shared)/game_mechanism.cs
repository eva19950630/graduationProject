using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class game_mechanism : MonoBehaviour {
	public static int teachAniControll, qid, gid;
	public static string qkind;
	public static bool gid3_isTimesup;

	// Use this for initialization
	void Start () {
		teachAniControll = 0;
	}
	
	// Update is called once per frame
	void Update () {
		gid3_isTimesup = SecondText_quickanswer.quick_isTimesup;
	}
	

/*Enter teaching scene*/
	public static void enterTeaching (int g_id) {
		gid = g_id;
		
		if (gid == 1) {
			qid = System.Convert.ToInt32(testbankDBHandler_lock.ques_id);
			qkind = testbankDBHandler_lock.ques_kind;
		} else if (gid == 2) {
			qid = System.Convert.ToInt32(testbankDBHandler_fishing.ques_id);
			qkind = testbankDBHandler_fishing.ques_kind;
		} else if (gid == 3) {
			print("[Game3] gid3_isTimesup:" + gid3_isTimesup);
			if (gid3_isTimesup)
				qid = System.Convert.ToInt32(SecondText_quickanswer.ques_id);
			else
				qid = System.Convert.ToInt32(GameController_quickanswer.qID);

			if (qid == 3)
				qkind = "A - B + C";
			else if (qid == 5)
				qkind = "A + B + C + D";
			else if (qid == 6)
				qkind = "A + B + C - D";
			else if (qid == 12)
				qkind = "A - B - C - D";
			else if (qid == 29)
				qkind = "A × B × C";
			else if (qid == 32)
				qkind = "A ÷ B ÷ C";
			else if (qid == 37)
				qkind = "A × B + C";
			else if (qid == 45)
				qkind = "A × B + C × D";
			else if (qid == 46)
				qkind = "A × B - C × D";
			else if (qid == 47)
				qkind = "A ÷ B + C ÷ D";
			else if (qid == 48)
				qkind = "A ÷ B - C ÷ D";
			else if (qid == 66)
				qkind = "A × ( B + C )";
		} else if (gid == 4) {
			qid = System.Convert.ToInt32(testbankDBHandler_balance.ques_id);
			qkind = testbankDBHandler_balance.ques_kind;
		} else if (gid == 5) {
			qid = System.Convert.ToInt32(testbankDBHandler_buying.ques_id);
			qkind = testbankDBHandler_buying.ques_kind;
		} 

		print("[Game] gid:" + gid + " / qid:" + qid + " / qkind:" + qkind);

		if (qid == 3 || qid == 5 || qid == 6 || qid == 12) {
			teachAniControll = 0;
			SceneManager.LoadScene("TeachingScene1");
		}
		else if (qid == 29 || qid == 32 || qid == 37 || qid == 45) {
			teachAniControll = 1;
			SceneManager.LoadScene("TeachingScene2");
		}
		else if (qid == 46 || qid == 47 || qid == 48 || qid == 66) {
			teachAniControll = 2;
			SceneManager.LoadScene("TeachingScene3");
		}

	}
}
