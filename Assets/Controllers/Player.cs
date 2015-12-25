using UnityEngine;

public class Player : MonoBehaviour {

	public static Player player;

	public PolygonAgent agent;
	protected Rigidbody2D rb2D;//not like anything will inherit though

	public int cameraScheme;
	public int rotateScheme;

	void Awake () {
		if (player == null) {//like a singleton
			//DontDestroyOnLoad (gameObject);
			player = this;
		} else {
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		agent = GetComponent<PolygonAgent> ();
		rb2D = GetComponent<Rigidbody2D> ();
		
		cameraScheme = PersistenceManager.persistenceManager.settings.cameraScheme;
		rotateScheme = PersistenceManager.persistenceManager.settings.rotateScheme;
	}
	
	// Update is called once per frame
	void Update () {
        Fire();
	}

	void FixedUpdate () {
		Rotate ();
		Move ();
	}

	void Rotate () {
		if (rotateScheme == 0 || rotateScheme == 1) {
			Vector2 mousePosition = Input.mousePosition;
			Vector2 transformPosition = Camera.main.WorldToScreenPoint (transform.position);
			//float currentRotation = transform.eulerAngles.z;//0 to 360 counterclockwise
			float currentRotation = transform.eulerAngles.z - Camera.main.transform.eulerAngles.z;
			float targetRotation = Mathf.Atan2 (mousePosition.x - transformPosition.x, mousePosition.y - transformPosition.y) * -Mathf.Rad2Deg;//0 to 180, then -180 to 0 counterclockwise
			float offsetRotation = (targetRotation - currentRotation) % 360;//between -360 and 360
			
			if (offsetRotation < -180f) {
				offsetRotation += 360f;
			} else if (offsetRotation > 180f) {
				offsetRotation -= 360f;
			}
			//between -180 and 180, where positive is still counterclockward

			//v=angularVelocity, a=angularAcceleration=alpha=torque/rotationalInertia, t=timeToDecelerate
			//vt=d versus vt-0.5at^2=d		distance vs distance while decelerating
			//v-at=0 => v=at => v/a=t		time to decelerate is v/a, of course
			//vt=d => v*v/a*t=d >= offset?	see if trajected distance overshoots offset
											//if so, then decelerate, else continue accelerating
			//rb2D.angularVelocity*rb2D.angularVelocity/(agent.torq)
			float trajectedRotation = 0.01f*Mathf.Abs(rb2D.angularVelocity)*rb2D.angularVelocity/(agent.torque/rb2D.inertia);//magic 0.01f is due to angular drag and bad calculus
				//positive is counterclock, negative is clock
			//Debug.Log("1:" + rb2D.angularVelocity + ", " + (agent.torque / rb2D.inertia));
			//Debug.Log("2:" + trajectedRotation + ", " + offsetRotation);

			//TODO: proactively slow down to avoid shaky
			if (rotateScheme == 0 || rotateScheme == 1) {//TODO: consider just collapsing/cleaning it all
				//Debug.Log("2:" + trajectedRotation + ", " + offsetRotation);
				if (trajectedRotation < offsetRotation) {
					//Debug.Log("left");
					rb2D.AddTorque(agent.torque);//turn left max
				} else {
					//Debug.Log("right");
					rb2D.AddTorque(-agent.torque);//turn right max
				}
			}
			/*
			if (rotateScheme == 0) {//recommended with cameraScheme == 0
				if (offsetRotation > 0f && offsetRotation < 180f) {
					rb2D.AddTorque (agent.torque);//turn left max
				} else {
					rb2D.AddTorque (-agent.torque);//turn right max
				}
			} else if (rotateScheme == 1) {//recommended with cameraScheme == 1
				if (offsetRotation > 0f && offsetRotation <= 90f) {									//(offsetRotation > 0f && offsetRotation <= 45f)
					rb2D.AddTorque (agent.torque * offsetRotation / 90f);//turn left slowly			//(agent.torque * offsetRotation/45f)
				} else if (offsetRotation > -90f && offsetRotation < 0f) {						//(offsetRotation > 315f && offsetRotation < 360f)
					rb2D.AddTorque (agent.torque * (offsetRotation - 360f) / 90f);//turn right slowly 	//(-agent.torque * (offsetRotation-360f)/-45f)
				} else if (offsetRotation > 0f && offsetRotation <= 180f) {
					rb2D.AddTorque (agent.torque);//turn left max
				} else if (offsetRotation > -180f && offsetRotation < 0f) {
					rb2D.AddTorque (-agent.torque);//turn right max
				}
			}
			*/
		} else if (rotateScheme == 2) {//highly recommended with cameraScheme == 1
			Vector2 mousePosition = Input.mousePosition;
			Vector2 transformPosition = Camera.main.WorldToScreenPoint (transform.position);
			float offsetHorizontal = mousePosition.x - transformPosition.x;//-400 to 400

			if (offsetHorizontal >= -400f && offsetHorizontal <= 400f) {
				rb2D.AddTorque (-agent.torque * offsetHorizontal / 400f);
			} else if (offsetHorizontal < -400f) {
				rb2D.AddTorque (agent.torque);//turn left max
			} else {//if (offsetHorizontal > 400f)
				rb2D.AddTorque (-agent.torque);//turn right max
			}
		}
	}

	void Move () {
		bool W = Input.GetKey (KeyCode.W);
		bool S = Input.GetKey (KeyCode.S);
		bool A = Input.GetKey (KeyCode.A);
		bool D = Input.GetKey (KeyCode.D);
		
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

		if (cameraScheme == 0)
			rb2D.AddForce (new Vector2 (horizForce, vertForce));
		else if (cameraScheme == 1)
			rb2D.AddRelativeForce (new Vector2 (horizForce, vertForce));
	   
		//either way, it's relative to camera
	}

	void Fire () {
		if (Input.GetMouseButton (0)) {
			agent.abilityList[0].Activate (transform, agent);
		}
	}
}
