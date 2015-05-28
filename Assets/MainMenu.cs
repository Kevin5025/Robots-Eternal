using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Button createAccount;
	public Button manageAccounts;
	public Button instructions;
	public Button credits;
	public Button quitGame;

	// Use this for initialization
	void Start () {
		createAccount.colors = MenuColors.buttonGreen;
		manageAccounts.colors = MenuColors.buttonYellow;
		instructions.colors = MenuColors.buttonYellow;
		credits.colors = MenuColors.buttonYellow;
		quitGame.colors = MenuColors.buttonRed;

		createAccount.onClick.AddListener (() => CreateAccount());
		manageAccounts.onClick.AddListener (() => ManageAccounts());
		instructions.onClick.AddListener (() => Instructions());
		credits.onClick.AddListener (() => Credits());
		quitGame.onClick.AddListener (() => QuitGame());
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
