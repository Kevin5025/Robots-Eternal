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
	public Button backButton;

	// Use this for initialization
	void Start () {
		//PlayerPrefs.SetString ("username", "Fahad");
		//textNewUsername.text = PlayerPrefs.GetString ("username");
		usernameText.text = "Fahad";//default username

		saveAccountButton.colors = MenuColors.buttonMagenta;
		usernameImage.color = MenuColors.buttonWhite.disabledColor;
		usernameInputField.colors = MenuColors.buttonCyan;
		confirmUsernameButton.colors = MenuColors.buttonWhite;
		randomUsernameButton.colors = MenuColors.buttonWhite;
		backButton.colors = MenuColors.buttonRed;
		
		saveAccountButton.onClick.AddListener (() => SaveAccount());
		confirmUsernameButton.onClick.AddListener (() => ConfirmUsername());
		randomUsernameButton.onClick.AddListener (() => RandomUsername());
		backButton.onClick.AddListener (() => Back());
	}
	
	// Update is called once per frame
	void Update () {
		//username = inputUsername.text;
		//textUsername.text = username;
	}
	
	void SaveAccount () {
		//need to check if username already exists
		AccountManager.account = new Account ();

		AccountManager.account.username = usernameText.text;
		int keyNumber = 0;
		while (AccountManager.keys.Contains (AccountManager.account.username + keyNumber)) {
			keyNumber++;
		}
		AccountManager.keys.Add (AccountManager.account.username + keyNumber);
		AccountManager.accountManager.SaveAccount (AccountManager.account.username + keyNumber);
		AccountManager.accountManager.SaveKeys ();
		//PlayerPrefs.DeleteKey ("username");
		//PlayerPrefs.DeleteAll ();
	}
	
	void ConfirmUsername () {
		usernameText.text = usernameInputField.text;
		//inputUsername.enabled = false;//then can only set once
	}
	
	void RandomUsername () {
		usernameInputField.text = "Fahad";
	}
	
	void Back () {
		Application.LoadLevel ("Main Menu");
	}
}
