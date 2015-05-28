using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	
	string username;
	public UnityEngine.UI.Button newUsername;
	public UnityEngine.UI.Text textNewUsername;
	public UnityEngine.UI.InputField inputUsername;
	public UnityEngine.UI.Button confirmUsername;
	public UnityEngine.UI.Button randomUsername;
	public UnityEngine.UI.Button back;

	// Use this for initialization
	void Start () {
		username = "Fahad";

		newUsername.colors = MenuColors.buttonGreen;
		back.colors = MenuColors.buttonRed;
		
		newUsername.onClick.AddListener (() => NewUsername());
		confirmUsername.onClick.AddListener (() => ConfirmUsername());
		randomUsername.onClick.AddListener (() => RandomUsername());
		back.onClick.AddListener (() => Back());
	}
	
	// Update is called once per frame
	void Update () {
		//username = inputUsername.text;
		//textUsername.text = username;
	}
	
	void NewUsername () {
		
	}
	
	void ConfirmUsername () {
		username = inputUsername.text;
		textNewUsername.text = username;
		//inputUsername.enabled = false;//can only set once
	}
	
	void RandomUsername () {
		inputUsername.text = "Fahad";
	}
	
	void Back () {
		Application.LoadLevel ("Main Menu");
	}
}
