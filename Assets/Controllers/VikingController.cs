using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingController : AgentController {

    protected override void Start () {
        base.Start();
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(GetType().ToString());
        agent.force *= 1.2f;
        agent.torque *= 1.2f;
        agent.inventoryEquipableArray[0] = new Axe();
        StartCoroutine(KeepFindClosestHostileAgent());
        StartCoroutine(KeepFindAStarSearchPath());
    }

    protected override void FixedUpdate () {
        base.FixedUpdate();
        if (closestHostileAgentGameObject != null) {
            targetPosition = closestHostileAgentGameObject.transform.position;
        }
        HuntPath();
    }
}
