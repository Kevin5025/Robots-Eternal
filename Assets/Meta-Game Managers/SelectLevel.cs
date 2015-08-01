using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectLevel : MonoBehaviour {
	
	public Button mainMenu;
	public Button[] levels;

	// Use this for initialization
	void Start () {
		mainMenu.colors = MenuColors.redColor;
		levels[0].colors = MenuColors.yellowColor;
		
		mainMenu.onClick.AddListener (() => MainMenu ());
		for (int i=0; i<levels.Length; i++) {
			int index = i;//capture
			levels[i].onClick.AddListener(() => LevelI (index));
		}

		AccountCanvas.accountCanvas.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LevelI (int i) {
		Application.LoadLevel ("Level " + i);
	}

	void MainMenu () {
		Application.LoadLevel ("Main Menu");
	}
}
