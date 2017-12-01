using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/**
 * This is anything that can be destroyed or killed. 
 */
public abstract class Entity : Actuator {

	public float health;
	public float maxHealth;
    public float healthRegenerationRate;
    //TODO damage reflection, lifesteal, etc. 

    public float baseMechanicalWeapon;//white
	public float baseMechanicalArmor;
    public float baseThermonuclearWeapon;//red
    public float baseThermonuclearArmor;
    public float baseBiochemicalWeapon;//green
    public float baseBiochemicalArmor;
    public float baseElectromagneticWeapon;//blue
    public float baseElectromagneticArmor;
    //astrocosmological //yellow
    //quantumcomputational //cyan
    //radioluminescent //magenta

    public float stoneOreBounty;
    public float experienceBounty;
    public int level;

    public bool defunct;//aka dead, destroyed, etc. 
    protected float fadeDuration = 1f;

	protected bool displayHealthBarContainerGameObject = true;
	protected GameObject healthBarContainerGameObject;
	protected Image healthBarContainerImage;

	// Use this for initialization
	protected override void Start () {
		base.Start();
		defunct = false;
        healthRegenerationRate = 0.01f;

        initializeResourceBars();
    }

    /**
     * Handles death (expiration) and regeneration. 
     */
	protected override void FixedUpdate () {
		base.FixedUpdate();
		if (!defunct) {
			if (health < maxHealth) {
				health += healthRegenerationRate * maxHealth * Time.deltaTime;//
			} else if (health > maxHealth) {
				health = maxHealth;
			}
			if (health <= 0) {
				health = 0;
				Expire();
			}
		}
	}

    /**
     * Initialize health bar. 
     */
    protected virtual void initializeResourceBars () {
        if (displayHealthBarContainerGameObject) {
            healthBarContainerGameObject = (GameObject)Instantiate(ResourceBarManager.resourceBarManager.healthBarContainerPrefab, new Vector2(transform.position.x, transform.position.y + 0.6f), Quaternion.identity);
            healthBarContainerGameObject.GetComponentInChildren<HealthBar>().targetTransform = transform;
            healthBarContainerGameObject.GetComponentInChildren<HealthBar>().targetEntity = this;
            healthBarContainerImage = healthBarContainerGameObject.GetComponent<Image>();
        }
    }

    /**
     * Occurs on death / destruction
     */
	protected virtual void Expire () {
		defunct = true;
        Collider2D collider2D = GetComponent<Collider2D>();
        if (collider2D != null) {
            collider2D.enabled = false;
        }
        StartCoroutine(Fade());
	}

    /**
     * Visually lets users know that the entity is defunct. 
     */
	protected virtual IEnumerator Fade () {
		spriteRenderer.color = new Color(r, g, b, 0.25f);//instant fade
		yield return new WaitForSeconds(fadeDuration);
		EliminateSelf();
	}

	protected virtual void EliminateSelf () {
		Destroy(healthBarContainerGameObject);
		Destroy(gameObject);
	}

    /**
     * Calculates discrete damage. 
     */
	public float takeDiscreteDamage (CircleAgent casterAgent, float damage) {//TODO damage floats
        float trueDamage = damage * (100f / (100f + baseMechanicalArmor));
        health -= trueDamage;
        if (health <= 0) {
            casterAgent.experience += experienceBounty + experienceBounty * Math.Max(0, level - casterAgent.level);
            casterAgent.oreInventoried[0] += stoneOreBounty;
        }
        return trueDamage;
	}

    /**
     * Calculates continuous damage. 
     */
    public float takeContinuousDamage (CircleAgent casterAgent, float damage) {//TODO: to be called in a float for-loop
        float trueDamage = Time.fixedDeltaTime * damage * (100f / (100f + baseMechanicalArmor));
        health -= trueDamage;
        if (health <= 0) {
            casterAgent.experience += experienceBounty + experienceBounty * Math.Max(0, level - casterAgent.level);
            casterAgent.oreInventoried[0] += stoneOreBounty;
        }
        return trueDamage;
	}
}
