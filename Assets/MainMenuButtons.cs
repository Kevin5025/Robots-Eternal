using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {
	
	string username;
	public UnityEngine.UI.InputField inputUsername;
	public UnityEngine.UI.Text textUsername;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//username = inputUsername.text;
		//textUsername.text = username;
	}

	public void StartGame () {
		Application.LoadLevel (1);
	}

	public void QuitGame () {
		//Debug.Log ("Quit");
		Application.Quit ();
	}
	
	public void UsernameConfirm () {
		username = inputUsername.text;
		textUsername.text = username;
		//inputUsername.enabled = false;//can only set once
	}
}
