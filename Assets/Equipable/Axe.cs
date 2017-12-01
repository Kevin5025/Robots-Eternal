using UnityEngine;
using System.Collections;

/**
 * One pistol
 */
public class Axe : ActivatableEquipable {

    public Axe () : base(EquipableClass.Ability, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 1f) {

    }

    /**
     * Then shoots a projectile in that direction infront of where casterAgent is facing. 
     * Projectiles will simply pass through friendly units. 
     */
    public override void Actuate (CircleAgent casterAgent) {
        base.Actuate(casterAgent);

        Vector2 headPosition = casterAgent.transform.TransformPoint(new Vector2(0, 0.75f * casterAgent.radius));
        GameObject projectileGameObject = GameObject.Instantiate(PrefabReferences.prefabReferences.circleAxe, headPosition, casterAgent.transform.rotation);
        //Vector3 forwardPosition = casterTransform.TransformPoint(new Vector2(0, 1f));
        //Vector2 forwardDirection = forwardPosition - casterTransform.position;
        Vector3 forwardDirection = casterAgent.transform.TransformDirection(new Vector2(0, 1f));
        projectileGameObject.GetComponent<Rigidbody2D>().velocity = forwardDirection * 20f;

        AxeProjectile projectile = projectileGameObject.AddComponent<AxeProjectile>();
        projectile.team = casterAgent.team;
        projectile.casterAgent = casterAgent;
        projectile.mechanicalDamage = 7.5f*casterAgent.mechanicalWeapon;

        //AudioManager.audioManager.silencerAudioSource.Play();
    }
}
