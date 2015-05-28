using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;//we do it through C# serializer, not Unity serializer
using System.IO;

public class AccountManager : MonoBehaviour {

	public static AccountManager accountManager;
	public static Account account = null;
	public static List<string> usernames;//accountUsernames

	void Awake () {
		if (accountManager == null) {//like a singleton
			DontDestroyOnLoad (gameObject);
			accountManager = this;
		} else { //if (menuColors != null)
			Destroy(gameObject);
		}

		LoadUsernames ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SaveAccount () {//https://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/persistence-data-saving-loading
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/_" + account.username + ".dat");
		bf.Serialize (file, account);
		file.Close ();
		Debug.Log (Application.persistentDataPath);
	}

	public void LoadAccount () {
		if (File.Exists (Application.persistentDataPath + "/_" + account.username + ".dat")){
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/_" + account.username + ".dat", FileMode.Open);
			account = (Account) bf.Deserialize (file);
			file.Close ();
		}
	}

	public void SaveUsernames() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/usernames.dat");
		bf.Serialize (file, usernames);
		file.Close ();
	}

	public void LoadUsernames () {
		if (File.Exists (Application.persistentDataPath + "/usernames.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/usernames.dat", FileMode.Open);
			usernames = (List<string>) bf.Deserialize (file);
			file.Close ();
		} else {
			usernames = new List<string>();
		}
	}
	
	public void Delete () {
		if (File.Exists (Application.persistentDataPath + "/_" + account.username + ".dat")) {
			File.Delete (Application.persistentDataPath + "/_" + account.username + ".dat");
		}
	}
}

[Serializable]
public class Account{//because don't write Monobehavior to file
	public string username;
}