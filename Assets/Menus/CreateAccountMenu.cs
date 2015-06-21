using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateAccountMenu : MonoBehaviour {

	public Image usernameImage;
	public Text usernameText;
	public InputField usernameInputField;
	public Button confirmUsernameButton;
	public Button randomUsernameButton;
	public Button saveAccountButton;
	public Button mainMenuButton;

	// Use this for initialization
	void Start () {
		//PlayerPrefs.SetString ("username", "Fahad");
		//textNewUsername.text = PlayerPrefs.GetString ("username");
		usernameText.text = "Default";//default username

		saveAccountButton.colors = MenuColors.magentaColor;
		usernameImage.color = MenuColors.whiteColor.disabledColor;
		usernameInputField.colors = MenuColors.cyanColor;
		confirmUsernameButton.colors = MenuColors.whiteColor;
		randomUsernameButton.colors = MenuColors.whiteColor;
		mainMenuButton.colors = MenuColors.redColor;
		
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
		PersistenceManager.persistenceManager.account = new Account ();

		PersistenceManager.persistenceManager.account.username = usernameText.text;
		int keyNumber = 0;//key is arbitrary
		while (PersistenceManager.persistenceManager.keys.Contains (PersistenceManager.persistenceManager.account.username + keyNumber)) {
			keyNumber++;
		}

		PersistenceManager.persistenceManager.SaveAccount (PersistenceManager.persistenceManager.account.username + keyNumber);

		PersistenceManager.persistenceManager.keys.Add (PersistenceManager.persistenceManager.account.username + keyNumber);
		PersistenceManager.persistenceManager.SaveKeys ();
		//PlayerPrefs.DeleteKey ("username");
		//PlayerPrefs.DeleteAll ();

		AccountCanvas.accountCanvas.UpdateAccountPanel ();
	}
	
	void ConfirmUsername () {
		usernameText.text = usernameInputField.text;
	}
	
	void RandomUsername () {
		usernameInputField.text = "Fahad";
	}
	
	void MainMenu () {
		Application.LoadLevel ("Main Menu");
	}
}
