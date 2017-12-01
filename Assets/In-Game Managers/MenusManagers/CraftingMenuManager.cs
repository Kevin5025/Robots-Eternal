using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingMenuManager : MonoBehaviour {

    public static CraftingMenuManager craftingMenuManager;

    public Canvas craftingMenuCanvas;//set in Unity

    public GameObject craftingEquipablePanel;//set in Unity
    public GameObject craftCraftingSlotGameObject;//set in Unity
    public GameObject[] craftingSlotArray;
    public GameObject craftingSlotPrefab;//set in Unity

    void Awake () {
        if (craftingMenuManager == null) {//like a singleton
            //DontDestroyOnLoad (gameObject);
            craftingMenuManager = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        craftingMenuCanvas = GetComponent<Canvas>();
        craftingMenuCanvas.sortingOrder = 1;

        int numLevelSlots = 64;
        craftingSlotArray = new GameObject[numLevelSlots];
        craftingSlotArray[0] = craftCraftingSlotGameObject;
        for (int l = 1; l < numLevelSlots; l++) {
            craftingSlotArray[l] = Instantiate(craftingSlotPrefab);
            craftingSlotArray[l].transform.SetParent(craftingEquipablePanel.transform);
        }

        Equipable[] craftingEquipableArray = new Equipable[64];
        craftingEquipableArray[9] = new MechanicalDegree();
        craftingEquipableArray[10] = new ThermonuclearDegree();
        craftingEquipableArray[11] = new BiochemicalDegree();
        craftingEquipableArray[12] = new ElectromagneticDegree();
        craftingEquipableArray[13] = new AstrocosmologicalDegree();
        craftingEquipableArray[14] = new QuantumcomputationalDegree();
        craftingEquipableArray[15] = new RadioluminescentDegree();

        for (int c = 0; c < craftingSlotArray.Length; c++) {
            GameObject equipableImageGameObject = craftingSlotArray[c].transform.GetChild(0).gameObject;
            equipableImageGameObject.GetComponent<EquipableImageManager>().craftingEquipableArray = craftingEquipableArray;
            equipableImageGameObject.GetComponent<EquipableImageManager>().craftingSlotIndex = c;
            equipableImageGameObject.GetComponent<EquipableImageManager>().shopEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().shopSlotIndex = -1;
            equipableImageGameObject.GetComponent<EquipableImageManager>().levelEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().levelSlotIndex = -1;
            equipableImageGameObject.GetComponent<EquipableImageManager>().equipmentEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().equipmentSlotIndex = -1;
            equipableImageGameObject.GetComponent<EquipableImageManager>().inventoryEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().inventorySlotIndex = -1;
            if (craftingEquipableArray[c] != null) {
                equipableImageGameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(craftingEquipableArray[c].GetType().ToString());
                equipableImageGameObject.GetComponent<Image>().color = new Color(1, 1, 1);
                equipableImageGameObject.GetComponent<EquipableImageManager>().equipable = craftingEquipableArray[c];
                craftingEquipableArray[c].equipableImageManager = equipableImageGameObject.GetComponent<EquipableImageManager>();
            }
        }
    }

    // Update is called once per frame
    void Update () {

    }

    /**
     * Displays the level menu
     */
    public void OpenCloseCraftingMenu (bool input) {
        if (input) {
            if (!craftingMenuCanvas.enabled) {
                craftingMenuCanvas.enabled = true;
            } else {
                craftingMenuCanvas.enabled = false;
            }
        }
    }
}
