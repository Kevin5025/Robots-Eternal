using UnityEngine;
using System.Collections;

public abstract class Ability {
	protected float cooldownTimeout;
	protected float nextReadyTime;

	public virtual void Activate (Transform casterTransform, PolygonAgent casterAgent) {
		if (nextReadyTime <= Time.time) {
			Actuate(casterTransform, casterAgent);
			nextReadyTime = Time.time + cooldownTimeout;
		}
	}
	public virtual void Actuate (Transform casterTransform, PolygonAgent casterAgent) {

	}
}
