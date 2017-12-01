using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Behavior for the brown pirates
 */
public class PirateController : AgentController {

    protected override void Start () {
        base.Start();
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(GetType().ToString());
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
