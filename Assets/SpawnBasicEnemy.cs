using UnityEngine;
using System.Collections;

public class SpawnBasicEnemy : MonoBehaviour {
    public int interval;
    public int hp = 100;
    public GameObject EnemyToSpawn;
    public Entity.Team team = Entity.Team.RED;

	// Use this for initialization
	void Start () {
        StartCoroutine(spawnEnemies());
	}

    IEnumerator spawnEnemies()
    {
        while (hp > 0)
        {
            //GameObject baddie = GameObject.c
            Instantiate(EnemyToSpawn, transform.position, transform.rotation);
            yield return new WaitForSeconds(interval);
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
