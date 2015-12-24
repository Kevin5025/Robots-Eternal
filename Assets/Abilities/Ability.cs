using UnityEngine;
using System.Collections;

public abstract class Ability {
	protected float cooldownTimeout;
	protected float nextReady;

	public virtual void Activate (Transform casterTransform, PolygonAgent casterAgent) {
		if (nextReady > Time.time)
			return;

		Actuate(casterTransform, casterAgent);

		nextReady = Time.time + cooldownTimeout;//TODO: does this ever overflow? 
	}
	public virtual void Actuate (Transform casterTransform, PolygonAgent casterAgent) {

	}
}
