using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=OxObASeB05I
//https://www.youtube.com/watch?v=1gveNfidKPA
public class InventoryMenuManager : MonoBehaviour {

    public static InventoryMenuManager inventoryMenuManager;

    public Canvas inventoryMenuCanvas;//set in Unity
    public GameObject inventoryPanel;//set in Unity
    public GameObject[] inventorySlotArray;
    public GameObject inventorySlotPrefab;//set in Unity

    void Awake () {
        if (inventoryMenuManager == null) {//like a singleton
            //DontDestroyOnLoad (gameObject);
            inventoryMenuManager = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        inventoryMenuCanvas = GetComponent<Canvas>();
        inventoryMenuCanvas.sortingOrder = 1;

        int numInventorySlots = 64;//TODO coordinate with CircleAgent.cs
        inventorySlotArray = new GameObject[numInventorySlots];
        for (int i=0; i<numInventorySlots; i++) {
            inventorySlotArray[i] = Instantiate(inventorySlotPrefab);
            inventorySlotArray[i].transform.SetParent(inventoryPanel.transform);
        }
    }

    /**
     * Displays the inventory menu
     * Initializes values for the EquipableImageManager for each equipable item image
     */
    public void OpenCloseInventoryMenu(bool input, Equipable[] inventoryEquipableArray) {
        if (input) {
            if (!inventoryMenuCanvas.enabled) {
                inventoryMenuCanvas.enabled = true;
                UpdateInventoryMenu(inventoryEquipableArray);
            } else {
                inventoryMenuCanvas.enabled = false;
            }
        }
    }

    public void UpdateInventoryMenu (Equipable[] inventoryEquipableArray) {
        for (int i = 0; i < inventoryEquipableArray.Length; i++) {
            GameObject equipableImageGameObject = inventorySlotArray[i].transform.GetChild(0).gameObject;
            equipableImageGameObject.GetComponent<EquipableImageManager>().craftingEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().craftingSlotIndex = -1;
            equipableImageGameObject.GetComponent<EquipableImageManager>().shopEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().shopSlotIndex = -1;
            equipableImageGameObject.GetComponent<EquipableImageManager>().levelEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().levelSlotIndex = -1;
            equipableImageGameObject.GetComponent<EquipableImageManager>().equipmentEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().equipmentSlotIndex = -1;
            equipableImageGameObject.GetComponent<EquipableImageManager>().inventoryEquipableArray = inventoryEquipableArray;
            equipableImageGameObject.GetComponent<EquipableImageManager>().inventorySlotIndex = i;
            if (inventoryEquipableArray[i] != null) {
                equipableImageGameObject.GetComponent<EquipableImageManager>().equipableClass = inventoryEquipableArray[i].equipableClass;
                equipableImageGameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(inventoryEquipableArray[i].GetType().ToString());
                equipableImageGameObject.GetComponent<EquipableImageManager>().equipable = inventoryEquipableArray[i];
                inventoryEquipableArray[i].equipableImageManager = equipableImageGameObject.GetComponent<EquipableImageManager>();
            } else {
                //equipableImageGameObject.GetComponent<Image>().sprite = null;
                equipableImageGameObject.GetComponent<Image>().color = new Color(1, 1, 1);
            }
        }
    }
}
