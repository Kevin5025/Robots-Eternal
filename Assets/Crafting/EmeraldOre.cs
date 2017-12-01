using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * weak to biochemical
 */
public class EmeraldOre : Ore {//green

    protected override void Start () {
        base.Start();
        oreClass = OreClass.Emerald;
        baseBiochemicalArmor = 50f;

        experienceBounty = 10f;
    }
}
