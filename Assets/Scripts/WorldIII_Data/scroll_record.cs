using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class scroll_record : MonoBehaviour {

	public static Scrollbar scrollbar;
	public static GameObject scrollbar_s_h;
	// private bool open = false;
	private float count;
	private int scrollConst;
	// Use this for initialization
	void Start () {
		scrollbar = GetComponent<Scrollbar> ();
		scrollbar_s_h = GameObject.Find("Scrollbar");
		scrollbar_s_h.SetActive(false);
		scrollConst = 0;
		// count = 
		// scrollbar.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

			if(Map1_2.history_count > 7){
				
				scrollConst = Map1_2.history_count - 7;
			// much
				// count = scrollConst * 43.44807f;
			// 21 - 22 less to much
				count = (scrollConst * 45.5f);

			}
			else{
				count = 0f;
			}
			// print(count);
	}

	public void ListScroll(RectTransform list){
        list.localPosition = new Vector3 (list.localPosition.x, (scrollbar.value * count) - 4842.5f, list.localPosition.z);////(1)
        // print(scrollbar.value * 100f - 300f);
    }
    public static void initial(){
    	scrollbar.value = 0;
    	if(Map1_2.history_count > 7){
    		scrollbar_s_h.SetActive(true);
    	}
    }
}
