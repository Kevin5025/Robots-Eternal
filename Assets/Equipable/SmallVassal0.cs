using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallVassal0 : ActivatableEquipable {

	public SmallVassal0 () : base (EquipableClass.SmallVassal, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 20f) {

    }

    public override void Actuate (CircleAgent casterAgent) {
        base.Actuate(casterAgent);

        GameObject vassalAgentGameObject = GameObject.Instantiate(SpawnManager.spawnManager.circleAgentGameObjectPrefab2, casterAgent.transform.position, casterAgent.transform.rotation);
        //vassalAgentGameObject.transform.SetParent(casterAgent.transform);
        CircleAgent vassalAgent = vassalAgentGameObject.AddComponent<CircleAgent>();
        vassalAgent.team = casterAgent.team;
        VassalController vassalController = vassalAgentGameObject.AddComponent<VassalController>();
        vassalController.lordAgent = casterAgent;
    }
}
