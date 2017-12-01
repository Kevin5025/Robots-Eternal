using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Increases your force for 2 seconds. 
 * This impacts your movement speed
 */
public class Sprint : ActivatableEquipable {
    float duration;

    public Sprint () : base(EquipableClass.Ability, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 10.0f) {
        duration = 4.0f;
    }

    public override void Actuate (CircleAgent casterAgent) {
        base.Actuate(casterAgent);
        casterAgent.force *= 1.4f;
        PrefabReferences.prefabReferences.StartCoroutine(durationTimeout(casterAgent));
    }

    IEnumerator durationTimeout (CircleAgent casterAgent) {
        yield return new WaitForSeconds(duration);
        casterAgent.force /= 1.4f;
    }

}
