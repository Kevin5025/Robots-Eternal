using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateAccount : MonoBehaviour {

	public Button startGame;//maybe change to createAccount later
	public Image newUsername;
	public Text textNewUsername;
	public InputField inputUsername;
	public Button confirmUsername;
	public Button randomUsername;
	public Button back;

	// Use this for initialization
	void Start () {
		//PlayerPrefs.SetString ("username", "Fahad");
		//textNewUsername.text = PlayerPrefs.GetString ("username");
		AccountManager.account = new Account ();

		inputUsername.text = "Fahad";//default username
		textNewUsername.text = inputUsername.text;

		startGame.colors = MenuColors.buttonGreen;
		newUsername.color = MenuColors.buttonWhite.disabledColor;
		back.colors = MenuColors.buttonRed;
		
		startGame.onClick.AddListener (() => StartGame());
		confirmUsername.onClick.AddListener (() => ConfirmUsername());
		randomUsername.onClick.AddListener (() => RandomUsername());
		back.onClick.AddListener (() => Back());
	}
	
	// Update is called once per frame
	void Update () {
		//username = inputUsername.text;
		//textUsername.text = username;
	}
	
	void StartGame () {
		AccountManager.account.username = textNewUsername.text;

		AccountManager.usernames.Add (AccountManager.account.username);
		AccountManager.accountManager.SaveAccount ();
		AccountManager.accountManager.SaveUsernames ();
		//PlayerPrefs.DeleteKey ("username");
		PlayerPrefs.DeleteAll ();
	}
	
	void ConfirmUsername () {
		textNewUsername.text = inputUsername.text;
		//inputUsername.enabled = false;//then can only set once
	}
	
	void RandomUsername () {
		inputUsername.text = "Fahad";
	}
	
	void Back () {
		Application.LoadLevel ("Main Menu");
	}
}
