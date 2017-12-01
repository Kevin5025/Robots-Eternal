using System;
using UnityEngine;

public class Wall : Boundary {

    // Use this for initialization
    protected override void Start () {
        base.Start();
        int minAllIndicesX = EnvironmentManager.environmentManager.minAllIndicesX;
        int minAllIndicesY = EnvironmentManager.environmentManager.minAllIndicesY;
        for (int indexX=this.looseMinIndexX - minAllIndicesX; indexX<=this.looseMaxIndexX - minAllIndicesX; indexX++) {
            for (int indexY=this.looseMinIndexY - minAllIndicesY; indexY<=this.looseMaxIndexY - minAllIndicesY; indexY++) {
                EnvironmentManager.environmentManager.looseEnvironmentGrid[indexX, indexY] = true;
            }
        }
        for (int indexX = this.tightMinIndexX - minAllIndicesX; indexX <= this.tightMaxIndexX - minAllIndicesX; indexX++) {
            for (int indexY = this.tightMinIndexY - minAllIndicesY; indexY <= this.tightMaxIndexY - minAllIndicesY; indexY++) {
                EnvironmentManager.environmentManager.tightEnvironmentGrid[indexX, indexY] = true;
                EnvironmentManager.environmentManager.looseEnvironmentGrid[indexX, indexY] = false;
            }
        }
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
	}
}
