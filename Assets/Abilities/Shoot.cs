using UnityEngine;
using System.Collections;

public class Shoot : Ability {

	public override void Activate (Transform casterTransform, Agent casterAgent) {
		base.Activate (casterTransform, casterAgent);
		Vector3 forward = casterTransform.TransformPoint (new Vector3 (0, 1));
		GameObject projectile = (GameObject) GameObject.Instantiate (StockReferences.stockReferences.circleSmall2, forward, casterTransform.rotation);
		projectile.GetComponent<Rigidbody2D> ().velocity = (forward - casterTransform.position) * casterAgent.area * 5;
		projectile.AddComponent<Projectile> ();
	}

}
