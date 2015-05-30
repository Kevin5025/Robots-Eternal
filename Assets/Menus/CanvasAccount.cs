using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasAccount : MonoBehaviour {
	
	public Account selectedAccount;
	public static CanvasAccount accountCanvas;
	public Button accountButton;
	public Text accountUsernameText;
	public Image accountImage;
	public Text accountDescriptionText;

	void Awake () {
		if (accountCanvas == null) {//like a singleton
			DontDestroyOnLoad (gameObject);
			accountCanvas = this;
		} else { //if (menuColors != null)
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		selectedAccount = new Account ();

		accountButton.colors = MenuColors.buttonBlue;
		accountImage.color = MenuColors.buttonBlue.disabledColor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ResetAccountPanel () {
		accountUsernameText.text = "Account";
		accountDescriptionText.text = "You must create or load an account to view account information. \n\nYou must create or load an account to enable select level button. ";
	}

	public void UpdateAccountPanel () {
		accountUsernameText.text = AccountManager.accountManager.account.username;
		accountDescriptionText.text = "Points: " + AccountManager.accountManager.account.points;
	}
}
