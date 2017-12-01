using System;
using UnityEngine;
using UnityEngine.UI;

/**
 * Initializes hud and resource bars. 
 */
public class HUDManager : MonoBehaviour {

    public static HUDManager hUDManager;//set in Unity

    public GameObject textPrefab;//set in Unity

    public GameObject equipmentPanel;//set in Unity
    public GameObject[] equipmentSlotArray;//filled in UpdateEquipmentMenu
    public GameObject equipmentSlotPrefab;//set in Unity

    public GameObject orePanel;//set in Unity
    public GameObject[] oreTextGameObjectArray;
    public Text[] oreTextArray;

    public GameObject statPanel;//set in Unity
    public GameObject[] statTextGameObjectArray;
    public Text[] statTextArray;

    public Text victoryText;//set in Unity

    void Awake () {
        if (hUDManager == null) {//like a singleton
            //DontDestroyOnLoad (gameObject);
            hUDManager = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        GetComponent<Canvas>().sortingOrder = 1;

        int numEquipmentSlots = 20;//TODO coordinate with CircleAgent.cs
        equipmentSlotArray = new GameObject[numEquipmentSlots];
        for (int e=0; e<numEquipmentSlots; e++) {
            equipmentSlotArray[e] = Instantiate(equipmentSlotPrefab);
            equipmentSlotArray[e].transform.SetParent(equipmentPanel.transform);
            equipmentSlotArray[e].transform.GetChild(0).GetComponent<Image>().color = equipmentSlotArrayColor(equipmentSlotArrayEquipableClass(e));
        }

        int numOreTexts = Enum.GetNames(typeof(Ore.OreClass)).Length;//5
        oreTextGameObjectArray = new GameObject[numOreTexts];
        oreTextArray = new Text[numOreTexts];
        for (int o=0; o<numOreTexts; o++) {
            oreTextGameObjectArray[o] = Instantiate(textPrefab);
            oreTextGameObjectArray[o].transform.SetParent(orePanel.transform);
            oreTextArray[o] = oreTextGameObjectArray[o].GetComponent<Text>();
            oreTextArray[o].color = Ore.GetOreColor(o);
            oreTextArray[o].text = "null";
        }

        int numStatTexts = 12;
        statTextGameObjectArray = new GameObject[numStatTexts];
        statTextArray = new Text[numStatTexts];
        for (int s=0; s<numStatTexts; s++) {
            statTextGameObjectArray[s] = Instantiate(textPrefab);
            statTextGameObjectArray[s].transform.SetParent(statPanel.transform);
            statTextArray[s] = statTextGameObjectArray[s].GetComponent<Text>();
            statTextArray[s].color = GetStatColor(s);
            statTextArray[s].text = "null";
        }
    }
    
    public Color GetStatColor(int s) {
        if (s==0 || s==1 || s==2) {
            return new Color(1, 0, 0);
        } else if (s==3 || s==4 || s==5) {
            return new Color(0.5f, 0.5f, 0.5f);
        } else if (s == 6 || s == 7) {
            return new Color(1, 1, 1);
        } else if (s == 8 || s == 9) {
            return new Color(1, 0, 0);
        } else if (s == 10 || s == 11) {
            return new Color(0, 1, 0);
        } else if (s == 12 || s == 13) {
            return new Color(0, 0, 1);
        } else {
            return new Color(0, 0, 0);
        }
    }
    
    /**
     * returns the correct background color for the given equipable class
     */
    public Color equipmentSlotArrayColor (Equipable.EquipableClass equipableClass) {
        if (equipableClass == Equipable.EquipableClass.AccessoryItem) {
            return new Color(1, 0.00f, 0.50f);
        } else if (equipableClass == Equipable.EquipableClass.HandItem) {
            return new Color(1, 0.00f, 0.00f);
        } else if (equipableClass == Equipable.EquipableClass.HeadItem) {
            return new Color(1, 0.50f, 0.00f);
        } else if (equipableClass == Equipable.EquipableClass.BodyItem) {
            return new Color(1, 1.00f, 0.00f);
        } else if (equipableClass == Equipable.EquipableClass.Ability) {
            return new Color(0, 1, 0);
        } else if (equipableClass == Equipable.EquipableClass.LargeVassal) {
            return new Color(0, 1, 1);
        } else if (equipableClass == Equipable.EquipableClass.SmallVassal) {
            return new Color(0, 0, 1);
        } else {
            return new Color(1, 1, 1);
        }
    }

    /**
     * Helper function to Initialize empty equipment slots
     * 4 Accessory Items
     * 2 Hand Items
     * 1 Head Item
     * 1 Body Item
     * 6 Abilities
     * 1 Large Vassals
     * 5 Small Vassals
     */
    public Equipable.EquipableClass equipmentSlotArrayEquipableClass (int e) {
        if (e < 4) {
            return Equipable.EquipableClass.AccessoryItem;
        } else if (e < 6) {
            return Equipable.EquipableClass.HandItem;
        } else if (e < 7) {
            return Equipable.EquipableClass.HeadItem;
        } else if (e < 8) {
            return Equipable.EquipableClass.BodyItem;
        } else if (e < 14) {
            return Equipable.EquipableClass.Ability;
        } else if (e < 15) {
            return Equipable.EquipableClass.LargeVassal;
        } else {//if (e<20) 
            return Equipable.EquipableClass.SmallVassal;
        }
    }

    /**
     * Initializes the equipment menu
     * Initializes values for the EquipableImageManager for each equipable item image
     * Called once by PlayerController.cs
     */
    public void UpdateEquipmentMenu (Equipable[] equipmentEquipableArray) {
        for (int e=0; e<equipmentEquipableArray.Length; e++) {
            GameObject equipableImageGameObject = equipmentSlotArray[e].transform.GetChild(0).gameObject;
            equipableImageGameObject.GetComponent<EquipableImageManager>().shopEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().shopSlotIndex = -1;
            equipableImageGameObject.GetComponent<EquipableImageManager>().levelEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().levelSlotIndex = -1;
            equipableImageGameObject.GetComponent<EquipableImageManager>().equipmentEquipableArray = equipmentEquipableArray;
            equipableImageGameObject.GetComponent<EquipableImageManager>().equipmentSlotIndex = e;
            equipableImageGameObject.GetComponent<EquipableImageManager>().inventoryEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().inventorySlotIndex = -1;
            equipableImageGameObject.GetComponent<EquipableImageManager>().equipableClass = equipmentSlotArrayEquipableClass(e);
            if (equipmentEquipableArray[e] != null) {
                equipableImageGameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(equipmentEquipableArray[e].GetType().ToString());
                equipableImageGameObject.GetComponent<Image>().color = new Color(1, 1, 1);
                equipableImageGameObject.GetComponent<EquipableImageManager>().equipable = equipmentEquipableArray[e];
                equipmentEquipableArray[e].equipableEquipmentImageManager = equipableImageGameObject.GetComponent<EquipableImageManager>();
            }
        }
    }

    /**
     * Called once by PlayerController.cs
     * Called many times by Pickaxe.cs
     */
    public void UpdateOreMenu (float[] OreInventoried) {
        for (int o=0; o<OreInventoried.Length; o++) {
            oreTextArray[o].text = (OreInventoried[o]).ToString();
        }
    }

    /**
     * Called once by PlayerController.cs
     * Called many times by EquipableImageManager.cs
     * Called by CircleAgent.cs
     */
    public void UpdateStatMenu (float maxHealth, float healthRegenerationRate, float level, float maxExperience, float mechanicalWeapon, float mechanicalArmor, float thermonuclearWeapon, float thermonuclearArmor, float biochemicalWeapon, float biochemicalArmor, float electromagneticWeapon, float electromagneticArmor) {
        statTextArray[0].text = (maxHealth).ToString();
        statTextArray[1].text = (healthRegenerationRate).ToString();
        statTextArray[2].text = (level).ToString();
        statTextArray[3].text = (maxExperience).ToString();
        statTextArray[4].text = (mechanicalWeapon).ToString();
        statTextArray[5].text = (mechanicalArmor).ToString();
        statTextArray[6].text = (thermonuclearWeapon).ToString();
        statTextArray[7].text = (thermonuclearArmor).ToString();
        statTextArray[8].text = (biochemicalWeapon).ToString();
        statTextArray[9].text = (biochemicalArmor).ToString();
        statTextArray[10].text = (electromagneticWeapon).ToString();
        statTextArray[11].text = (electromagneticArmor).ToString();
    }
}