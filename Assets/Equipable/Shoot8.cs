using UnityEngine;
using System.Collections;
using System;

/**
 * Sock shotgun thing
 */
public class Shoot8 : ActivatableEquipable {

    public Shoot8 () : base(EquipableClass.Ability, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.3f) {

    }

    /**
     * Shoots five projectiles spread to an arc of theta * 4
     * Projectiles will simply pass through friendly units. 
     */
    public override void Actuate (CircleAgent casterAgent) {
        base.Actuate(casterAgent);
        float theta = (float)(Math.PI / 64);

        Vector2 headPosition0 = casterAgent.transform.TransformPoint(new Vector2(-0.375f * casterAgent.radius, 0.6495f * casterAgent.radius));
        Vector3[] forwardDirectionArray0 = new Vector3[] { casterAgent.transform.TransformDirection(new Vector2((float)Math.Sin(2 * theta), (float)Math.Cos(2 * theta))), casterAgent.transform.TransformDirection(new Vector2((float)Math.Sin(theta), (float)Math.Cos(theta))), casterAgent.transform.TransformDirection(new Vector2((float)Math.Sin(0), (float)Math.Cos(0))), casterAgent.transform.TransformDirection(new Vector2((float)Math.Sin(-theta), (float)Math.Cos(-theta))), casterAgent.transform.TransformDirection(new Vector2((float)Math.Sin(-2 * theta), (float)Math.Cos(-2 * theta))) };
        for (int d = 0; d < forwardDirectionArray0.Length; d++) {
            GameObject projectileGameObject = GameObject.Instantiate(PrefabReferences.prefabReferences.circleSmall2, headPosition0, casterAgent.transform.rotation);
            projectileGameObject.GetComponent<Rigidbody2D>().velocity = forwardDirectionArray0[d] * 10f;
            Projectile projectile = projectileGameObject.AddComponent<Projectile>();
            projectile.team = casterAgent.team;
            projectile.casterAgent = casterAgent;
            projectile.mechanicalDamage = casterAgent.mechanicalWeapon;
        }

        Vector2 headPosition1 = casterAgent.transform.TransformPoint(new Vector2(0.375f * casterAgent.radius, 0.6495f * casterAgent.radius));
        Vector3[] forwardDirectionArray1 = new Vector3[] { casterAgent.transform.TransformDirection(new Vector2((float)Math.Sin(2 * theta), (float)Math.Cos(2 * theta))), casterAgent.transform.TransformDirection(new Vector2((float)Math.Sin(theta), (float)Math.Cos(theta))), casterAgent.transform.TransformDirection(new Vector2((float)Math.Sin(0), (float)Math.Cos(0))), casterAgent.transform.TransformDirection(new Vector2((float)Math.Sin(-theta), (float)Math.Cos(-theta))), casterAgent.transform.TransformDirection(new Vector2((float)Math.Sin(-2 * theta), (float)Math.Cos(-2 * theta))) };
        for (int d = 0; d < forwardDirectionArray1.Length; d++) {
            GameObject projectileGameObject = GameObject.Instantiate(PrefabReferences.prefabReferences.circleSmall2, headPosition1, casterAgent.transform.rotation);
            projectileGameObject.GetComponent<Rigidbody2D>().velocity = forwardDirectionArray1[d] * 10f;
            Projectile projectile = projectileGameObject.AddComponent<Projectile>();
            projectile.team = casterAgent.team;
            projectile.casterAgent = casterAgent;
            projectile.mechanicalDamage = casterAgent.mechanicalWeapon;
        }

        AudioManager.audioManager.shotgunAudioSource.Play();
    }
}
