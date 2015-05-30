using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;//we do it through C# serializer, not Unity serializer
using System.IO;

public class AccountManager : MonoBehaviour {

	public static AccountManager accountManager;
	public Account account;
	public List<string> keys;//a real singleton would not have these as statics

	void Awake () {
		account = null;
		if (accountManager == null) {//like a singleton
			DontDestroyOnLoad (gameObject);
			accountManager = this;
		} else { //if (menuColors != null)
			Destroy(gameObject);
		}

		LoadKeys ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SaveAccount (string key) {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/_" + key + ".dat");
		bf.Serialize (file, account);
		file.Close ();
		//Debug.Log (Application.persistentDataPath);
	}

	public void LoadAccount (string key) {
		if (File.Exists (Application.persistentDataPath + "/_" + key + ".dat")){
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/_" + key + ".dat", FileMode.Open);
			account = (Account) bf.Deserialize (file);
			file.Close ();
		}
	}
	
	public void DeleteAccount (string key) {
		if (File.Exists (Application.persistentDataPath + "/_" + key + ".dat")) {
			File.Delete (Application.persistentDataPath + "/_" + key + ".dat");
		}
	}

	public void SaveKeys() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/usernames.dat");
		bf.Serialize (file, keys);
		file.Close ();
	}

	public void LoadKeys () {
		if (File.Exists (Application.persistentDataPath + "/usernames.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/usernames.dat", FileMode.Open);
			keys = (List<string>) bf.Deserialize (file);
			file.Close ();
		} else {
			keys = new List<string>();
		}
	}
}

[Serializable]
public class Account{//because shouldn't write Monobehavior to file
	public string username;
	public int points;

	public Account () {
		points = 0;
	}
}