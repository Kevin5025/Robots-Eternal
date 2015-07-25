using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent : Entity {//will be abstract <- shape <- class

	public int sides;
	public float inradius;
	public float radius;
	public float area;
	public float mass;
	public float force;
	public float torque;

	public List<Ability> abilityList;

	protected override void Awake () {
		base.Awake ();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		sides = 5;//pentagon
		inradius = 0.68819f * 0.41f;//pentagon; sidelength is 0.41
		radius = 0.85065f * 0.41f;//pentagon
		area = 1.705f;//pentagon
		force = area * 15f;
		torque = area * 1.5f;//* 15f when there's no collider
		GetComponent<Rigidbody2D>().mass = area;

		maxHealth = area * 100f;
		health = maxHealth;
		mechanicalArmor = 20;

		abilityList = new List<Ability> ();
		abilityList.Add (new Shoot());
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	protected override void FixedUpdate () {
		base.FixedUpdate ();
		if (!eliminated) {
			if (health < maxHealth) {
				health += Time.deltaTime * maxHealth/100;
			} else if (health > maxHealth) {
				health = maxHealth;
			}
			if (health <= 0) {
				Die ();
			}
		}
	}

	protected override void Die () {
		base.Die ();
		StartCoroutine (Fade ());
	}

	protected override IEnumerator Fade () {
		for (float f=0.25f; f>0; f-=Time.deltaTime) {
			spriteRenderer.color = new Color(r, g, b, f);
			yield return new WaitForSeconds(1f);//3f? //is this consistent? 
		}
		Destroy (gameObject);
	}
}
