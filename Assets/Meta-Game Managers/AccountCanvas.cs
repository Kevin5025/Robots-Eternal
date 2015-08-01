using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AccountCanvas : MonoBehaviour {
	
	public Account selectedAccount;
	public static AccountCanvas accountCanvas;
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

		//AccountCanvas.accountCanvas.GetComponent<Canvas> ().enabled = true;
		gameObject.GetComponent<Canvas> ().enabled = true;
	}

	// Use this for initialization
	void Start () {
		selectedAccount = new Account ();

		accountButton.colors = MenuColors.blueColor;
		accountImage.color = MenuColors.blueColor.disabledColor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ResetAccountPanel () {
		accountUsernameText.text = "Account";
		accountDescriptionText.text = "You must create or load an account to view account information. \n\nYou must create or load an account to enable select level button. ";
	}

	public void UpdateAccountPanel () {
		accountUsernameText.text = PersistenceManager.persistenceManager.account.username;
		accountDescriptionText.text = "Points: " + PersistenceManager.persistenceManager.account.points;
	}
}
