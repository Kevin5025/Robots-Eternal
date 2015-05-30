using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Button selectLevelButton;
	public Button createAccountButton;
	public Button manageAccountsButton;
	public Button readInstructionsButton;
	public Button viewCreditsButton;
	public Button quitGameButton;

	// Use this for initialization
	void Start () {
		selectLevelButton.colors = MenuColors.buttonGreen;
		createAccountButton.colors = MenuColors.buttonYellow;
		manageAccountsButton.colors = MenuColors.buttonYellow;
		readInstructionsButton.colors = MenuColors.buttonYellow;
		viewCreditsButton.colors = MenuColors.buttonYellow;
		quitGameButton.colors = MenuColors.buttonRed;

		selectLevelButton.onClick.AddListener (() => SelectLevel ());
		createAccountButton.onClick.AddListener (() => CreateAccount ());
		manageAccountsButton.onClick.AddListener (() => ManageAccounts ());
		readInstructionsButton.onClick.AddListener (() => ReadInstructions ());
		viewCreditsButton.onClick.AddListener (() => ViewCredits ());
		quitGameButton.onClick.AddListener (() => QuitGame ());

		if (AccountManager.accountManager.account == null) {
			selectLevelButton.interactable = false;
		}

		CanvasAccount.accountCanvas.gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SelectLevel () {
		Application.LoadLevel ("Select Level");
	}

	void CreateAccount () {
		Application.LoadLevel ("Create Account");
	}
	
	void ManageAccounts () {
		Application.LoadLevel ("Manage Accounts");
	}
	
	void ReadInstructions () {
		Application.LoadLevel ("Read Instructions");
	}
	
	void ViewCredits () {
		Application.LoadLevel ("View Credits");
	}
	
	void QuitGame () {
		Debug.Log ("QuitGame");
		Application.Quit ();
	}
}
