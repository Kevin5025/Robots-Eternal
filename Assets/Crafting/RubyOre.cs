using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Weak to thermonuclear
 */
public class RubyOre : Ore {//red

    protected override void Start () {
        base.Start();
        oreClass = OreClass.Ruby;
        baseThermonuclearArmor = 50f;

        experienceBounty = 10f;
    }
}
