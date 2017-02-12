using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_buying : MonoBehaviour {
	public Sprite[] basketSprite = new Sprite[2];
	public SpriteRenderer[] mainren_basket = new SpriteRenderer[4];

	private bool[] isClickBasket = new bool[4];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void clickBasket(string btnName) {
		if (btnName == "selectBasket1") {
			isClickBasket[0] = true;;
		}
		else if (btnName == "selectBasket2") {
			isClickBasket[1] = true;
		}
		else if (btnName == "selectBasket3") {
			isClickBasket[2] = true;
		}
		else if (btnName == "selectBasket4") {
			isClickBasket[3] = true;
		}

/*Only take one basket*/
		for (int i = 0; i < 4; i++) {
			if (isClickBasket[i] == true) {
				mainren_basket[i].sprite = basketSprite[1];
				isClickBasket[i] = false;
			}
			else {
				mainren_basket[i].sprite = basketSprite[0];
			}
		}
	}
}
