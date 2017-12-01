using UnityEngine;
using System.Collections;
using System;

/**
 * Abilities, Items, and Vassals
 */
public abstract class Equipable : ICloneable {

    public EquipableImageManager equipableImageManager;//Set in EquipableImageManager.cs
    public EquipableImageManager equipableEquipmentImageManager;

    public enum EquipableClass { AccessoryItem, HandItem, HeadItem, BodyItem, Ability, LargeVassal, SmallVassal };
    public EquipableClass equipableClass;

    public float value;
    public float mechanicalWeapon;
    public float mechanicalArmor;
    public float thermonuclearWeapon;
    public float thermonuclearArmor;
    public float biochemicalWeapon;
    public float biochemicalArmor;
    public float electromagneticWeapon;
    public float electromagneticArmor;

    public Equipable(EquipableClass equipableClass, float value, float mechanicalWeapon, float mechanicalArmor, float thermonuclearWeapon, float thermonuclearArmor, float biochemicalWeapon, float biochemicalArmor, float electromagneticWeapon, float electromagneticArmor) {
        this.equipableClass = equipableClass;
        this.value = value;
        this.mechanicalWeapon = mechanicalWeapon;
        this.mechanicalArmor = mechanicalArmor;
        this.thermonuclearWeapon = thermonuclearWeapon;
        this.thermonuclearArmor = thermonuclearArmor;
        this.biochemicalWeapon = biochemicalWeapon;
        this.biochemicalArmor = biochemicalArmor;
        this.electromagneticWeapon = electromagneticWeapon;
        this.electromagneticArmor = electromagneticArmor;
}

    public virtual void Activate (CircleAgent casterAgent) {//TODO get rid of casterTransform parameter

    }

    public object Clone () {
        return this.MemberwiseClone();//http://stackoverflow.com/questions/3647983/how-to-clone-an-inherited-object
    }
}
