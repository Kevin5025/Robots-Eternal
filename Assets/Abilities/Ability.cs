using UnityEngine;
using System.Collections;

public abstract class Ability
{
    protected float cooldownTimeout;
    protected float nextReady;

	public virtual void Activate (Transform casterTransform, PolygonAgent casterAgent) {

	}

}
