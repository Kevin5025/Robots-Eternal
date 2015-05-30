using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectLevel : MonoBehaviour {
	
	public Button mainMenu;
	public Button[] levels;

	// Use this for initialization
	void Start () {
		mainMenu.colors = MenuColors.buttonRed;
		levels[0].colors = MenuColors.buttonYellow;
		
		mainMenu.onClick.AddListener (() => MainMenu ());
		for (int i=0; i<levels.Length; i++) {
			levels[i].onClick.AddListener(() => LevelI (i));
		}
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
