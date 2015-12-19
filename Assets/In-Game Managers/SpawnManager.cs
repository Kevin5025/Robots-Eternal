using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public static SpawnManager spawnManager;

	public GameObject spawnPointGameObjectStock;//almost purely for visual, but also keeps position and rotation joined
	public GameObject pentagonAgentGameObjectStock;
	

	public GameObject blueRespawnPointGameObject;
	public GameObject redRespawnPointGameObject;

	public GameObject agent1;
	public GameObject agent2;

	void Awake() {
		if (spawnManager == null) {//like a singleton
			//DontDestroyOnLoad (gameObject);
			spawnManager = this;
		} else { //if (menuColors != null)
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		blueRespawnPointGameObject = (GameObject) Instantiate (spawnPointGameObjectStock, new Vector2 (0f, -1f), Quaternion.identity);
		redRespawnPointGameObject = (GameObject) Instantiate (spawnPointGameObjectStock, new Vector2 (0f, 1f), Quaternion.Euler (0, 0, 180));

		agent1 = (GameObject) Instantiate (pentagonAgentGameObjectStock, blueRespawnPointGameObject.transform.position, blueRespawnPointGameObject.transform.rotation);
		agent2 = (GameObject) Instantiate (pentagonAgentGameObjectStock, redRespawnPointGameObject.transform.position, redRespawnPointGameObject.transform.rotation);

		MainCamera.mainCamera.playerTransform = agent1.transform;

		agent1.AddComponent<Player> ();
		agent1.GetComponent<Agent> ().team = Entity.Team.BLUE;
		agent2.GetComponent<Agent> ().team = Entity.Team.RED;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
