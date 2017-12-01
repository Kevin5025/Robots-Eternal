using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This is any agent that cannot move
 */
public class CircleTower : CircleHero {

    Transform spawnPoint;

    protected override void Start () {
        base.Start();
        inventoryEquipableArray[0] = new Shoot8();

        respawnTime = 20f;
        spawnPoint = transform;
    }

    /**
     * Respawning logic to just reinitialize any necessary values to be good as new. 
     * Respawns at spawn point. 
     */
    protected override IEnumerator Respawn () {
        yield return new WaitForSeconds(respawnTime - fadeTime);
        health = maxHealth;
        defunct = false; eliminated = false;
        gameObject.GetComponent<Collider2D>().enabled = true;
        spriteRenderer.color = new Color(r, g, b, 1f);
        healthBarContainerImage.enabled = true;

        if (spawnPoint != null) {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
        }
    }
}
