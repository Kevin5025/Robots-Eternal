using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObjective : CircleTower {

    protected override void Start () {
        base.Start();
        inventoryEquipableArray[0] = new Shoot13();
    }

    protected override void EliminateSelf () {
        if (!HUDManager.hUDManager.victoryText.text.Contains("win")) {
            HUDManager.hUDManager.victoryText.text = GetOpponentTeam(team) + " wins!";
            HUDManager.hUDManager.victoryText.color = GetTeamColor(new Color(1,1,1), GetOpponentTeam(team));
        }
        base.EliminateSelf();
    }
}
