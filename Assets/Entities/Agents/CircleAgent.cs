using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/**
 * This is anything that can fight. 
 */
public class CircleAgent : CircleEntity {

    public float mechanicalWeapon;
    public float mechanicalArmor;
    public float biochemicalWeapon;
    public float biochemicalArmor;
    public float electromagneticWeapon;
    public float electromagneticArmor;
    public float thermonuclearWeapon;
    public float thermonuclearArmor;

    public bool rightHanded;

    protected float fadeTime;
    protected float fadeTimeConstant;
    //public GameObject circleAgentHead;

    public Equipable[] inventoryEquipableArray;
    public Equipable[] equipmentEquipableArray;
    public float[] oreInventoried;

    public float experience;
    public float maxExperience;
    protected GameObject experienceBarContainerGameObject;
    protected Image experienceBarContainerImage;
    public int levelPoints;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        gameObject.layer = LayersManager.layersManager.getTeamAgentLayer(team);

        rightHanded = Random.value < 0.5;

        fadeTime = 6f;
        fadeTimeConstant = 0.25f / fadeTime;

        oreInventoried = new float[5];
        oreInventoried[0] = 300;
        oreInventoried[1] = 300;
        stoneOreBounty = 5f;
        experienceBounty = 5f;

        InitializeStats();
        InitializeEquipableArrays();
        UpdateStats();
    }

    private void InitializeStats () {
        maxHealth = area * 300f;
        health = maxHealth;

        baseMechanicalWeapon = 10f;
        baseMechanicalArmor = 20f;
        baseBiochemicalWeapon = 10f;
        baseBiochemicalArmor = 0f;
        baseElectromagneticWeapon = 10f;
        baseElectromagneticArmor = 0f;
        baseThermonuclearWeapon = 10f;
        baseThermonuclearArmor = 0f;

        experience = 0f;
        maxExperience = 160f;
        level = 3;
        levelPoints = level;
    }

    public void UpdateStats () {
        mechanicalWeapon = baseMechanicalWeapon;
        mechanicalArmor = baseMechanicalArmor;//radioluminescent?, etc. 
        thermonuclearWeapon = baseThermonuclearWeapon;
        thermonuclearArmor = baseThermonuclearArmor;
        biochemicalWeapon = baseBiochemicalWeapon;
        biochemicalArmor = baseBiochemicalArmor;
        electromagneticWeapon = baseElectromagneticWeapon;
        electromagneticArmor = baseElectromagneticArmor;

        for (int e=0; e<equipmentEquipableArray.Length; e++) {
            if (equipmentEquipableArray[e] != null) {
                mechanicalWeapon += equipmentEquipableArray[e].mechanicalWeapon;
                mechanicalArmor += equipmentEquipableArray[e].mechanicalArmor;
                thermonuclearWeapon += equipmentEquipableArray[e].thermonuclearWeapon;
                thermonuclearArmor += equipmentEquipableArray[e].thermonuclearArmor;
                biochemicalWeapon += equipmentEquipableArray[e].biochemicalWeapon;
                biochemicalArmor += equipmentEquipableArray[e].biochemicalArmor;
                electromagneticWeapon += equipmentEquipableArray[e].electromagneticWeapon;
                electromagneticArmor += equipmentEquipableArray[e].electromagneticArmor;
            }
        }
    }

    public void PrintStats() {
        Debug.Log(mechanicalWeapon + ", " + mechanicalArmor + ", " + thermonuclearWeapon + ", " + thermonuclearArmor + ", " + biochemicalWeapon + ", " + biochemicalArmor + ", " + electromagneticWeapon + ", " + electromagneticArmor);
    }

    /**
     * Passively gain experience for now. 
     */
    protected override void FixedUpdate () {
        base.FixedUpdate();
        if (!defunct) {
            experience += Time.deltaTime;
            if (experience >= maxExperience) {
                experience -= maxExperience;
                maxExperience += 20;
                levelPoints++;
                level++;
                LevelMenuManager.levelMenuManager.UpdateLevelStatMenu(levelPoints, level);
                HUDManager.hUDManager.UpdateStatMenu(maxHealth, healthRegenerationRate, level, maxExperience, mechanicalWeapon, mechanicalArmor, thermonuclearWeapon, thermonuclearArmor, biochemicalWeapon, biochemicalArmor, electromagneticWeapon, electromagneticArmor);
            }
        }
    }

    /**
     * Inventory has 64 slots and equipment has 20 slots. 
     * I do not expect to make more than 64 equipables in this preview game, so such an edge case is not handled. 
     */
    protected virtual void InitializeEquipableArrays () {
        inventoryEquipableArray = new Equipable[64];//TODO coordinate with InventoryMenuManager.cs
        inventoryEquipableArray[0] = new Shoot0();
        inventoryEquipableArray[5] = new Heal();
        inventoryEquipableArray[6] = new Sprint();
        inventoryEquipableArray[7] = new Pickaxe();

        equipmentEquipableArray = new Equipable[20];//TODO coordinate with HUDManager.cs
        equipmentEquipableArray[8] = inventoryEquipableArray[0];
        equipmentEquipableArray[10] = inventoryEquipableArray[5];
        equipmentEquipableArray[11] = inventoryEquipableArray[6];
    }

    /**
     * Initialize experience bar. 
     */
    protected override void initializeResourceBars () {
        base.initializeResourceBars();
        experienceBarContainerGameObject = (GameObject)Instantiate(ResourceBarManager.resourceBarManager.experienceBarContainerPrefab, new Vector2(transform.position.x, transform.position.y + 0.6f), Quaternion.identity);
        experienceBarContainerGameObject.GetComponentInChildren<ExperienceBar>().targetTransform = transform;
        experienceBarContainerGameObject.GetComponentInChildren<ExperienceBar>().targetCircleAgent = this;
        experienceBarContainerImage = experienceBarContainerGameObject.GetComponent<Image>();
    }

    /**
     * Overrides entity fade for a gradual disappearance, since these agents are more important than any entity. 
     */
    protected override IEnumerator Fade () {
		for (float f = 0.25f; f > 0; f -= Time.deltaTime * fadeTimeConstant) {
			spriteRenderer.color = new Color(r, g, b, f);
			//yield return new WaitForSeconds(1f);//3f? //is this consistent? 
			yield return null;
		}
		EliminateSelf();
    }

    protected override void EliminateSelf () {
        Destroy(experienceBarContainerGameObject);
        base.EliminateSelf();
    }
}
