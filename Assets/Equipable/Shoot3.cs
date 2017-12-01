using UnityEngine;
using System.Collections;
using System;

/**
 * Sock shotgun thing
 */
public class Shoot3 : ActivatableEquipable {

    public Shoot3 () : base(EquipableClass.Ability, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.3f) {

    }

    /**
     * Shoots five projectiles spread to an arc of theta * 4
     * Projectiles will simply pass through friendly units. 
     */
    public override void Actuate (CircleAgent casterAgent) {
        base.Actuate(casterAgent);

        Vector2 headPosition = casterAgent.transform.TransformPoint(new Vector2(0f, 0.75f * casterAgent.radius));
        float theta = (float) (Math.PI / 64);
        Vector3[] forwardDirectionArray = new Vector3[] { casterAgent.transform.TransformDirection(new Vector2((float) Math.Sin(2*theta), (float) Math.Cos(2*theta))), casterAgent.transform.TransformDirection(new Vector2((float)Math.Sin(theta), (float)Math.Cos(theta))), casterAgent.transform.TransformDirection(new Vector2((float)Math.Sin(0), (float)Math.Cos(0))), casterAgent.transform.TransformDirection(new Vector2((float)Math.Sin(-theta), (float)Math.Cos(-theta))), casterAgent.transform.TransformDirection(new Vector2((float)Math.Sin(-2 * theta), (float)Math.Cos(-2 * theta))) };
        for (int d=0; d<forwardDirectionArray.Length; d++) {
            GameObject projectileGameObject = GameObject.Instantiate(PrefabReferences.prefabReferences.circleSmall2, headPosition, casterAgent.transform.rotation);
            projectileGameObject.GetComponent<Rigidbody2D>().velocity = forwardDirectionArray[d] * 10f;
            Projectile projectile = projectileGameObject.AddComponent<Projectile>();
            projectile.team = casterAgent.team;
            projectile.casterAgent = casterAgent;
            projectile.mechanicalDamage = casterAgent.mechanicalWeapon;
        }

        AudioManager.audioManager.shotgunAudioSource.Play();
    }
}
