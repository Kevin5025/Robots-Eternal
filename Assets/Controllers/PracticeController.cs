using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Very dumb AI behavior. 
 */
public class PracticeController : AgentController {

    // Use this for initialization
    protected override void Start () {
        base.Start();
        //StartCoroutine(AStarSearch(PlayerController.player.agent.transform.position));
        targetPosition = new Vector2(-15, 15);
        StartCoroutine(KeepFindAStarSearchPath());
    }

    // Update is called once per frame
    protected override void FixedUpdate () {
        base.FixedUpdate();
        FollowPath();
    }
}
