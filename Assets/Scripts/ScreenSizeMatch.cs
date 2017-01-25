﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenSizeMatch : MonoBehaviour {
	void Awake(){

	    CanvasScaler canvasScaler = GetComponent<CanvasScaler>();

	    float screenWidthScale = Screen.width / canvasScaler.referenceResolution.x;
	    float screenHeightScale = Screen.height / canvasScaler.referenceResolution.y;
	  
	    canvasScaler.matchWidthOrHeight = screenWidthScale > screenHeightScale ? 1 : 0;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
