using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Very basic heal skill. 
 */
public class Heal : ActivatableEquipable {
    public Heal () : base (EquipableClass.Ability, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 3.0f) {

    }

    public override void Actuate (CircleAgent casterAgent) {
        base.Actuate(casterAgent);
        casterAgent.health += 0.2f*casterAgent.maxHealth;
    }
}
