using UnityEngine;
using System.Collections;

public class Shoot : Ability {
    public Shoot()
    {
        cooldownTimeout = 0.3f;
    }

    public override void Activate(Transform casterTransform, PolygonAgent casterAgent)
    {
        if (nextReady > Time.time)
            return;

		base.Activate (casterTransform, casterAgent);
		Vector3 head = casterTransform.TransformPoint (new Vector3 (0, casterAgent.inradius));
		GameObject projectileGameObject = (GameObject) GameObject.Instantiate (StockReferences.stockReferences.circleSmall2, head, casterTransform.rotation);
		Vector3 forwardPosition = casterTransform.TransformPoint (new Vector3 (0, 1f));
		projectileGameObject.GetComponent<Rigidbody2D> ().velocity = (forwardPosition - casterTransform.position) * casterAgent.force;

		projectileGameObject.AddComponent<Projectile> ();
		if (casterAgent.team == Entity.Team.BLUE) {
			projectileGameObject.GetComponent<Projectile> ().team = Entity.Team.BLUE;
		} else if (casterAgent.team == Entity.Team.RED) {
			projectileGameObject.GetComponent<Projectile> ().team = Entity.Team.RED;
		}

        nextReady = Time.time + cooldownTimeout;
	}

}
