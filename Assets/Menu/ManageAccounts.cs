using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ManageAccounts : MonoBehaviour {
	
	public Transform contentPanel;
	public GameObject accountButton;

	public Button createButton;
	public Button loadButton;
	public Button deleteButton;
	public Button mainMenuButton;

	string selectedKey;
	GameObject selectedAccountButton;
	Button selectedAccountButtonComponent;

	// Use this for initialization
	void Start () {
		AccountManager.accountManager.LoadKeys ();

		Account temp = AccountManager.accountManager.account;
		foreach (string key in AccountManager.accountManager.keys) {
			GameObject newAccountButton = Instantiate (accountButton) as GameObject;
			newAccountButton.transform.SetParent (contentPanel);

			AccountManager.accountManager.LoadAccount (key);

			AccountButtonComponents accountButtonComponents = newAccountButton.GetComponent <AccountButtonComponents> ();
			accountButtonComponents.accountButton.colors = MenuColors.buttonWhite;
			accountButtonComponents.textUsername.text = AccountManager.accountManager.account.username;
			string capturedKey = key;//directly passing key would pass key of the very last iteration
			accountButtonComponents.accountButton.onClick.AddListener (() => Select (capturedKey, newAccountButton, accountButtonComponents.accountButton));
		}
		AccountManager.accountManager.account = temp;

		createButton.colors = MenuColors.buttonYellow;
		loadButton.colors = MenuColors.buttonMagenta;
		deleteButton.colors = MenuColors.buttonWhite;
		mainMenuButton.colors = MenuColors.buttonRed;
		
		loadButton.interactable = false;
		deleteButton.interactable = false;

		createButton.onClick.AddListener (() => Create ());
		loadButton.onClick.AddListener (() => Load ());
		deleteButton.onClick.AddListener (() => Delete ());
		mainMenuButton.onClick.AddListener (() => MainMenu ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Select (string key, GameObject accountButton, Button accountButtonComponent) {
		if (selectedAccountButtonComponent != null) {
			selectedAccountButtonComponent.colors = MenuColors.buttonWhite;
		}
		accountButtonComponent.colors = MenuColors.buttonCyan;

		selectedKey = key;
		selectedAccountButton = accountButton;
		selectedAccountButtonComponent = accountButtonComponent;
		
		loadButton.interactable = true;
		deleteButton.interactable = true;
	}

	void Create () {
		Application.LoadLevel ("Create Account");
	}
	
	void Load () {
		AccountManager.accountManager.LoadAccount (selectedKey);
		AccountCanvas.accountCanvas.UpdateAccountPanel ();
	}
	
	void Delete () {
		AccountManager.accountManager.DeleteAccount (selectedKey);
		AccountManager.accountManager.keys.Remove (selectedKey);
		AccountManager.accountManager.SaveKeys ();
		Destroy (selectedAccountButton);
	}
	
	void MainMenu () {
		Application.LoadLevel ("Main Menu");
	}
}