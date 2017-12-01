using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallVassal1 : ActivatableEquipable {

    public SmallVassal1 () : base(EquipableClass.SmallVassal, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 20f) {

    }

    public override void Actuate (CircleAgent casterAgent) {
        base.Actuate(casterAgent);

        GameObject vassalAgentGameObject = GameObject.Instantiate(SpawnManager.spawnManager.circleAgentGameObjectPrefab2, casterAgent.transform.position, casterAgent.transform.rotation);
        //vassalAgentGameObject.transform.SetParent(casterAgent.transform);
        CircleAgent vassalAgent = vassalAgentGameObject.AddComponent<CircleAgent>();
        vassalAgent.team = casterAgent.team;
        PrefabReferences.prefabReferences.StartCoroutine(EquipVassal(vassalAgent));
        VassalController vassalController = vassalAgentGameObject.AddComponent<VassalController>();
        vassalController.lordAgent = casterAgent;
    }

    public IEnumerator EquipVassal(CircleAgent vassalAgent) {
        yield return new WaitForSeconds(1f);
        vassalAgent.inventoryEquipableArray[0] = new Mine0();
    }
}
