using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateAccountMenu : MonoBehaviour {

	public InputField usernameConfirmField;
	//public Text usernameText;
	public InputField usernameInputField;
	//public Button confirmUsernameButton;
	public Button randomUsernameButton;
	public Button saveAccountButton;
	public Button manageAccountsButton;
	public Button mainMenuButton;

	// Use this for initialization
	void Start () {
		//usernameText.text = "Default";//default username

		usernameConfirmField.colors = MenuColors.cyanColor;
		usernameInputField.colors = MenuColors.cyanColor;
		//confirmUsernameButton.colors = MenuColors.whiteColor;
		randomUsernameButton.colors = MenuColors.whiteColor;
		saveAccountButton.colors = MenuColors.magentaColor;
		manageAccountsButton.colors = MenuColors.redColor;
		mainMenuButton.colors = MenuColors.redColor;

		//InputField.SubmitEvent usernameInputFieldSubmitEvent = new InputField.SubmitEvent ();
		//usernameInputFieldSubmitEvent.AddListener (ConfirmUsername);
		usernameInputField.onEndEdit.AddListener (ConfirmUsername);//onSubmit doesn't with a lowercase 'o'
		usernameInputField.characterValidation = InputField.CharacterValidation.Alphanumeric;
		//confirmUsernameButton.onClick.AddListener (() => ConfirmUsername ());
		randomUsernameButton.onClick.AddListener (() => RandomUsername ());
		saveAccountButton.onClick.AddListener (() => SaveAccount ());
		manageAccountsButton.onClick.AddListener (() => ManageAccounts ());
		mainMenuButton.onClick.AddListener (() => MainMenu ());

		AccountCanvas.accountCanvas.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void SaveAccount () {//Also selects the account
		//I also need to check if username already exists
		if (usernameConfirmField.text != "") {
			PersistenceManager.persistenceManager.account = new Account ();
			
			PersistenceManager.persistenceManager.account.username = usernameConfirmField.text;
			int keyNumber = 0;//key is arbitrary - only for recognizing by eye
			while (PersistenceManager.persistenceManager.keys.Contains (PersistenceManager.persistenceManager.account.username + keyNumber)) {
				keyNumber++;
			}
			
			PersistenceManager.persistenceManager.SaveAccount (PersistenceManager.persistenceManager.account.username + keyNumber);
			PersistenceManager.persistenceManager.keys.Add (PersistenceManager.persistenceManager.account.username + keyNumber);
			PersistenceManager.persistenceManager.SaveKeys ();
			
			AccountCanvas.accountCanvas.UpdateAccountPanel ();
			PersistenceManager.persistenceManager.hasAccount = true;
		}
	}
	
	void ConfirmUsername () {
		if (usernameInputField.text != "") {
			usernameConfirmField.text = usernameInputField.text;
		}
	}
	
	void ConfirmUsername (string dummy) {
		if (usernameInputField.text != "") {
			//usernameText.text = usernameInputField.text;
			usernameConfirmField.text = dummy;
		}
	}
	
	void RandomUsername () {
		usernameInputField.text = "Fahad";
	}

	void ManageAccounts () {
		Application.LoadLevel ("Manage Accounts Menu");
	}
	
	void MainMenu () {
		Application.LoadLevel ("Main Menu");
	}
}
