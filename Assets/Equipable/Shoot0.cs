using UnityEngine;
using System.Collections;

/**
 * One pistol
 */
public class Shoot0 : ActivatableEquipable {

	public Shoot0 () : base(EquipableClass.Ability, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.3f) {
		
	}

    /**
     * Then shoots a projectile in that direction infront of where casterAgent is facing. 
     * Projectiles will simply pass through friendly units. 
     */
	public override void Actuate (CircleAgent casterAgent) {
		base.Actuate(casterAgent);

        Vector2 headPosition = casterAgent.transform.TransformPoint(new Vector2(0, 0.75f * casterAgent.radius));
        GameObject projectileGameObject = GameObject.Instantiate(PrefabReferences.prefabReferences.circleSmall2, headPosition, casterAgent.transform.rotation);
        //Vector3 forwardPosition = casterTransform.TransformPoint(new Vector2(0, 1f));
        //Vector2 forwardDirection = forwardPosition - casterTransform.position;
        Vector3 forwardDirection = casterAgent.transform.TransformDirection(new Vector2(0, 1f));
        projectileGameObject.GetComponent<Rigidbody2D>().velocity = forwardDirection * 10f;

		Projectile projectile = projectileGameObject.AddComponent<Projectile>();
        projectile.team = casterAgent.team;
        projectile.casterAgent = casterAgent;
        projectile.mechanicalDamage = casterAgent.mechanicalWeapon;

        //AudioManager.audioManager.silencerAudioSource.Play();
	}
}
