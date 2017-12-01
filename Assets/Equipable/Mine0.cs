using UnityEngine;
using System.Collections;

/**
 * One pistol
 */
public class Mine0 : ActivatableEquipable {

    public Mine0 () : base(EquipableClass.Ability, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.3f) {

    }

    /**
     * Then shoots a projectile in that direction infront of where casterAgent is facing. 
     * Projectiles will simply pass through friendly units. 
     */
    public override void Actuate (CircleAgent casterAgent) {
        base.Actuate(casterAgent);

        Vector2 headPosition = casterAgent.transform.TransformPoint(new Vector2(0, 0.75f * casterAgent.radius));
        GameObject projectileGameObject = GameObject.Instantiate(PrefabReferences.prefabReferences.circleSmall2, headPosition, casterAgent.transform.rotation);

        MineProjectile projectile = projectileGameObject.AddComponent<MineProjectile>();
        projectile.team = casterAgent.team;
        projectile.casterAgent = casterAgent;
        projectile.mechanicalDamage = casterAgent.mechanicalWeapon;

        //AudioManager.audioManager.silencerAudioSource.Play();
    }
}
