using UnityEngine;
using System.Collections;

/**
 * This is anything that is important enough to perpetually respawn. 
 */
public class CircleHero : CircleAgent {

	protected bool eliminated;
	protected float respawnTime;

	// Use this for initialization
	protected override void Start () {
		base.Start();

        inventoryEquipableArray[0] = new Shoot1();
        equipmentEquipableArray[8] = inventoryEquipableArray[0];
        inventoryEquipableArray[1] = new Axe();
        equipmentEquipableArray[9] = inventoryEquipableArray[1];

        eliminated = false;
		respawnTime = 10f;

        stoneOreBounty = 25f;
        experienceBounty = 25f;
    }

	protected override void EliminateSelf () {
		eliminated = true;
		healthBarContainerImage.enabled = false;
        experienceBarContainerImage.enabled = false;
        StartCoroutine(Respawn());
	}

    /**
     * Respawning logic to just reinitialize any necessary values to be good as new. 
     * Respawns at spawn point. 
     */
	protected virtual IEnumerator Respawn () {
		yield return new WaitForSeconds(respawnTime - fadeTime);
		health = maxHealth;
        defunct = false; eliminated = false;
		gameObject.GetComponent<Collider2D>().enabled = true;
		spriteRenderer.color = new Color(r, g, b, 1f);
		healthBarContainerImage.enabled = true;
        experienceBarContainerImage.enabled = true;

        GameObject spawnPoint = SpawnManager.spawnManager.GetTeamHeroSpawnPointGameObject(team);
		if (spawnPoint != null) {
			transform.position = spawnPoint.transform.position;
			transform.rotation = spawnPoint.transform.rotation;
		}
	}
}
