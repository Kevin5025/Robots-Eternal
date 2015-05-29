using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ManageAccounts : MonoBehaviour {

	public GameObject accountButton;

	public Transform contentPanel;

	// Use this for initialization
	void Start () {
		AccountManager.accountManager.LoadUsernames ();

		foreach (string username in AccountManager.usernames) {
			GameObject newAccountButton = Instantiate (accountButton) as GameObject;
			newAccountButton.transform.SetParent (contentPanel);

			AccountButtonComponents accountButtonComponents = newAccountButton.GetComponent <AccountButtonComponents> ();
			accountButtonComponents.textUsername.text = username;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}