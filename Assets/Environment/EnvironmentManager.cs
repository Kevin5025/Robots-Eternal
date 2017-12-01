using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Stores representation of the map for A-Star search. 
 */
public class EnvironmentManager : MonoBehaviour {

    public static EnvironmentManager environmentManager;


    
    public GameObject[] boundaryGameObjectArray;
    public int maxAllX;
    public int minAllIndicesX;
    public int maxAllY;
    public int minAllIndicesY;

    public bool[,] tightEnvironmentGrid;
    public bool[,] looseEnvironmentGrid;

    void Awake () {
        if (environmentManager == null) {
            environmentManager = this;
        } else {
            Destroy(gameObject);
        }
    }

    /**
     * If the boundaries encompass a 100x100 unit^2 area, then environmentGrid will be a 101x101 2d array
     */
    void Start () {
        maxAllX = int.MinValue;
        minAllIndicesX = int.MaxValue;
        maxAllY = int.MinValue;
        minAllIndicesX = int.MaxValue;
        for (int b=0; b<boundaryGameObjectArray.Length; b++) {
            Boundary boundary = boundaryGameObjectArray[b].GetComponent<Boundary>();
            maxAllX = boundary.looseMaxIndexX > maxAllX ? boundary.looseMaxIndexX : maxAllX;
            minAllIndicesX = boundary.looseMinIndexX < minAllIndicesX ? boundary.looseMinIndexX : minAllIndicesX;
            maxAllY = boundary.looseMaxIndexY > maxAllY ? boundary.looseMaxIndexY : maxAllY;
            minAllIndicesY = boundary.looseMinIndexY < minAllIndicesY ? boundary.looseMinIndexY : minAllIndicesY;
        }
        looseEnvironmentGrid = new bool[maxAllX-minAllIndicesX+1, maxAllY-minAllIndicesY+1];
        tightEnvironmentGrid = new bool[maxAllX - minAllIndicesX + 1, maxAllY - minAllIndicesY + 1];
    }
	
	/**
     * Prints environmentGrid on keypress P
     */
	void Update () {
        if (Input.GetKey(KeyCode.P)) {
            for (int indexX = minAllIndicesX; indexX <= maxAllX; indexX++) {
                for (int indexY = minAllIndicesY; indexY <= maxAllY; indexY++) {
                    if (looseEnvironmentGrid[indexX - minAllIndicesX, indexY - minAllIndicesY]) {
                        Vector2 position = 0.5f * new Vector2(indexX, indexY);
                        Instantiate(PrefabReferences.prefabReferences.circleSmall2, position, transform.rotation);
                    }
                }
            }
        }
    }

    /**
     * returns an array of the 8 neighbor indices or null if array out of bounds
     */
    public int[][] getNeighbors (int indexX, int indexY) {
        int[][] neighbors = new int[8][];

        bool N = indexY + 1 < looseEnvironmentGrid.GetLength(1);
        bool S = indexY - 1 >= 0;
        bool E = indexX + 1 < looseEnvironmentGrid.GetLength(0);
        bool W = indexX - 1 >= 0;
        bool NE = N && E;
        bool SW = S && W;
        bool SE = S && E;
        bool NW = N && W;

        addToNeighbors(neighbors, 0, N, indexX, indexY + 1);
        addToNeighbors(neighbors, 1, S, indexX, indexY - 1);
        addToNeighbors(neighbors, 2, E, indexX + 1, indexY);
        addToNeighbors(neighbors, 3, W, indexX - 1, indexY);
        addToNeighbors(neighbors, 4, NE, indexX + 1, indexY + 1);
        addToNeighbors(neighbors, 5, SW, indexX - 1, indexY - 1);
        addToNeighbors(neighbors, 6, SE, indexX + 1, indexY - 1);
        addToNeighbors(neighbors, 7, NW, indexX - 1, indexY + 1);

        return neighbors;
    }

    /**
     * Helper function for getNeighbors
     */
    private void addToNeighbors (int[][] neighbors, int neighborsIndex, bool N, int indexX, int indexY) {
        if (N) {
            neighbors[neighborsIndex] = new int[2] { indexX, indexY };
            //Debug.Log("addToNeighbors: " + indexX + ", " + indexY);
        } else {
            neighbors[neighborsIndex] = null;
            //Debug.Log("addToNeighbors: null");
        }
    }

    public float getPositionX (int indexX) {
        return 0.5f * (indexX + minAllIndicesX);
    }

    public float getPositionY (int indexY) {
        return 0.5f * (indexY + minAllIndicesY);
    }

    public int getIndexX (float positionX) {
        return (int)Math.Round(2f * positionX) - minAllIndicesX;
    }

    public int getIndexY (float positionY) {
        return (int)Math.Round(2f * positionY) - minAllIndicesY;
    }
}
