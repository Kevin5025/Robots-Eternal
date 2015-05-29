using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateAccount : MonoBehaviour {

	public Button saveAccountButton;//need to fix all these names and reassign in editor
	public Image usernameImage;
	public Text usernameText;
	public InputField usernameInputField;
	public Button confirmUsernameButton;
	public Button randomUsernameButton;
	public Button mainMenuButton;

	// Use this for initialization
	void Start () {
		//PlayerPrefs.SetString ("username", "Fahad");
		//textNewUsername.text = PlayerPrefs.GetString ("username");
		usernameText.text = "Default";//default username

		saveAccountButton.colors = MenuColors.buttonMagenta;
		usernameImage.color = MenuColors.buttonWhite.disabledColor;
		usernameInputField.colors = MenuColors.buttonCyan;
		confirmUsernameButton.colors = MenuColors.buttonWhite;
		randomUsernameButton.colors = MenuColors.buttonWhite;
		mainMenuButton.colors = MenuColors.buttonRed;
		
		saveAccountButton.onClick.AddListener (() => SaveAccount());
		confirmUsernameButton.onClick.AddListener (() => ConfirmUsername());
		randomUsernameButton.onClick.AddListener (() => RandomUsername());
		mainMenuButton.onClick.AddListener (() => MainMenu());

		AccountCanvas.accountCanvas.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//username = inputUsername.text;
		//textUsername.text = username;
	}
	
	void SaveAccount () {//Also selects the account
		//need to check if username already exists
		AccountManager.accountManager.account = new Account ();

		AccountManager.accountManager.account.username = usernameText.text;
		int keyNumber = 0;
		while (AccountManager.accountManager.keys.Contains (AccountManager.accountManager.account.username + keyNumber)) {
			keyNumber++;
		}
		AccountManager.accountManager.SaveAccount (AccountManager.accountManager.account.username + keyNumber);

		AccountManager.accountManager.keys.Add (AccountManager.accountManager.account.username + keyNumber);
		AccountManager.accountManager.SaveKeys ();
		//PlayerPrefs.DeleteKey ("username");
		//PlayerPrefs.DeleteAll ();

		AccountCanvas.accountCanvas.UpdateAccountPanel ();
	}
	
	void ConfirmUsername () {
		usernameText.text = usernameInputField.text;
		//inputUsername.enabled = false;//then can only set once
	}
	
	void RandomUsername () {
		usernameInputField.text = "Fahad";
	}
	
	void MainMenu () {
		Application.LoadLevel ("Main Menu");
	}
}
