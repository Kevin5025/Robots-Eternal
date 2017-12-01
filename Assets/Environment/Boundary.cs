using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {

    public int tightMaxIndexX;
    public int tightMinIndexX;
    public int tightMaxIndexY;
    public int tightMinIndexY;

    public int looseMaxIndexX;
    public int looseMinIndexX;
    public int looseMaxIndexY;
    public int looseMinIndexY;

    // Use this for initialization
    protected virtual void Start () {
        tightMaxIndexX = (int)Math.Round(2 * transform.position.x + transform.lossyScale.x);
        tightMinIndexX = (int)Math.Round(2 * transform.position.x - transform.lossyScale.x);
        tightMaxIndexY = (int)Math.Round(2 * transform.position.y + transform.lossyScale.y);
        tightMinIndexY = (int)Math.Round(2 * transform.position.y - transform.lossyScale.y);

        looseMaxIndexX = (int)Math.Round(2 * transform.position.x + transform.lossyScale.x) + 1;
        looseMinIndexX = (int)Math.Round(2 * transform.position.x - transform.lossyScale.x) - 1;
        looseMaxIndexY = (int)Math.Round(2 * transform.position.y + transform.lossyScale.y) + 1;
        looseMinIndexY = (int)Math.Round(2 * transform.position.y - transform.lossyScale.y) - 1;
    }
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}
}
