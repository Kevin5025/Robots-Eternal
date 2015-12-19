using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReadInstructionsMenu : MonoBehaviour {

	public Button mainMenuButton;

	// Use this for initialization
	void Start () {
		mainMenuButton.colors = MenuColors.redColor;

		mainMenuButton.onClick.AddListener (() => MainMenu ());

		AccountCanvas.accountCanvas.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void MainMenu () {
        //Application.LoadLevel("Main Menu");
        SceneManager.LoadScene("Main Menu");
	}
}
