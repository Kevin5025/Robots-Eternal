using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

	public static PauseMenuManager pauseMenuManager;

	public Canvas pauseMenuCanvas;
	public Button resumeButton;
	public Button quitButton;

	void Awake () {
		if (pauseMenuManager == null) {
			DontDestroyOnLoad (gameObject);
			pauseMenuManager = this;
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
		
	}

    public void OpenClosePauseMenu(bool input) {
        if (input) {
            if (!pauseMenuCanvas.enabled) {
                Pause();
            } else {
                Resume();
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
		PauseMenuManager.pauseMenuManager.gameObject.SetActive (false);
        //Application.LoadLevel("Main Menu");//doesn't destroy everything
        SceneManager.LoadScene("Main Menu");
	}
}
