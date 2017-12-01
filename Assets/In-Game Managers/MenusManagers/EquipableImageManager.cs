using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * Controls behavior of the draggable equipables for inventory menu and equipment hud
 */
public class EquipableImageManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler {

    public Equipable[] equipmentEquipableArray;//used to determine type of slot
    public int equipmentSlotIndex;
    public Equipable[] inventoryEquipableArray;//used to determine type of slot
    public int inventorySlotIndex;
    public Equipable[] levelEquipableArray;//used to determine type of slot
    public int levelSlotIndex;
    public Equipable[] shopEquipableArray;//used to determine type of slot
    public int shopSlotIndex;
    public Equipable[] craftingEquipableArray;//used to determine type of slot
    public int craftingSlotIndex;

    public Equipable.EquipableClass equipableClass;
    public Equipable equipable;
    protected bool onCooldown;

    public Transform tempParentTransform;
    public bool tempIsDropped;

    protected void Start () {

    }

    protected void Update () {
        
    }

    /**
     * Changes color of image to reflect whether ability is on cooldown
     */
    public void NotifyCooldown () {
        onCooldown = !onCooldown;
        if (onCooldown) {
            GetComponent<Image>().color *= 0.5f;//TODO consider storing original color for numerical stability
        } else {
            GetComponent<Image>().color *= 2f;
        }
    }

    /**
     * Makes equipable image follow mouse and sets parent to avoid being occluded
     */
    public void OnBeginDrag (PointerEventData eventData) {
        if (equipmentEquipableArray == null) {
            GetComponent<CanvasGroup>().blocksRaycasts = false;

            tempParentTransform = transform.parent.transform;
            transform.SetParent(tempParentTransform.parent);
            transform.position = eventData.position;
        }
    }

    /**
     * Makes equipable image follow mouse
     */
    public void OnDrag (PointerEventData eventData) {
        if (equipmentEquipableArray == null) {
            transform.transform.position = eventData.position;
        }
    }

