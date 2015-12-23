using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static Player player;

	public PolygonAgent agent;
	protected Rigidbody2D rb2D;//not like anything will inherit though

	public int rotateScheme;
	public int cameraScheme;

	void Awake () {
		if (player == null) {//like a singleton
			//DontDestroyOnLoad (gameObject);
			player = this;
		} else { //if (menuColors != null)
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
			float offsetRotation = (targetRotation - currentRotation) % 360;
			
			if (offsetRotation < 0f) {
				offsetRotation += 360f;
			}

			if (rotateScheme == 0) {//recommended with cameraScheme == 0
				if (offsetRotation > 0f && offsetRotation <= 180f) {//0-360=-360 and 180-360=-180
					rb2D.AddTorque (agent.torque);//turn left max
				} else {//180-360=-180 and 360-360=0
					rb2D.AddTorque (-agent.torque);//turn right max
				}
			} else if (rotateScheme == 1) {//recommended with cameraScheme == 1
				if (offsetRotation > 0f && offsetRotation <= 90f) {									//(offsetRotation > 0f && offsetRotation <= 45f)
					rb2D.AddTorque (agent.torque * offsetRotation / 90f);//turn left slowly			//(agent.torque * offsetRotation/45f)
				} else if (offsetRotation > 270f && offsetRotation < 360f) {						//(offsetRotation > 315f && offsetRotation < 360f)
					rb2D.AddTorque (agent.torque * (offsetRotation - 360f) / 90f);//turn right slowly 	//(-agent.torque * (offsetRotation-360f)/-45f)
				} else if (offsetRotation > 0f && offsetRotation <= 180f) {//0-360=-360 and 180-360=-180
					rb2D.AddTorque (agent.torque);//turn left max
				} else if (offsetRotation > 180f && offsetRotation < 360f) {//180-360=-180 and 360-360=0
					rb2D.AddTorque (-agent.torque);//turn right max
				}
			}
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

		if (cameraScheme == 0) {
			rb2D.AddForce (new Vector2 (horizForce, vertForce));
		} else if (cameraScheme == 1) {
			rb2D.AddRelativeForce (new Vector2 (horizForce, vertForce));
		}
		//either way, it's relative to camera
	}

	void Fire () {
		if (Input.GetMouseButton (0)) {
			agent.abilityList[0].Activate (transform, agent);
		}
	}
}
