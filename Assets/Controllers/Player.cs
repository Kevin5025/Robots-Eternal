using UnityEngine;

public class Player : PolygonAgentController {

	public static Player player;

	public int cameraScheme;
	public int rotateScheme;//TODO: eventually change to controlScheme

	protected override void Awake () {
		base.Awake();

		if (player == null) {//like a singleton
			//DontDestroyOnLoad (gameObject);
			player = this;
		} else {
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	protected override void Start () {
		base.Start();

		cameraScheme = PersistenceManager.persistenceManager.settings.cameraScheme;
		rotateScheme = PersistenceManager.persistenceManager.settings.rotateScheme;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();

        Fire();
	}

	protected override void FixedUpdate () {
		base.FixedUpdate();

		//Rotate(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		//Move(Camera.main.ScreenToWorldPoint(Input.mousePosition), true);
		Rotate();
		Move(cameraScheme==1);
	}

	void Rotate () {
		if (rotateScheme == 0 || rotateScheme == 1) {
			Rotate(Camera.main.ScreenToWorldPoint(Input.mousePosition));

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

	void Move (bool relative) {
		Move(Input.GetKey (KeyCode.W), Input.GetKey (KeyCode.S), Input.GetKey (KeyCode.A), Input.GetKey (KeyCode.D), relative);
	}

	void Fire () {
		if (Input.GetMouseButton (0)) {
			agent.abilityList[0].Activate (transform, agent);
		}
	}
}
