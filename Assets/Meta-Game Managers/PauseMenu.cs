using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public static PauseMenu pauseMenu;

	public Canvas pauseMenuCanvas;
	public Button resumeButton;
	public Button quitButton;

	void Awake () {
		if (pauseMenu == null) {
			DontDestroyOnLoad (gameObject);
			pauseMenu = this;
		} else {
			Destroy (gameObject);
		}

		gameObject.GetComponent<Canvas> ().enabled = true;
	}

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
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (!pauseMenuCanvas.enabled) {
				Pause ();
			} else {
				Resume ();
			}
			//Cursor.lockState = false;
			//Cursor.visible = true;
		}
	}

	void Pause () {
		Time.timeScale = 0;
		pauseMenuCanvas.enabled = true;
	}
	
	void Resume () {
		Time.timeScale = 1;
		pauseMenuCanvas.enabled = false;
	}

	void Quit () {
		PauseMenu.pauseMenu.gameObject.SetActive (false);
        //Application.LoadLevel("Main Menu");//doesn't destroy everything
        SceneManager.LoadScene("Main Menu");
	}
}
