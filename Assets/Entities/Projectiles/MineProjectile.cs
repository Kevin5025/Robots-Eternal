using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineProjectile : Projectile {

    protected override void Start () {
        base.Start();
        timer = 15f;
    }
}
