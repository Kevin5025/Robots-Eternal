using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VassalController : AgentController {

    public CircleAgent lordAgent;//set by lord
    protected override void Start () {
        base.Start();
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(GetType().ToString());
        //lordAgent = transform.parent.GetComponent<CircleAgent>();
        StartCoroutine(KeepFindClosestHostileAgent());
        StartCoroutine(KeepFindAStarSearchPath());
    }

    protected override void FixedUpdate () {
        base.FixedUpdate();
        if (lordAgent != null) {
            targetPosition = lordAgent.transform.position;
        } else {
            targetPosition = closestHostileAgentGameObject.transform.position;
        }
        HuntPath();
    }

}
