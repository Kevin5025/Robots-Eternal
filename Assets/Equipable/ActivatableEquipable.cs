using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * An equipable that has an activatable ability
 */
public class ActivatableEquipable : Equipable {

    protected float cooldownTimeout;
    protected float nextReadyTime;

    public ActivatableEquipable(EquipableClass equipableClass, float value, float mechanicalWeapon, float mechanicalArmor, float thermonuclearWeapon, float thermonuclearArmor, float biochemicalWeapon, float biochemicalArmor, float electromagneticWeapon, float electromagneticArmor, float cooldownTimeout) : base(equipableClass, value, mechanicalWeapon, mechanicalArmor, thermonuclearWeapon, thermonuclearArmor, biochemicalWeapon, biochemicalArmor, electromagneticWeapon, electromagneticArmor) {
        this.cooldownTimeout = cooldownTimeout;
    }

    /**
     * Checks whether the user is still functional and whether the cooldown period has ended. 
     */
    public override void Activate (CircleAgent casterAgent) {
        base.Activate(casterAgent);
        if (!casterAgent.defunct && nextReadyTime <= Time.time) {
            Actuate(casterAgent);
            nextReadyTime = Time.time + cooldownTimeout;
            if (equipableImageManager != null) {
                PrefabReferences.prefabReferences.StartCoroutine(TimedNotifyCooldown());
            }
            if (equipableEquipmentImageManager != null) {
                PrefabReferences.prefabReferences.StartCoroutine(TimedEquipmentNotifyCooldown());
            }
        }
    }

    /**
     * To be override with the details of the ability
     */
    public virtual void Actuate (CircleAgent casterAgent) {
        if (equipableImageManager != null) {
            equipableImageManager.NotifyCooldown();
        }
        if (equipableEquipmentImageManager != null) {
            equipableEquipmentImageManager.NotifyCooldown();
        }
    }

    /**
     * Tells corresponding equipableImageManager to change display
     */
     protected virtual IEnumerator TimedNotifyCooldown () {
        yield return new WaitForSeconds(cooldownTimeout);
        equipableImageManager.NotifyCooldown();
     }

    /**
        * Tells corresponding equipableImageManager to change display
        */
    protected virtual IEnumerator TimedEquipmentNotifyCooldown () {
        yield return new WaitForSeconds(cooldownTimeout);
        equipableEquipmentImageManager.NotifyCooldown();
    }
}
