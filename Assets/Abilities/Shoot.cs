using UnityEngine;
using System.Collections;

public class Shoot : Ability {

	public override void Activate (Transform casterTransform, Agent casterAgent) {
		base.Activate (casterTransform, casterAgent);

		Vector3 head = casterTransform.TransformPoint (new Vector3 (0, casterAgent.radius));
		GameObject projectileGameobject = (GameObject) GameObject.Instantiate (StockReferences.stockReferences.circleSmall2, head, casterTransform.rotation);
		Vector3 forward = casterTransform.TransformPoint (new Vector3 (0, 1));
		projectileGameobject.GetComponent<Rigidbody2D> ().velocity = (forward - casterTransform.position) * casterAgent.area * 5;
		projectileGameobject.AddComponent<Projectile> ();

		if (casterAgent.team == Entity.Team.BLUE) {
			projectileGameobject.layer = LayersManager.layersManager.blueProjectileLayer;
		} else if (casterAgent.team == Entity.Team.RED) {
			projectileGameobject.layer = LayersManager.layersManager.redProjectileLayer;
		}
	}

}
