using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReadInstructions : MonoBehaviour {

	public Button mainMenuButton;

	// Use this for initialization
	void Start () {
		mainMenuButton.colors = MenuColors.buttonRed;

		mainMenuButton.onClick.AddListener (() => MainMenu ());

		AccountCanvas.accountCanvas.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void MainMenu () {
		Application.LoadLevel ("Main Menu");
	}
}
