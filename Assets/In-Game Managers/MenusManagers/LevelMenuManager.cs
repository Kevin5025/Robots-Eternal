using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuManager : MonoBehaviour {

    public static LevelMenuManager levelMenuManager;

    public Canvas levelMenuCanvas;//set in Unity
    public GameObject levelStatPanel;//set in Unity
    public Text levelPointsStatText;//set in Unity
    public Text levelStatText;//set in Unity

    public GameObject levelEquipablePanel;//set in Unity
    public GameObject learnLevelSlotGameObject;//set in Unity
    public GameObject[] levelSlotArray;
    public GameObject levelSlotPrefab;//set in Unity

    void Awake () {
        if (levelMenuManager == null) {//like a singleton
            //DontDestroyOnLoad (gameObject);
            levelMenuManager = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        levelMenuCanvas = GetComponent<Canvas>();
        levelMenuCanvas.sortingOrder = 1;

        levelPointsStatText.text = "null";
        levelStatText.text = "null";
        
        int numLevelSlots = 64;
        levelSlotArray = new GameObject[numLevelSlots];
        levelSlotArray[0] = learnLevelSlotGameObject;
        for (int l=1; l<numLevelSlots; l++) {
            levelSlotArray[l] = Instantiate(levelSlotPrefab);
            levelSlotArray[l].transform.SetParent(levelEquipablePanel.transform);
        }

        Equipable[] levelEquipableArray = new Equipable[64];
        levelEquipableArray[1] = new Shoot0();
        levelEquipableArray[2] = new Shoot1();
        levelEquipableArray[3] = new Shoot2();
        levelEquipableArray[4] = new Shoot3();
        levelEquipableArray[5] = new Shoot4();
        levelEquipableArray[6] = new Heal();
        levelEquipableArray[7] = new Sprint();

        for (int l=0; l< levelSlotArray.Length; l++) {
            GameObject equipableImageGameObject = levelSlotArray[l].transform.GetChild(0).gameObject;
            equipableImageGameObject.GetComponent<EquipableImageManager>().craftingEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().craftingSlotIndex = -1;
            equipableImageGameObject.GetComponent<EquipableImageManager>().shopEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().shopSlotIndex = -1;
            equipableImageGameObject.GetComponent<EquipableImageManager>().levelEquipableArray = levelEquipableArray;
            equipableImageGameObject.GetComponent<EquipableImageManager>().levelSlotIndex = l;
            equipableImageGameObject.GetComponent<EquipableImageManager>().equipmentEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().equipmentSlotIndex = -1;
            equipableImageGameObject.GetComponent<EquipableImageManager>().inventoryEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().inventorySlotIndex = -1;
            if (levelEquipableArray[l] != null) {
                equipableImageGameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(levelEquipableArray[l].GetType().ToString());
                equipableImageGameObject.GetComponent<Image>().color = new Color(1, 1, 1);
                equipableImageGameObject.GetComponent<EquipableImageManager>().equipable = levelEquipableArray[l];
                levelEquipableArray[l].equipableImageManager = equipableImageGameObject.GetComponent<EquipableImageManager>();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /**
     * Called by PlayerController
     * Called by CircleAgent
     */
    public void UpdateLevelStatMenu (int levelPoints, int level) {
        levelPointsStatText.text = levelPoints.ToString();
        levelStatText.text = level.ToString();
    }

    /**
     * Displays the level menu
     */
    public void OpenCloseLevelMenu (bool input) {
        if (input) {
            if (!levelMenuCanvas.enabled) {
                levelMenuCanvas.enabled = true;
            } else {
                levelMenuCanvas.enabled = false;
            }
        }
    }
}
