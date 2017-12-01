using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Minions gradually try to take control. 
 */
public class MinionController : AgentController {

    protected override void Start () {
        base.Start();
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(GetType().ToString());
        StartCoroutine(KeepFindClosestHostileAgent());
        StartCoroutine(KeepFindAStarSearchPath());
    }
    
    protected override void FixedUpdate () {
        base.FixedUpdate();
        if (closestHostileAgentGameObject != null && MyStaticLibrary.GetDistance(gameObject, closestHostileAgentGameObject) < 12f) {
            targetPosition = closestHostileAgentGameObject.transform.position;
        } else if (!SpawnManager.spawnManager.GetOpponentTowerGameObject(agent.team).GetComponent<CircleTower>().defunct) {
            targetPosition = SpawnManager.spawnManager.GetOpponentTowerGameObject(agent.team).transform.position;
        } else if (!SpawnManager.spawnManager.GetOpponentObjectiveGameObject(agent.team).GetComponent<CircleTower>().defunct) {
            targetPosition = SpawnManager.spawnManager.GetOpponentObjectiveGameObject(agent.team).transform.position;
        } else if (closestHostileAgentGameObject != null) {
            targetPosition = closestHostileAgentGameObject.transform.position;
        }
        CautiousHuntPath();
    }
}
