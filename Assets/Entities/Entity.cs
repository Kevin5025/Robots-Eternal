using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour {

	public float health;
	public float maxHealth;
	public float mechanicalArmor;//will also have biochemical, electromagnetic, thermal, nuclear, etc. 
	public bool eliminated;//aka dead
	
	protected SpriteRenderer spriteRenderer;
	protected float r; protected float g; protected float b;

	protected virtual void Awake () {

	}

	// Use this for initialization
	protected virtual void Start () {
		eliminated = false;//how am I able to implement a virtual function?
		
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		r = spriteRenderer.color.r; g = spriteRenderer.color.g; b = spriteRenderer.color.b;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}

	protected virtual void FixedUpdate () {

	}

	protected virtual void Die () {
		eliminated = true; 
		health = 0; 
		gameObject.GetComponent<Collider2D> ().enabled = false;
		StartCoroutine (Fade ());
	}

	protected virtual IEnumerator Fade () {
		spriteRenderer.color = new Color(r, g, b, 0.25f);
		yield return new WaitForSeconds(1f);
		Destroy (gameObject);
	}

	public void takeDiscreteDamage (float damage) {
		health -= damage * (100f / (100f + mechanicalArmor));
	}

	public void takeContinuousDamage (float damage) {//to be called in a float for-loop
		health -= Time.fixedDeltaTime * damage * (100f / (100f + mechanicalArmor));
	}
}
