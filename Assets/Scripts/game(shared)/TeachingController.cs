using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeachingController : MonoBehaviour {
	public Animator teachAni;
	public Text qkindText;
	public GameObject continueBtn, watchagainBtn, bgObj;
	public Material[] teaching_bg = new Material[5];

	private int tc, qid, gid;
	private string qkind;
	private bool isWait = false;

	// Use this for initialization
	void Start () {
		gid = game_mechanism.gid;
		tc = game_mechanism.teachAniControll;
		qid = game_mechanism.qid;
		qkind = game_mechanism.qkind;
		print("[TeachingScene] gid: " + gid + " / tc:" + tc + " / qid:" + qid + " / qkind:" + qkind);

		for (int i = 1; i < 6; i++) {
			if (gid == i)
				bgObj.GetComponent<Renderer>().material = teaching_bg[i-1];
		}

		qkindText.text = qkind;

		playTeachingAni ();

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
		
		if (isWait) {
			qkindText.text = qkind;
			continueBtn.SetActive(false);
			watchagainBtn.SetActive(false);
			StartCoroutine(waitingSec());
		}

	}

	void playTeachingAni () {
		isWait = true;

		if (tc == 0) {
			if (qid == 3)
				teachAni.Play("ID_3");
			else if (qid == 5)
				teachAni.Play("ID_5");
			else if (qid == 6)
				teachAni.Play("ID_6");
			else if (qid == 12)
				teachAni.Play("ID_12");
		} else if (tc == 1) {
			if (qid == 29)
				teachAni.Play("ID_29");
			else if (qid == 32)
				teachAni.Play("ID_32");
			else if (qid == 37)
				teachAni.Play("ID_37");
			else if (qid == 45)
				teachAni.Play("ID_45");
		} else if (tc == 2) {
			if (qid == 46)
				teachAni.Play("ID_46");
			else if (qid == 47)
				teachAni.Play("ID_47");
			else if (qid == 48)
				teachAni.Play("ID_48");
			else if (qid == 66)
				teachAni.Play("ID_66");
		}

	}

	IEnumerator waitingSec () {
		yield return new WaitForSeconds(4f);
		qkindText.text = "";
		isWait = false;
	}

	public void showWatchingAgainBtn () {
		watchagainBtn.SetActive(true);
	}

	public void showcontinueBtn () {
		continueBtn.SetActive(true);
	}


	public void cilckShowWatchingAgain () {
		isWait = true; 
		teachAni.enabled = true;
		playTeachingAni ();
	}

	public void stopAni () {
		teachAni.enabled = false;
	}

	
}
