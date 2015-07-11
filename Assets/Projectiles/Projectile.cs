using UnityEngine;
using System.Collections;

public class Projectile : Entity {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		maxHealth = 1f;
		health = maxHealth;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	void OnCollisionEnter2D (Collision2D collision) {
		Entity collisionGameObjectEntity = collision.gameObject.GetComponent<Entity> ();
		if (collisionGameObjectEntity != null) {
			collisionGameObjectEntity.takeDiscreteDamage(20);
			Die ();
		}
	}
}
