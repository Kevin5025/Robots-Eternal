using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Behavior for the towers
 */
public class TowerController : AgentController {

    // Use this for initialization
    protected override void Start () {
        base.Start();
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(GetType().ToString());
        StartCoroutine(KeepFindClosestHostileAgent());
    }

    // Update is called once per frame
    protected override void FixedUpdate () {
        base.FixedUpdate();
        if (closestHostileAgentGameObject != null) {
            targetPosition = closestHostileAgentGameObject.transform.position;
        }
        Hunt(targetPosition);
    }
}