    /**
     * If dropped on another inventory equipable, swaps their positions
     * If dropped on an equipment slot, equipas the equipable
     */
    public void OnDrop (PointerEventData eventData) {
        Image senderImage = eventData.pointerDrag.GetComponent<Image>();
        if (senderImage.sprite == null) {
            GetComponent<Image>().sprite = null;
            GetComponent<Image>().color = HUDManager.hUDManager.equipmentSlotArrayColor(equipableClass);
            equipmentEquipableArray[equipmentSlotIndex] = null;
            PlayerController.playerController.agent.UpdateStats();
            PlayerController.playerController.agent.PrintStats();
            return;
        }

        EquipableImageManager sender = eventData.pointerDrag.GetComponent<EquipableImageManager>();
        if (inventoryEquipableArray != null) {
            sender.tempIsDropped = true;

            /**
             * swap parent slots
             */
            Transform senderParentTransform = sender.tempParentTransform;
            Transform receiverParentTransform = transform.parent;
            transform.SetParent(senderParentTransform);
            transform.position = senderParentTransform.position;
            sender.transform.SetParent(receiverParentTransform);
            sender.transform.position = receiverParentTransform.position;

            /**
             * swap inventoryEquipmentArray indices
             */
            int senderSlotIndex = sender.inventorySlotIndex;
            int receiverSlotIndex = inventorySlotIndex;
            inventorySlotIndex = senderSlotIndex;
            sender.inventorySlotIndex = receiverSlotIndex;
            Equipable senderEquipable = inventoryEquipableArray[senderSlotIndex];
            Equipable receiverEquipable = inventoryEquipableArray[receiverSlotIndex];
            inventoryEquipableArray[senderSlotIndex] = receiverEquipable;
            inventoryEquipableArray[receiverSlotIndex] = senderEquipable;
        } else if (equipmentEquipableArray != null) {
            if (sender.equipableClass == equipableClass) {
                /**
                 * equip the inventory equipable to equipment
                 */
                GetComponent<Image>().sprite = Resources.Load<Sprite>(sender.inventoryEquipableArray[sender.inventorySlotIndex].GetType().ToString());
                GetComponent<Image>().color = new Color(1, 1, 1);

                int senderSlotIndex = sender.inventorySlotIndex;
                Equipable senderEquipable = sender.inventoryEquipableArray[senderSlotIndex];
                equipmentEquipableArray[equipmentSlotIndex] = senderEquipable;
                GetComponent<EquipableImageManager>().equipable = senderEquipable;
                senderEquipable.equipableEquipmentImageManager = this;

                PlayerController.playerController.agent.UpdateStats();
                //PlayerController.player.agent.PrintStats();
                //TODO make less ridiculous
                HUDManager.hUDManager.UpdateStatMenu(PlayerController.playerController.agent.maxHealth, PlayerController.playerController.agent.healthRegenerationRate, PlayerController.playerController.agent.level, PlayerController.playerController.agent.maxExperience, PlayerController.playerController.agent.mechanicalWeapon, PlayerController.playerController.agent.mechanicalArmor, PlayerController.playerController.agent.thermonuclearWeapon, PlayerController.playerController.agent.thermonuclearArmor, PlayerController.playerController.agent.biochemicalWeapon, PlayerController.playerController.agent.biochemicalArmor, PlayerController.playerController.agent.electromagneticWeapon, PlayerController.playerController.agent.electromagneticArmor);
            }
        } else if (levelEquipableArray != null) {
            if (levelSlotIndex == 0 && PlayerController.playerController.agent.levelPoints >= 1) {
                PlayerController.playerController.agent.levelPoints--;
                for (int i=0; i<PlayerController.playerController.agent.inventoryEquipableArray.Length; i++) {
                    if (PlayerController.playerController.agent.inventoryEquipableArray[i] == null) {
                        int senderSlotIndex = sender.levelSlotIndex;
                        PlayerController.playerController.agent.inventoryEquipableArray[i] = (Equipable) levelEquipableArray[senderSlotIndex].Clone();
                        break;
                    }
                }
                LevelMenuManager.levelMenuManager.UpdateLevelStatMenu(PlayerController.playerController.agent.levelPoints, PlayerController.playerController.agent.level);
            }
        } else if (shopEquipableArray != null) {
            if (shopSlotIndex == 0 && PlayerController.playerController.agent.oreInventoried[0] >= 100) {
                PlayerController.playerController.agent.oreInventoried[0] -= 100;
                for (int i = 0; i < PlayerController.playerController.agent.inventoryEquipableArray.Length; i++) {
                    if (PlayerController.playerController.agent.inventoryEquipableArray[i] == null) {
                        //Type equipableType = levelEquipableArray[senderSlotIndex].GetType();
                        //Debug.Log(equipableType);
                        //ConstructorInfo equipableConstructorInfo = equipableType.GetConstructor(Type.EmptyTypes);//http://stackoverflow.com/questions/142356/most-efficient-way-to-get-default-constructor-of-a-type
                        //dynamic equipable = Convert.ChangeType(equipableConstructorInfo.Invoke(new object[] { }), equipableType);//http://stackoverflow.com/questions/972636/casting-a-variable-using-a-type-variable
                        //PlayerController.playerController.agent.inventoryEquipableArray[i] = equipable;//http://stackoverflow.com/questions/3255697/using-c-sharp-reflection-to-call-a-constructor
                        //Debug.Log(PlayerController.playerController.agent.inventoryEquipableArray[i].GetType());

                        int senderSlotIndex = sender.shopSlotIndex;
                        PlayerController.playerController.agent.inventoryEquipableArray[i] = (Equipable) shopEquipableArray[senderSlotIndex].Clone();
                        break;
                    }
                }
                HUDManager.hUDManager.UpdateOreMenu(PlayerController.playerController.agent.oreInventoried);
            }
        } else if (craftingEquipableArray != null) {
            if (craftingSlotIndex == 0 && PlayerController.playerController.agent.oreInventoried[1] >= 100) {
                PlayerController.playerController.agent.oreInventoried[1] -= 100;
                for (int i = 0; i < PlayerController.playerController.agent.inventoryEquipableArray.Length; i++) {
                    if (PlayerController.playerController.agent.inventoryEquipableArray[i] == null) {
                        int senderSlotIndex = sender.craftingSlotIndex;
                        PlayerController.playerController.agent.inventoryEquipableArray[i] = (Equipable) craftingEquipableArray[senderSlotIndex].Clone();
                        break;
                    }
                }
                HUDManager.hUDManager.UpdateOreMenu(PlayerController.playerController.agent.oreInventoried);
            }
        }
    }

    /**
     * Resets parent back to normal
     */
    public void OnEndDrag (PointerEventData eventData) {
        if (equipmentEquipableArray == null) {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            if (!tempIsDropped) {
                transform.SetParent(tempParentTransform);
                transform.position = tempParentTransform.position;
            } else {
                tempIsDropped = false;
            }
        }
    }
}
