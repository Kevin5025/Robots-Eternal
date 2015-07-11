using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Agent agent;
	protected Rigidbody2D rb2D;//not like anything will inherit though

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
		agent = GetComponent<Agent> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		Rotate ();
		Move ();
		Fire ();
	}

	void Rotate () {
		Vector2 mousePosition = Input.mousePosition;
		Vector2 shapePosition = Camera.main.WorldToScreenPoint (transform.position);
		float currentRotation = transform.eulerAngles.z;//0 to 360 counterclockwise
		float targetRotation = Mathf.Atan2 (mousePosition.x - shapePosition.x, mousePosition.y - shapePosition.y) * -Mathf.Rad2Deg;//0 to 180, then -180 to 0 counterclockwise
		float offsetRotation = (targetRotation - currentRotation)%360;
		
		if ((offsetRotation > -360 && offsetRotation <= -180) || (offsetRotation > 0 && offsetRotation <= 180)) {
			rb2D.AddTorque (agent.torque);//turn left
		} else if ((offsetRotation > -180 && offsetRotation < 0) || (offsetRotation > 180 && offsetRotation < 360)) {
			rb2D.AddTorque (-agent.torque);//turn right
		}
	}

	void Move () {
		bool W = Input.GetKey (KeyCode.W);
		bool S = Input.GetKey (KeyCode.S);
		bool A = Input.GetKey (KeyCode.A);
		bool D = Input.GetKey (KeyCode.D);
		
		float horizForce, vertForce;

		if (W && !S) {
			vertForce = agent.force;
		} else if (S && !W) {
			vertForce = -agent.force;
		} else {//if (W && S || !W && !S)
			vertForce = 0;
		}
		
		if (A && !D) {
			horizForce = -agent.force;
		} else if (D && !A) {
			horizForce = agent.force;
		} else {
			horizForce = 0;
		}
		
		if (vertForce != 0 && horizForce != 0) {
			vertForce *= MyStaticLibrary.sqrt2over2;
			horizForce *= MyStaticLibrary.sqrt2over2;
		}
		
		rb2D.AddRelativeForce (new Vector2 (horizForce, vertForce));
	}

	void Fire () {
		if (Input.GetMouseButtonDown (0)) {
			agent.abilityList[0].Activate (transform, agent);
		}
	}
}
