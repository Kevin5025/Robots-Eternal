using UnityEngine;
using System.Collections;

public abstract class Actuator : MonoBehaviour {

    public enum Team { BLUE, RED };
    public Team team;

    protected SpriteRenderer spriteRenderer;
    protected float r; protected float g; protected float b;

    protected virtual void Awake() {

    }

	// Use this for initialization
    protected virtual void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (team == Team.BLUE) {
            //spriteRenderer.color = new Color(0f, 0f, 1f);
            spriteRenderer.color = new Color(0, 0, spriteRenderer.color.b);
        }
        else if (team == Team.RED) {
            //spriteRenderer.color = new Color(1f, 0f, 0f);
            spriteRenderer.color = new Color(spriteRenderer.color.r, 0, 0);
        }
        r = spriteRenderer.color.r; g = spriteRenderer.color.g; b = spriteRenderer.color.b;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	    
	}

    protected virtual void FixedUpdate() {

    }
}
