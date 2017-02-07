using UnityEngine;
using System.Collections;

public class savedataII : MonoBehaviour {

	public static savedataII saver;
	public static bool gameEntered, comeback;

	// Use this for initialization
	void Start () {
		//gameEntered = false;
		comeback = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameventII.world_finish){			
			// Destroy(this);
			Destroy(this.gameObject);
			// print("kill save");
		}
	}

	void Awake(){

		if(saver){
			DestroyImmediate(gameObject);
			comeback = true;
		}else{
		 	DontDestroyOnLoad(transform.gameObject);
		 	saver = this;
		}

	}

	public static void newround(){
		gameEntered = false;
	}

	public static void entergame(){
		gameEntered = true;
	}

}
