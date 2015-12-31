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
		Move(1f, cameraScheme==1);
	}

	void Rotate () {
		if (rotateScheme == 0 || rotateScheme == 1) {
			Rotate(Camera.main.ScreenToWorldPoint(Input.mousePosition));
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

	void Move (float power, bool relative) {
		Move(Input.GetKey (KeyCode.W), Input.GetKey (KeyCode.S), Input.GetKey (KeyCode.A), Input.GetKey (KeyCode.D), power, relative);
	}

	void Fire () {
		if (Input.GetMouseButton (0)) {
			agent.abilityList[0].Activate (transform, agent);
		}
	}
}
