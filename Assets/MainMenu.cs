using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void StartGame () {
		Application.LoadLevel (1);
	}
	
	public void Instructions () {
		Application.LoadLevel (1);
	}
	
	public void Credits () {
		Application.LoadLevel (1);
	}
	
	public void QuitGame () {
		Debug.Log ("QuitGame");
		Application.Quit ();
	}
}
