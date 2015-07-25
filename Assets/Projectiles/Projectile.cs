using UnityEngine;
using System.Collections;

public class Projectile : Entity {

	private float mechanicalDamage;
	private float timer;

	protected override void Awake () {
		base.Awake ();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		maxHealth = 1f;
		health = maxHealth;
		mechanicalDamage = 20f;
		timer = 2f;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		timer -= Time.deltaTime;
		if (timer <= 0 || health <= 0) {
			Die ();
		}
	}

	void OnCollisionEnter2D (Collision2D collision) {
		Entity collisionGameObjectEntity = collision.gameObject.GetComponent<Entity> ();
		if (collisionGameObjectEntity != null) {
			collisionGameObjectEntity.takeDiscreteDamage(mechanicalDamage);
			Die ();
		}
	}
}
