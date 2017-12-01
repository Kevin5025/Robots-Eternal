using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Weak to electromagnetic
 */
public class SapphireOre : Ore {//blue

    protected override void Start () {
        base.Start();
        oreClass = OreClass.Sapphire;
        baseElectromagneticArmor = 50f;

        experienceBounty = 10f;
    }
}
