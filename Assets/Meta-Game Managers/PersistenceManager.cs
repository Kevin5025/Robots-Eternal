using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;//we do it through C# serializer, not Unity serializer
using System.IO;

public class PersistenceManager : MonoBehaviour {
	
	public static PersistenceManager persistenceManager;
	public List<string> keys;
	public bool hasAccount;
	public Account account;
	public Settings settings;
	
	void Awake () {
		if (persistenceManager == null) {//like a singleton
			DontDestroyOnLoad (gameObject);
			persistenceManager = this;
		} else { //if (menuColors != null)
			Destroy(gameObject);
		}

		hasAccount = false;
		
		LoadKeys ();
		LoadSettings ();
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private string ObjectToString (object o) {
		using (MemoryStream ms = new MemoryStream ()) {
			new BinaryFormatter().Serialize(ms, o);
			return Convert.ToBase64String(ms.ToArray());
		}
	}
	
	private object StringToObject (string s) {
		byte[] bytes = Convert.FromBase64String (s);
		using (MemoryStream ms = new MemoryStream (bytes, 0, bytes.Length)) {
			ms.Write (bytes, 0, bytes.Length);
			ms.Position = 0;
			return new BinaryFormatter ().Deserialize (ms);
		}
	}
	
	public void SaveKeys () {
		PlayerPrefs.SetString ("keys", ObjectToString (keys));
		PlayerPrefs.Save ();
	}
	
	public void LoadKeys () {
		if (PlayerPrefs.HasKey ("keys")){
			keys = (List<string>) StringToObject (PlayerPrefs.GetString ("keys"));
		} else {
			keys = new List<string>();
		}
	}

	public void SaveAccount (string key) {
		PlayerPrefs.SetString ("_" + key, ObjectToString (account));
		PlayerPrefs.Save ();
	}
	
	public void LoadAccount (string key) {
		if (PlayerPrefs.HasKey ("_" + key)) {
			account = (Account) StringToObject (PlayerPrefs.GetString ("_" + key));
		}
	}
	
	public void DeleteAccount (string key) {
		if (PlayerPrefs.HasKey ("_" + key)) {
			PlayerPrefs.DeleteKey ("_" + key);
		}
	}

	public void SaveSettings () {
		PlayerPrefs.SetString ("settings", ObjectToString (settings));
		PlayerPrefs.Save ();
	}

	public void LoadSettings () {
		if (PlayerPrefs.HasKey ("settings")) {
			settings = (Settings) StringToObject (PlayerPrefs.GetString ("settings"));
		} else {
			settings = new Settings();
		}
	}
}

[Serializable]
public class Account {//because shouldn't write Monobehavior to file
	public string username;
	public int points;
	
	public Account () {
		points = 0;
	}
}

[Serializable]
public class Settings {
	public int cameraScheme;
	public int rotateScheme;

	public Settings() {
		cameraScheme = 0;
		rotateScheme = 0;
	}
}