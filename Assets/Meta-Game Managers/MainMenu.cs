using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

	public Button selectLevelButton;
	//public Button createAccountButton;
	public Button manageAccountsButton;
	public Button readInstructionsButton;
	public Button changeSettingsButton;
	public Button viewCreditsButton;
	public Button quitGameButton;

	// Use this for initialization
	void Start () {
		selectLevelButton.colors = MenuColors.greenColor;
		//createAccountButton.colors = MenuColors.yellowColor;
		manageAccountsButton.colors = MenuColors.yellowColor;
		readInstructionsButton.colors = MenuColors.yellowColor;
		changeSettingsButton.colors = MenuColors.yellowColor;
		viewCreditsButton.colors = MenuColors.yellowColor;
		quitGameButton.colors = MenuColors.redColor;

		selectLevelButton.onClick.AddListener (() => SelectLevel ());
		//createAccountButton.onClick.AddListener (() => CreateAccount ());
		manageAccountsButton.onClick.AddListener (() => ManageAccounts ());
		readInstructionsButton.onClick.AddListener (() => ReadInstructions ());
		changeSettingsButton.onClick.AddListener (() => ChangeSettings ());
		viewCreditsButton.onClick.AddListener (() => ViewCredits ());
		quitGameButton.onClick.AddListener (() => QuitGame ());

		if (!PersistenceManager.persistenceManager.hasAccount) {
			selectLevelButton.interactable = false;
		}

		AccountCanvas.accountCanvas.gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SelectLevel () {
		//Destroy (AccountCanvas.accountCanvas.gameObject);//Done in SelectLevel.cs for visual appeal
		Application.LoadLevel ("Select Level Menu");
	}
/*
	void CreateAccount () {
		Application.LoadLevel ("Create Account Menu");
	}
*/	
	void ManageAccounts () {
		Application.LoadLevel ("Manage Accounts Menu");
	}
	
	void ReadInstructions () {
		Application.LoadLevel ("Read Instructions Menu");
	}

	void ChangeSettings () {
		Application.LoadLevel ("Change Settings Menu");
	}
	
	void ViewCredits () {
		Application.LoadLevel ("View Credits Menu");
	}
	
	void QuitGame () {
		Debug.Log ("QuitGame");
		Application.Quit ();
	}
}
