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
	
	protected bool eliminated;
	protected float fadeTime;
	protected float fadeTimeConstant;
	protected float respawnTime;

	public List<Ability> abilityList;
	
	private GameObject healthBarContainerGameObject;

	protected override void Awake () {
		base.Awake ();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		if (team == Team.BLUE) {
			gameObject.layer = LayersManager.layersManager.blueAgentLayer;
		} else if (team == Team.RED) {
			gameObject.layer = LayersManager.layersManager.redAgentLayer;
		}

		sides = 5;//pentagon
		inradius = 0.68819f * 0.41f;//pentagon; sidelength is 0.41
		radius = 0.85065f * 0.41f;//pentagon
		area = 1.705f;//pentagon
		force = area * 15f;
		torque = area * 1.5f;//* 15f when there's no collider
		GetComponent<Rigidbody2D>().mass = area;

		maxHealth = area * 100f;
		health = maxHealth;
		mechanicalArmor = 20f;
		
		eliminated = false;
		fadeTime = 6f;
		fadeTimeConstant = 0.25f / fadeTime;
		respawnTime = 10f;

		abilityList = new List<Ability> ();
		abilityList.Add (new Shoot());

		healthBarContainerGameObject = (GameObject) Instantiate (HUDManager.hUDManager.healthBarContainerStock, new Vector2 (transform.position.x, transform.position.y + 0.6f), Quaternion.identity);
		healthBarContainerGameObject.GetComponentInChildren<ResourceBar> ().targetTransform = transform;
		healthBarContainerGameObject.GetComponentInChildren<ResourceBar> ().targetEntity = this;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	protected override void FixedUpdate () {
		base.FixedUpdate ();
		if (!expired) {
			if (health < maxHealth) {
				health += Time.deltaTime * maxHealth/100;//
			} else if (health > maxHealth) {
				health = maxHealth;
			}
			if (health <= 0) {
				health = 0; 
				Expire ();
			}
		}
	}

	protected override void Expire () {
		base.Expire ();
	}

	protected override IEnumerator Fade () {
		for (float f=0.25f; f>0; f-=Time.deltaTime * fadeTimeConstant) {
			spriteRenderer.color = new Color(r, g, b, f);
			//yield return new WaitForSeconds(1f);//3f? //is this consistent? 
			yield return null;
		}
		//Destroy (gameObject);
		eliminated = true;
		StartCoroutine (Respawn ());
	}

	protected IEnumerator Respawn () {
		yield return new WaitForSeconds (respawnTime - fadeTime);
		health = maxHealth;
		expired = false; eliminated = false;
		spriteRenderer.color = new Color (r, g, b, 1f);
		gameObject.GetComponent<Collider2D> ().enabled = true;

		GameObject spawnPoint = null;
		if (team == Team.BLUE) {
			spawnPoint = SpawnManager.spawnManager.blueRespawnPointGameObject;
		} else if (team == Team.RED) {
			spawnPoint = SpawnManager.spawnManager.redRespawnPointGameObject;
		}
		if (spawnPoint != null) {
			transform.position = spawnPoint.transform.position;
			transform.rotation = spawnPoint.transform.rotation;
		}
	}
}
