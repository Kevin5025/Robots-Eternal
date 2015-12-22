using UnityEngine;
using System.Collections;

public class PolygonHero : PolygonAgent {

	protected bool eliminated;
	protected float respawnTime;

	// Use this for initialization
	protected override void Start() {
		base.Start();
		eliminated = false;
		respawnTime = 10f;
	}

	protected override void EliminateSelf() {
		eliminated = true;
		healthBarContainerImage.enabled = false;
		StartCoroutine(Respawn());
	}

	protected IEnumerator Respawn() {
		yield return new WaitForSeconds(respawnTime - fadeTime);
		health = maxHealth;
		defunct = false; eliminated = false;
		gameObject.GetComponent<Collider2D>().enabled = true;
		spriteRenderer.color = new Color(r, g, b, 1f);
		healthBarContainerImage.enabled = true;

		GameObject spawnPoint = null;
		if (team == Team.BLUE) {
			spawnPoint = SpawnManager.spawnManager.blueRespawnPointGameObject;
		}
		else if (team == Team.RED) {
			spawnPoint = SpawnManager.spawnManager.redRespawnPointGameObject;
		}
		if (spawnPoint != null) {
			transform.position = spawnPoint.transform.position;
			transform.rotation = spawnPoint.transform.rotation;
		}
	}
}
