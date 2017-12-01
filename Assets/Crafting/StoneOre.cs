using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Weak to all
 */
public class StoneOre : Ore {//blue

    protected override void Start () {
        base.Start();
        oreClass = OreClass.Stone;
        baseMechanicalArmor = 250f;
        baseThermonuclearArmor = 150f;
        baseBiochemicalArmor = 150f;
        baseElectromagneticArmor = 150f;

        experienceBounty = 5f;
    }
}
