using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Interactive resource on the map
 */
public abstract class Ore : Entity {

    public enum OreClass {Stone, Diamond, Ruby, Emerald, Sapphire};
    public OreClass oreClass;

    /**
     * Ores are tough to eliminate
     */
    protected override void Start () {
        base.Start();
        team = Team.NONE;

        maxHealth = 100f;
        health = maxHealth;
        healthRegenerationRate = 0f;

        baseMechanicalWeapon = 0f;
        baseMechanicalArmor = 500f;
        baseBiochemicalWeapon = 0f;
        baseBiochemicalArmor = 300f;
        baseElectromagneticWeapon = 0f;
        baseElectromagneticArmor = 300f;
        baseThermonuclearWeapon = 0f;
        baseThermonuclearArmor = 300f;
    }

    public void GetPickaxed (CircleAgent casterAgent, float trueDamage) {
        Vector3 pickaxeDirection = transform.position - casterAgent.transform.position;
        pickaxeDirection /= pickaxeDirection.magnitude;
        pickaxeDirection *= trueDamage / 100f;
        transform.position += pickaxeDirection;
    }

    public static Color GetOreColor (int t) {
        if (t == 0) {
            return new Color(0.25f, 0.25f, 0.25f);
        } else if (t == 1) {
            return new Color(1, 1, 1);
        } else if (t == 2) {
            return new Color(1, 0, 0);
        } else if (t == 3) {
            return new Color(0, 1, 0);
        } else if (t == 4) {
            return new Color(0, 0, 1);
        } else {
            return new Color(0, 0, 0);
        }
    }
}
