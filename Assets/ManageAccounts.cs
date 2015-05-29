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
	public Button backButton;

	string selectedKey;
	GameObject selectedAccountButton;

	// Use this for initialization
	void Start () {
		AccountManager.accountManager.LoadKeys ();

		foreach (string key in AccountManager.keys) {
			GameObject newAccountButton = Instantiate (accountButton) as GameObject;
			newAccountButton.transform.SetParent (contentPanel);

			AccountManager.accountManager.LoadAccount (key);

			AccountButtonComponents accountButtonComponents = newAccountButton.GetComponent <AccountButtonComponents> ();
			accountButtonComponents.accountButton.colors = MenuColors.buttonWhite;
			accountButtonComponents.textUsername.text = AccountManager.account.username;
			string capturedKey = key;//directly passing key would pass key of the very last iteration
			accountButtonComponents.accountButton.onClick.AddListener (() => Select (capturedKey, newAccountButton));
		}

		createButton.colors = MenuColors.buttonYellow;
		loadButton.colors = MenuColors.buttonMagenta;
		deleteButton.colors = MenuColors.buttonWhite;
		backButton.colors = MenuColors.buttonRed;

		createButton.onClick.AddListener (() => Create ());
		loadButton.onClick.AddListener (() => Load ());
		deleteButton.onClick.AddListener (() => Delete ());
		backButton.onClick.AddListener (() => Back ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Select (string key, GameObject accountButton) {
		selectedKey = key;
		selectedAccountButton = accountButton;
	}

	void Create () {
		Application.LoadLevel ("Create Account");
	}
	
	void Load () {
		AccountManager.accountManager.LoadAccount (selectedKey);
	}
	
	void Delete () {
		AccountManager.accountManager.DeleteAccount (selectedKey);
		AccountManager.keys.Remove (selectedKey);
		AccountManager.accountManager.SaveKeys ();
		Destroy (selectedAccountButton);
	}
	
	void Back () {
		Application.LoadLevel ("Main Menu");
	}
}