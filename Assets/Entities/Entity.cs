using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour {

	public float health;
	public float maxHealth;
	public float mechanicalArmor;//will also have (bio)chemical, electromagnetic, thermal, nuclear, radiant?, etc. 
	public bool expired;//aka dead, destroyed, etc. 

	public enum Team {BLUE, RED};
	public Team team;
	
	protected SpriteRenderer spriteRenderer;
	protected float r; protected float g; protected float b;

	protected virtual void Awake () {

	}

	// Use this for initialization
	protected virtual void Start () {
		expired = false;

		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		if (team == Team.BLUE) {
			spriteRenderer.color = new Color(0f, 0f, 1f);
		} else if (team == Team.RED) {
			spriteRenderer.color = new Color(1f, 0f, 0f);
		}
		r = spriteRenderer.color.r; g = spriteRenderer.color.g; b = spriteRenderer.color.b;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}

	protected virtual void FixedUpdate () {

	}

	protected virtual void Expire () {
		expired = true; 
		gameObject.GetComponent<Collider2D> ().enabled = false;
		StartCoroutine (Fade ());
	}

	protected virtual IEnumerator Fade () {
		spriteRenderer.color = new Color (r, g, b, 0.25f);//instant fade
		yield return new WaitForSeconds (1f);
		Destroy (gameObject);
	}

	public void takeDiscreteDamage (float damage) {
		health -= damage * (100f / (100f + mechanicalArmor));
	}

	public void takeContinuousDamage (float damage) {//to be called in a float for-loop
		health -= Time.fixedDeltaTime * damage * (100f / (100f + mechanicalArmor));
	}
}
