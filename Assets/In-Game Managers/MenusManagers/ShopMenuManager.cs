using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuManager : MonoBehaviour {

    public static ShopMenuManager shopMenuManager;

    public Canvas shopMenuCanvas;//set in Unity

    public GameObject shopEquipablePanel;//set in Unity
    public GameObject buyShopSlotGameObject;//set in Unity
    public GameObject[] shopSlotArray;
    public GameObject shopSlotPrefab;//set in Unity

    void Awake () {
        if (shopMenuManager == null) {//like a singleton
            //DontDestroyOnLoad (gameObject);
            shopMenuManager = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        shopMenuCanvas = GetComponent<Canvas>();
        shopMenuCanvas.sortingOrder = 1;

        int numLevelSlots = 64;
        shopSlotArray = new GameObject[numLevelSlots];
        shopSlotArray[0] = buyShopSlotGameObject;
        for (int l = 1; l < numLevelSlots; l++) {
            shopSlotArray[l] = Instantiate(shopSlotPrefab);
            shopSlotArray[l].transform.SetParent(shopEquipablePanel.transform);
        }

        Equipable[] shopEquipableArray = new Equipable[64];
        shopEquipableArray[8] = new Pickaxe();
        shopEquipableArray[16] = new SmallVassal0();
        shopEquipableArray[17] = new SmallVassal1();

        for (int s = 0; s < shopSlotArray.Length; s++) {
            GameObject equipableImageGameObject = shopSlotArray[s].transform.GetChild(0).gameObject;
            equipableImageGameObject.GetComponent<EquipableImageManager>().craftingEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().craftingSlotIndex = -1;
            equipableImageGameObject.GetComponent<EquipableImageManager>().shopEquipableArray = shopEquipableArray;
            equipableImageGameObject.GetComponent<EquipableImageManager>().shopSlotIndex = s;
            equipableImageGameObject.GetComponent<EquipableImageManager>().levelEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().levelSlotIndex = -1;
            equipableImageGameObject.GetComponent<EquipableImageManager>().equipmentEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().equipmentSlotIndex = -1;
            equipableImageGameObject.GetComponent<EquipableImageManager>().inventoryEquipableArray = null;
            equipableImageGameObject.GetComponent<EquipableImageManager>().inventorySlotIndex = -1;
            if (shopEquipableArray[s] != null) {
                equipableImageGameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(shopEquipableArray[s].GetType().ToString());
                equipableImageGameObject.GetComponent<Image>().color = new Color(1, 1, 1);
                equipableImageGameObject.GetComponent<EquipableImageManager>().equipable = shopEquipableArray[s];
                shopEquipableArray[s].equipableImageManager = equipableImageGameObject.GetComponent<EquipableImageManager>();
            }
        }
    }

    // Update is called once per frame
    void Update () {

    }

    /**
     * Displays the level menu
     */
    public void OpenCloseShopMenu (bool input) {
        if (input) {
            if (!shopMenuCanvas.enabled) {
                shopMenuCanvas.enabled = true;
            } else {
                shopMenuCanvas.enabled = false;
            }
        }
    }
}
