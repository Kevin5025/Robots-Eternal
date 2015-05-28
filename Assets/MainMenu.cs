using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Button startGame;
	public Button instructions;
	public Button credits;
	public Button quitGame;

	// Use this for initialization
	void Start () {
		startGame.colors = MenuColors.buttonGreen;
		instructions.colors = MenuColors.buttonYellow;
		credits.colors = MenuColors.buttonYellow;
		quitGame.colors = MenuColors.buttonRed;

		startGame.onClick.AddListener (() => StartGame());
		instructions.onClick.AddListener (() => Instructions());
		credits.onClick.AddListener (() => Credits());
		quitGame.onClick.AddListener (() => QuitGame());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void StartGame () {
		Application.LoadLevel ("Start Menu");
	}
	
	void Instructions () {
		Application.LoadLevel ("Instructions");
	}
	
	void Credits () {
		Application.LoadLevel ("Credits");
	}
	
	void QuitGame () {
		Debug.Log ("QuitGame");
		Application.Quit ();
	}
}
