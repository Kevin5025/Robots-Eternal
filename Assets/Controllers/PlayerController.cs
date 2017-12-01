using UnityEngine;

/**
 * Allows humans to interface their character. 
 */
public class PlayerController : AgentController {

    public static PlayerController playerController;

    protected override void Awake () {
        base.Awake();
        if (playerController == null) {
            playerController = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    protected override void Start () {
        base.Start();
        MainCamera.mainCamera.playerTransform = transform;
        HUDManager.hUDManager.UpdateEquipmentMenu(agent.equipmentEquipableArray);
        HUDManager.hUDManager.UpdateOreMenu(agent.oreInventoried);
        //TODO make less ridiculous
        HUDManager.hUDManager.UpdateStatMenu(agent.maxHealth, agent.healthRegenerationRate, agent.level, agent.maxExperience, agent.mechanicalWeapon, agent.mechanicalArmor, agent.thermonuclearWeapon, agent.thermonuclearArmor, agent.biochemicalWeapon, agent.biochemicalArmor, agent.electromagneticWeapon, agent.electromagneticArmor);
        LevelMenuManager.levelMenuManager.UpdateLevelStatMenu(agent.levelPoints, agent.level);
    }

    protected override void Update () {
        base.Update();
        Menus();
    }

    protected override void FixedUpdate () {
        base.FixedUpdate();
        Rotate();
        Move();
        Fire();
    }

    /**
     * Character rotates to face wherever the mouse is. 
     */
    protected virtual void Rotate () {
        agent.Rotate(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    /**
     * WASD controls. 
     */
    protected virtual void Move () {
        agent.Move(Input.GetKey(KeyCode.W), Input.GetKey(KeyCode.S), Input.GetKey(KeyCode.D), Input.GetKey(KeyCode.A));
    }

    /**
     * Activation of abilities. 
     */
    protected virtual void Fire () {
        if (Input.GetKey(KeyCode.Q)) {
            if (agent.equipmentEquipableArray[4] != null) {
                agent.equipmentEquipableArray[4].Activate(agent);
            }
        }
        if (Input.GetKey(KeyCode.E)) {
            if (agent.equipmentEquipableArray[5] != null) {
                agent.equipmentEquipableArray[5].Activate(agent);
            }
        }
        //R6, F7
        if (Input.GetMouseButton(0)) {
            if (agent.equipmentEquipableArray[8] != null) {
                agent.equipmentEquipableArray[8].Activate(agent);
            }
        }
        if (Input.GetMouseButton(1)) {
            if (agent.equipmentEquipableArray[9] != null) {
                agent.equipmentEquipableArray[9].Activate(agent);
            }
        }
        if (Input.GetKey(KeyCode.Alpha1)) {
            if (agent.equipmentEquipableArray[10] != null) {
                agent.equipmentEquipableArray[10].Activate(agent);
            }
        }
        if (Input.GetKey(KeyCode.Alpha2)) {
            if (agent.equipmentEquipableArray[11] != null) {
                agent.equipmentEquipableArray[11].Activate(agent);
            }
        }
        if (Input.GetKey(KeyCode.Alpha3)) {
            if (agent.equipmentEquipableArray[12] != null) {
                agent.equipmentEquipableArray[12].Activate(agent);
            }
        }
        if (Input.GetKey(KeyCode.Alpha4)) {
            if (agent.equipmentEquipableArray[13] != null) {
                agent.equipmentEquipableArray[13].Activate(agent);
            }
        }
        if (Input.GetKey(KeyCode.Z)) {
            if (agent.equipmentEquipableArray[14] != null) {
                agent.equipmentEquipableArray[14].Activate(agent);
            }
        }
        if (Input.GetKey(KeyCode.X)) {
            if (agent.equipmentEquipableArray[15] != null) {
                agent.equipmentEquipableArray[15].Activate(agent);
            }
        }
        if (Input.GetKey(KeyCode.C)) {
            if (agent.equipmentEquipableArray[16] != null) {
                agent.equipmentEquipableArray[16].Activate(agent);
            }
        }
        if (Input.GetKey(KeyCode.V)) {
            if (agent.equipmentEquipableArray[17] != null) {
                agent.equipmentEquipableArray[17].Activate(agent);
            }
        }
        if (Input.GetKey(KeyCode.B)) {
            if (agent.equipmentEquipableArray[18] != null) {
                agent.equipmentEquipableArray[18].Activate(agent);
            }
        }
        if (Input.GetKey(KeyCode.N)) {
            if (agent.equipmentEquipableArray[19] != null) {
                agent.equipmentEquipableArray[19].Activate(agent);
            }
        }
    }

    protected virtual void Menus () {
        InventoryMenuManager.inventoryMenuManager.OpenCloseInventoryMenu(Input.GetKeyDown(KeyCode.I), agent.inventoryEquipableArray);
        LevelMenuManager.levelMenuManager.OpenCloseLevelMenu(Input.GetKeyDown(KeyCode.K));
        ShopMenuManager.shopMenuManager.OpenCloseShopMenu(Input.GetKeyDown(KeyCode.O));
        CraftingMenuManager.craftingMenuManager.OpenCloseCraftingMenu(Input.GetKeyDown(KeyCode.L));
        PauseMenuManager.pauseMenuManager.OpenClosePauseMenu(Input.GetKeyDown(KeyCode.Escape));
    }
}


