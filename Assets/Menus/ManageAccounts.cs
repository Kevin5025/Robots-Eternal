using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ManageAccounts : MonoBehaviour {
	
	public Transform contentPanel;
	public GameObject buttonAccount;

	public Button createButton;
	public Button loadButton;
	public Button deleteButton;
	public Button mainMenuButton;

	string selectedKey;
	GameObject selectedButtonAccount;
	Button selectedAccountButton;

	// Use this for initialization
	void Start () {
		AccountManager.accountManager.LoadKeys ();

		Account temp = AccountManager.accountManager.account;
		foreach (string key in AccountManager.accountManager.keys) {
			GameObject newAccountButton = Instantiate (buttonAccount) as GameObject;
			newAccountButton.transform.SetParent (contentPanel);

			AccountManager.accountManager.LoadAccount (key);

			ButtonAccount accountButton = newAccountButton.GetComponent <ButtonAccount> ();
			accountButton.accountButton.colors = MenuColors.buttonWhite;
			accountButton.textUsername.text = AccountManager.accountManager.account.username;
			string capturedKey = key;//directly passing key would pass key of the very last iteration
			accountButton.accountButton.onClick.AddListener (() => Select (capturedKey, newAccountButton, accountButton.accountButton));
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

	void Select (string key, GameObject buttonAccount, Button accountButton) {
		if (selectedAccountButton != null) {
			selectedAccountButton.colors = MenuColors.buttonWhite;
		}
		accountButton.colors = MenuColors.buttonCyan;

		selectedKey = key;
		selectedButtonAccount = buttonAccount;
		selectedAccountButton = accountButton;
		
		loadButton.interactable = true;
		deleteButton.interactable = true;
	}

	void Create () {
		Application.LoadLevel ("Create Account");
	}
	
	void Load () {
		AccountManager.accountManager.LoadAccount (selectedKey);
		CanvasAccount.accountCanvas.UpdateAccountPanel ();
	}
	
	void Delete () {
		AccountManager.accountManager.DeleteAccount (selectedKey);
		AccountManager.accountManager.keys.Remove (selectedKey);
		AccountManager.accountManager.SaveKeys ();
		Destroy (selectedButtonAccount);
		loadButton.interactable = false;
		deleteButton.interactable = false;
		CanvasAccount.accountCanvas.ResetAccountPanel ();
	}
	
	void MainMenu () {
		Application.LoadLevel ("Main Menu");
	}
}