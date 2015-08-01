using UnityEngine;
using System.Collections;

public class Shoot : Ability {

	public override void Activate (Transform casterTransform, Agent casterAgent) {
		base.Activate (casterTransform, casterAgent);

		Vector3 head = casterTransform.TransformPoint (new Vector3 (0, casterAgent.inradius));
		GameObject projectileGameObject = (GameObject) GameObject.Instantiate (StockReferences.stockReferences.circleSmall2, head, casterTransform.rotation);
		Vector3 forward = casterTransform.TransformPoint (new Vector3 (0, 1));
		projectileGameObject.GetComponent<Rigidbody2D> ().velocity = (forward - casterTransform.position) * casterAgent.area * 5;
		projectileGameObject.AddComponent<Projectile> ();
		projectileGameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "Projectiles";

		if (casterAgent.team == Entity.Team.BLUE) {
			projectileGameObject.GetComponent<Projectile> ().team = Entity.Team.BLUE;
			projectileGameObject.layer = LayersManager.layersManager.blueProjectileLayer;
		} else if (casterAgent.team == Entity.Team.RED) {
			projectileGameObject.GetComponent<Projectile> ().team = Entity.Team.RED;
			projectileGameObject.layer = LayersManager.layersManager.redProjectileLayer;
		}
	}

}
