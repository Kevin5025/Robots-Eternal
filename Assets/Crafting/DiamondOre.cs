using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Weak to mechanical
 */
public class DiamondOre : Ore {//white

    protected override void Start () {
        base.Start();
        oreClass = OreClass.Diamond;
        baseMechanicalArmor = 400f;

        experienceBounty = 15f;
    }

}
