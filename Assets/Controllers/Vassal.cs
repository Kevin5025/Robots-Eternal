using UnityEngine;
using System.Collections;

public class Vassal : PolygonAgentController {

	public GameObject lordGameObject;

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	protected override void FixedUpdate () {
		base.FixedUpdate();
		if (agent.defunct) {
			return;
		}
		if (lordGameObject) {
			Follow(lordGameObject, 3);
		}
	}
}
