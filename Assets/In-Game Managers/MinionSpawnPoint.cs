using UnityEngine;
using System.Collections;
using System;

/**
 * Respawn point for hero and spawn point for minions. 
 */
public class MinionSpawnPoint : SpawnPoint {

    //public GameObject hero;
    public Type minionController;//set in SpawnManager
    
    public float minionWaveInterval = 30;
    Vector3 leftPosition;
    Vector3 forwardPosition;
    Vector3 rightPosition;
    //Vector3 positionCenter;

	// Use this for initialization
	protected override void Start () {
        base.Start();

        leftPosition = transform.rotation * new Vector3(-0.2f, -0.1f, 0f);
        forwardPosition = transform.rotation * new Vector3(0f, 0.24f, 0f);
        rightPosition = transform.rotation * new Vector3(0.2f, -0.1f, 0f);
        //positionCenter = transform.rotation * new Vector3(0f, 0f, 0f);

        StartCoroutine(spawnMinionWaves());
	}

    /**
     * Instantiates minions
     */
    IEnumerator spawnMinionWaves() {
        while (true) {
            GameObject leftMinion = (GameObject) Instantiate(SpawnManager.spawnManager.circleAgentGameObjectPrefab2, transform.position + leftPosition, transform.rotation);
            GameObject forwardMinion = (GameObject) Instantiate(SpawnManager.spawnManager.circleAgentGameObjectPrefab2, transform.position + forwardPosition, transform.rotation);
            GameObject rightMinion = (GameObject) Instantiate(SpawnManager.spawnManager.circleAgentGameObjectPrefab2, transform.position + rightPosition, transform.rotation);
            //GameObject minionCenter = (GameObject)Instantiate(SpawnManager.spawnManager.circleAgentGameObjectPrefab2, transform.position + positionCenter, transform.rotation);

            leftMinion.AddComponent<CircleAgent>();
            leftMinion.GetComponent<CircleAgent>().team = GetComponent<MinionSpawnPoint>().team;
            leftMinion.AddComponent(minionController);
            forwardMinion.AddComponent<CircleAgent>();
            forwardMinion.GetComponent<CircleAgent>().team = GetComponent<MinionSpawnPoint>().team;
            forwardMinion.AddComponent(minionController);
            rightMinion.AddComponent<CircleAgent>();
            rightMinion.GetComponent<CircleAgent>().team = GetComponent<MinionSpawnPoint>().team;
            rightMinion.AddComponent(minionController);
            //minionCenter.AddComponent<CircleAgent>();
            //minionCenter.GetComponent<CircleAgent>().team = GetComponent<SpawnPoint>().team;
            //minionCenter.AddComponent(minionController);

            yield return new WaitForSeconds(minionWaveInterval);
        }
    }

	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}
}
