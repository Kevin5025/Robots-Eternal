using UnityEngine;
using System.Collections;

/**
 * Two pistols
 */
public class Shoot1 : ActivatableEquipable {

    public Shoot1 () : base(EquipableClass.Ability, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.3f) {

    }

    /**
     * Then shoots a projectile in that direction infront of where casterAgent is facing. 
     * Projectiles will simply pass through friendly units. 
     */
    public override void Actuate (CircleAgent casterAgent) {
        base.Actuate(casterAgent);

        Vector3 forwardDirection = casterAgent.transform.TransformDirection(new Vector2(0, 1f));
        //0.75*sin(pi/6)//0.75cos(pi/6)
        Vector2 headPosition0 = casterAgent.transform.TransformPoint(new Vector2(-0.375f * casterAgent.radius, 0.6495f * casterAgent.radius));
        GameObject projectileGameObject0 = GameObject.Instantiate(PrefabReferences.prefabReferences.circleSmall2, headPosition0, casterAgent.transform.rotation);
        projectileGameObject0.GetComponent<Rigidbody2D>().velocity = forwardDirection * 10f;
        Projectile projectile0 = projectileGameObject0.AddComponent<Projectile>();
        projectile0.team = casterAgent.team;
        projectile0.casterAgent = casterAgent;
        projectile0.mechanicalDamage = casterAgent.mechanicalWeapon;

        Vector2 headPosition1 = casterAgent.transform.TransformPoint(new Vector2(0.375f * casterAgent.radius, 0.6495f * casterAgent.radius));
        GameObject projectileGameObject1 = GameObject.Instantiate(PrefabReferences.prefabReferences.circleSmall2, headPosition1, casterAgent.transform.rotation);
        projectileGameObject1.GetComponent<Rigidbody2D>().velocity = forwardDirection * 10f;
        Projectile projectile1 = projectileGameObject1.AddComponent<Projectile>();
        projectile1.team = casterAgent.team;
        projectile1.casterAgent = casterAgent;
        projectile1.mechanicalDamage = casterAgent.mechanicalWeapon;

        AudioManager.audioManager.silencerAudioSource.Play();
    }
}
