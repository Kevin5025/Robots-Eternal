using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	
	public Canvas pauseMenuCanvas;
	public Button resumeButton;
	public Button quitButton;
	
	// Use this for initialization
	void Start () {
		resumeButton.colors = MenuColors.greenColor;
		quitButton.colors = MenuColors.redColor;

		resumeButton.onClick.AddListener (() => Resume ());
		quitButton.onClick.AddListener (() => Quit ());

		pauseMenuCanvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Time.timeScale = 0;
			pauseMenuCanvas.enabled = true;
			//Cursor.lockState = false;
			//Cursor.visible = true;
		}
	}
	
	void Resume () {
		Time.timeScale = 1;
		pauseMenuCanvas.enabled = false;
	}

	void Quit () {
		Application.LoadLevel ("Main Menu");
	}
}
