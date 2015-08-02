using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeSettingsMenu : MonoBehaviour {

	public Button[] cameraSchemeButtons;
	public Button[] rotateSchemeButtons;

	public Button cancelButton;
	public Button saveButton;

	Button selectedCameraSchemeButton;
	Button selectedRotateSchemeButton;

	// Use this for initialization
	void Start () {
		//cameraSchemeButtons = new Button[2];//in editor
		//rotateSchemeButtons = new Button[3];

		cameraSchemeButtons[0].colors = MenuColors.whiteColor;
		cameraSchemeButtons[1].colors = MenuColors.whiteColor;
		rotateSchemeButtons[0].colors = MenuColors.whiteColor;
		rotateSchemeButtons[1].colors = MenuColors.whiteColor;
		rotateSchemeButtons[2].colors = MenuColors.whiteColor;
		cancelButton.colors = MenuColors.redColor;
		saveButton.colors = MenuColors.magentaColor;

		cameraSchemeButtons [PersistenceManager.persistenceManager.settings.cameraScheme].colors = MenuColors.cyanColor;
		selectedCameraSchemeButton = cameraSchemeButtons [PersistenceManager.persistenceManager.settings.cameraScheme];
		rotateSchemeButtons [PersistenceManager.persistenceManager.settings.rotateScheme].colors = MenuColors.cyanColor;
		selectedRotateSchemeButton = rotateSchemeButtons [PersistenceManager.persistenceManager.settings.rotateScheme];

		cameraSchemeButtons[0].onClick.AddListener (() => CameraScheme (0, cameraSchemeButtons[0]));
		cameraSchemeButtons[1].onClick.AddListener (() => CameraScheme (1, cameraSchemeButtons[1]));
		rotateSchemeButtons[0].onClick.AddListener (() => RotateScheme (0, rotateSchemeButtons[0]));
		rotateSchemeButtons[1].onClick.AddListener (() => RotateScheme (1, rotateSchemeButtons[1]));
		rotateSchemeButtons[2].onClick.AddListener (() => RotateScheme (2, rotateSchemeButtons[2]));
		cancelButton.onClick.AddListener (() => Cancel ());
		saveButton.onClick.AddListener (() => Save ());

		AccountCanvas.accountCanvas.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CameraScheme (int cameraScheme, Button cameraSchemeButton) {
		if (selectedCameraSchemeButton != null) {
			selectedCameraSchemeButton.colors = MenuColors.whiteColor;
		}
		cameraSchemeButton.colors = MenuColors.cyanColor;

		selectedCameraSchemeButton = cameraSchemeButton;

		PersistenceManager.persistenceManager.settings.cameraScheme = cameraScheme;
	}

	void RotateScheme (int rotateScheme, Button rotateSchemeButton) {
		if (selectedRotateSchemeButton != null) {
			selectedRotateSchemeButton.colors = MenuColors.whiteColor;
		}
		rotateSchemeButton.colors = MenuColors.cyanColor;

		selectedRotateSchemeButton = rotateSchemeButton;
		
		PersistenceManager.persistenceManager.settings.rotateScheme = rotateScheme;

	}

	void Cancel () {
		Application.LoadLevel ("Main Menu");
	}

	void Save () {
		PersistenceManager.persistenceManager.SaveSettings ();
		Application.LoadLevel ("Main Menu");
	}
}
