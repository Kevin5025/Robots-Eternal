using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Button createAccountButton;
	public Button manageAccountsButton;
	public Button instructionsButton;
	public Button creditsButton;
	public Button quitGameButton;

	// Use this for initialization
	void Start () {
		createAccountButton.colors = MenuColors.buttonYellow;
		manageAccountsButton.colors = MenuColors.buttonYellow;
		instructionsButton.colors = MenuColors.buttonYellow;
		creditsButton.colors = MenuColors.buttonYellow;
		quitGameButton.colors = MenuColors.buttonRed;

		createAccountButton.onClick.AddListener (() => CreateAccount());
		manageAccountsButton.onClick.AddListener (() => ManageAccounts());
		instructionsButton.onClick.AddListener (() => Instructions());
		creditsButton.onClick.AddListener (() => Credits());
		quitGameButton.onClick.AddListener (() => QuitGame());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void CreateAccount () {
		Application.LoadLevel ("Create Account");
	}
	
	void ManageAccounts () {
		Application.LoadLevel ("Manage Accounts");
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
