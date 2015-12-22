using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Entity : Actuator {

	public float health;
	public float maxHealth;
	public float mechanicalArmor;//will also have (bio)chemical, electromagnetic, thermal, nuclear, radiant?, etc. 
	public bool defunct;//aka dead, destroyed, etc. 

	protected GameObject healthBarContainerGameObject;
	protected Image healthBarContainerImage;

	// Use this for initialization
	protected override void Start() {
		base.Start();
		defunct = false;

		//TODO: do not sure healthbar for low hp projectile
		healthBarContainerGameObject = (GameObject)Instantiate(HUDManager.hUDManager.healthBarContainerStock, new Vector2(transform.position.x, transform.position.y + 0.6f), Quaternion.identity);
		healthBarContainerGameObject.GetComponentInChildren<ResourceBar>().targetTransform = transform;
		healthBarContainerGameObject.GetComponentInChildren<ResourceBar>().targetEntity = this;
		healthBarContainerImage = healthBarContainerGameObject.GetComponent<Image>();
	}

	protected override void FixedUpdate() {
		base.FixedUpdate();

		if (!defunct) {
			if (health < maxHealth) {
				health += Time.deltaTime * maxHealth / 100;//
			}
			else if (health > maxHealth) {
				health = maxHealth;
			}
			if (health <= 0) {
				health = 0;
				Expire();
			}
		}
	}

	protected virtual void Expire() {
		defunct = true;
		gameObject.GetComponent<Collider2D>().enabled = false;
		StartCoroutine(Fade());
	}

	protected virtual IEnumerator Fade() {
		spriteRenderer.color = new Color(r, g, b, 0.25f);//instant fade
		yield return new WaitForSeconds(1f);
		EliminateSelf();
	}

	protected virtual void EliminateSelf() {
		Destroy(healthBarContainerGameObject);
		Destroy(gameObject);
	}

	public void takeDiscreteDamage(float damage) {
		health -= damage * (100f / (100f + mechanicalArmor));
	}

	public void takeContinuousDamage(float damage) {//to be called in a float for-loop
		health -= Time.fixedDeltaTime * damage * (100f / (100f + mechanicalArmor));
	}
}
