using UnityEngine;
using System.Collections;

public abstract class PolygonAgentController : MonoBehaviour {

	public PolygonAgent agent;
	protected Rigidbody2D rb2D;//not like anything will inherit though

	protected virtual void Awake () {}

	// Use this for initialization
	protected virtual void Start () {
		agent = GetComponent<PolygonAgent>();
		rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {}

	protected virtual void FixedUpdate () {}

	protected virtual void Follow (GameObject targetGameObject, float targetDistanceThreshold) {
		float targetDistance = Vector2.Distance(targetGameObject.transform.position, transform.position);
		if (targetDistance > targetDistanceThreshold || Time.time % 0.5f < 0.1f) {
			Rotate(targetGameObject.transform.position);
			Move(targetGameObject.transform.position);
		} else {
			Move(true, false, !agent.rightHanded, agent.rightHanded);//offhand faces outside
		}
	}

	protected virtual void Rotate (Vector2 targetPosition) {
		float currentRotation = transform.eulerAngles.z;
		float trajectedChangeInRotation = 0.5f * Time.fixedDeltaTime * Mathf.Abs(rb2D.angularVelocity) * rb2D.angularVelocity / (agent.torque / rb2D.inertia);//magic 0.5f is due to angular drag
		float trajectedRotation = currentRotation + trajectedChangeInRotation;
		//v=angularVelocity, a=angularAcceleration=alpha=torque/rotationalInertia, t=timeToDecelerate
		//vt=d versus vt-0.5at^2=d		distance vs distance while decelerating
		//v-at=0 => v=at => v/a=t		time to decelerate is v/a, of course
		//vt=d => v*v/a*t=d >= offset?	see if trajected distance overshoots offset
		//if so, then decelerate, else continue accelerating
		//rb2D.angularVelocity*rb2D.angularVelocity/(agent.torq)

		float targetRotation = Mathf.Atan2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y) * -Mathf.Rad2Deg;//0 to 180, then -180 to 0 counterclockwise
		float offsetRotation = (targetRotation - trajectedRotation) % 360;//between -360 and 360
		if (offsetRotation < -180f) {
			offsetRotation += 360f;
		} else if (offsetRotation > 180f) {
			offsetRotation -= 360f;
		}//between -180 and 180, where positive is still counterclockward

		if (offsetRotation > 0) {
			rb2D.AddTorque(agent.torque);//turn left max
		} else {
			rb2D.AddTorque(-agent.torque);//turn right max
		}
	}

	protected virtual void Move (Vector2 targetPosition, bool relative = true) {
		Vector2 currentPosition = transform.position;
		Vector2 trajectedChangeInPosition = 40f * Time.fixedDeltaTime * new Vector2(Mathf.Abs(rb2D.velocity.x) * rb2D.velocity.x, Mathf.Abs(rb2D.velocity.y) * rb2D.velocity.y) / (agent.force / rb2D.mass);//magic 0.5f is due to linear drag
		Vector2 trajectedPosition = currentPosition + trajectedChangeInPosition;
		//vt=d => v*v/a*t=d >= offset?	see if trajected distance overshoots offset
		//if so, then decelerate, else continue accelerating

		float currentDirection = transform.eulerAngles.z;
		Vector2 offsetPosition = targetPosition - trajectedPosition;
		//alternatively get relativeOffsetPosition from offsetPosition
		float offsetDirection;
		if (relative) {
			offsetDirection = (Mathf.Atan2(offsetPosition.x, offsetPosition.y) * -Mathf.Rad2Deg - currentDirection) % 360f;
		} else {
			offsetDirection = (Mathf.Atan2(offsetPosition.x, offsetPosition.y) * -Mathf.Rad2Deg) % 360f;
		}
		if (offsetDirection < -180f) {
			offsetDirection += 360f;
		} else if (offsetDirection > 180f) {
			offsetDirection -= 360f;
		}
		
		//magic 2f
		bool W = offsetDirection > -88f && offsetDirection < 88f;
		bool D = offsetDirection > -178f && offsetDirection < -2f;
		bool S = offsetDirection > 92f || offsetDirection < -92f;
		bool A = offsetDirection > 2f && offsetDirection < 178f;
		Move(W, S, A, D, relative);
	}

	protected virtual void Move (bool W, bool S, bool A, bool D, bool relative = true) {

		float horizForce, vertForce;

		if (W && !S)
			vertForce = agent.force;
		else if (S && !W)
			vertForce = -agent.force;
		else //if (W && S || !W && !S)
			vertForce = 0;


		if (A && !D)
			horizForce = -agent.force;
		else if (D && !A)
			horizForce = agent.force;
		else
			horizForce = 0;


		if (vertForce != 0 && horizForce != 0) {
			vertForce *= MyStaticLibrary.sqrt2over2;
			horizForce *= MyStaticLibrary.sqrt2over2;
		}

		if (relative) {
			rb2D.AddRelativeForce(new Vector2(horizForce, vertForce));
		} else {
			rb2D.AddForce(new Vector2(horizForce, vertForce));
		}
	}
}
