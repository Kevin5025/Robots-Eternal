using UnityEngine;

public class AxeProjectile : Entity {

    public CircleAgent casterAgent;//set by casterAgent

    public float mechanicalDamage;
    private float timer;

    protected override void Awake () {
        base.Awake();
        GetComponent<CircleCollider2D>().enabled = false;
    }

    // Use this for initialization
    protected override void Start () {
        displayHealthBarContainerGameObject = false;
        base.Start();

        GetComponent<SpriteRenderer>().sortingLayerName = "Projectiles";
        gameObject.layer = LayersManager.layersManager.getTeamProjectileLayer(team);

        maxHealth = 7.5f;
        health = maxHealth;
        timer = 0.075f;
        fadeDuration = 0f;
        GetComponent<CircleCollider2D>().enabled = true;
    }

    /**
     * Projectiles have a limited range and duration
     */
    protected override void Update () {
        base.Update();
        timer -= Time.deltaTime;
        if (timer <= 0 || health <= 0) {
            Expire();
        }
    }

    /**
     * Damages enemies upon impact. 
     */
    void OnCollisionEnter2D (Collision2D collision) {
        Entity collisionGameObjectEntity = collision.gameObject.GetComponent<Entity>();
        if (collisionGameObjectEntity != null) {
            collisionGameObjectEntity.takeDiscreteDamage(casterAgent, mechanicalDamage);
            Expire();
        }
    }

}
