using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class scroll_record : MonoBehaviour {

	private Scrollbar scrollbar;

	// Use this for initialization
	void Start () {
		scrollbar = GetComponent<Scrollbar> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void ListScroll(RectTransform list){
        list.localPosition = new Vector3 (list.localPosition.x, scrollbar.value * 600f - 300f, list.localPosition.z);////(1)
    }
}
