using UnityEngine;
using System.Collections;

public class Shoot : Ability {
	public Shoot() {
		cooldownTimeout = 0.3f;
	}

	public override void Actuate(Transform casterTransform, PolygonAgent casterAgent) {
		base.Actuate(casterTransform, casterAgent);

		Vector3 head = casterTransform.TransformPoint(new Vector3(0, casterAgent.inradius));
		Vector3 forwardPosition = casterTransform.TransformPoint(new Vector3(0, 1f));
		Vector3 forwardDirection = forwardPosition - casterTransform.position;

		GameObject projectileGameObject = (GameObject)GameObject.Instantiate(StockReferences.stockReferences.circleSmall2, head, casterTransform.rotation);
		projectileGameObject.GetComponent<Rigidbody2D>().velocity = forwardDirection * casterAgent.force;//TODO: consider F=ma; v=at; t=0.05 presumably 

		projectileGameObject.AddComponent<Projectile>();
		if (casterAgent.team == Entity.Team.BLUE) {
			projectileGameObject.GetComponent<Projectile>().team = Entity.Team.BLUE;
		} else if (casterAgent.team == Entity.Team.RED) {
			projectileGameObject.GetComponent<Projectile>().team = Entity.Team.RED;
		}
	}
}
