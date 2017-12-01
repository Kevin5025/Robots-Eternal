using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceBar : ResourceBar {
    
    public CircleAgent targetCircleAgent;

    // Use this for initialization
    protected override void Start () {
        resourceBarWidth = targetCircleAgent.radius;//TODO: consider LoL style
        base.Start();
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
        //healthBarContainerTransform.position = Camera.main.WorldToScreenPoint (new Vector2 (targetTransform.position.x, targetTransform.position.y + 0.6f));
        Vector3 forwardPosition = Camera.main.transform.TransformPoint(new Vector3(0, -0.6f));
        resourceBarContainerTransform.position = Camera.main.WorldToScreenPoint(targetTransform.position + forwardPosition - Camera.main.transform.position);
        //resourceBarContainerTransform.rotation = targetTransform.rotation;//as if the Entity is still the parent
        transform.localScale = new Vector3(targetCircleAgent.experience / targetCircleAgent.maxExperience, 1f, 1f);
    }
}
