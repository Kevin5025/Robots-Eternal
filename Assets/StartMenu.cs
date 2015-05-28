using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	
	string username;
	public UnityEngine.UI.Text textUsername;
	public UnityEngine.UI.InputField inputUsername;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//username = inputUsername.text;
		//textUsername.text = username;
	}
	
	public void RandomUsername () {
		inputUsername.text = "Fahad";
	}
	
	public void ConfirmUsername () {
		username = inputUsername.text;
		textUsername.text = username;
		//inputUsername.enabled = false;//can only set once
	}
	
	public void Back () {
		Application.LoadLevel (0);
	}
}
