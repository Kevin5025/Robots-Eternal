using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Used for mining ores
 */
public class Pickaxe : ActivatableEquipable {

    public Pickaxe () : base(EquipableClass.HandItem, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0.3f) {

    }

    /**
     * Finds closest ore, if any, and starts mining it if within range
     */
    public override void Actuate (CircleAgent casterAgent) {
        base.Actuate(casterAgent);

        GameObject closestOreGameObject = FindClosestOre(casterAgent);
        if (closestOreGameObject != null) {
            float distance = MyStaticLibrary.GetDistance(casterAgent.gameObject, closestOreGameObject);
            if (distance < 3f) {
                Ore closestOreEntity = closestOreGameObject.GetComponent<Ore>();
                float trueDamage = closestOreEntity.takeDiscreteDamage(casterAgent, casterAgent.mechanicalWeapon);
                closestOreEntity.GetPickaxed(casterAgent, trueDamage);
                Ore.OreClass oreClass = closestOreGameObject.GetComponent<Ore>().oreClass;
                int oreClassIndex = (int)oreClass;
                casterAgent.oreInventoried[oreClassIndex] += trueDamage;
                //Debug.Log(oreClass + " inventoried: " + casterAgent.OreInventoried[oreClassIndex]);
                HUDManager.hUDManager.UpdateOreMenu(casterAgent.oreInventoried);

                AudioManager.audioManager.pickaxeAudioSource.Play();
            }
        }
    }

    /**
     * Finds closest ore
     */
    protected GameObject FindClosestOre (CircleAgent casterAgent) {
        Ore[] oreArray = GameObject.FindObjectsOfType<Ore>();
        List<Ore> oreList = new List<Ore>();
        for (int o=0; o<oreArray.Length; o++) {
            if (oreArray[o].team != casterAgent.team && !oreArray[o].defunct) {
                oreList.Add(oreArray[o]);
            }
        }
        if (oreList.Count <= 0) {
            return null;
        }

        GameObject closestOreGameObject = oreList[0].gameObject;
        float closestOreGameObjectDistance = MyStaticLibrary.GetDistance(casterAgent.gameObject, closestOreGameObject);
        for (int o=1; o<oreList.Count; o++) {
            float distance = MyStaticLibrary.GetDistance(casterAgent.gameObject, oreList[o].gameObject);
            if (distance < closestOreGameObjectDistance) {
                closestOreGameObject = oreList[o].gameObject;
                closestOreGameObjectDistance = distance;
            }
        }

        return closestOreGameObject;
    }
}
