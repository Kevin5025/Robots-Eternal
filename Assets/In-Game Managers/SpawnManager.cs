using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public static SpawnManager spawnManager;

	public GameObject spawnPointGameObjectStock;//keeps position and rotation
	public GameObject pentagonAgentGameObjectStock;
    public GameObject triangleAgentGameObjectStock;
	
	public GameObject blueRespawnPointGameObject;//TODO: make not public and use a public method instead
	public GameObject redRespawnPointGameObject;

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

        blueRespawnPointGameObject.GetComponent<SpawnPoint>().team = Actuator.Team.BLUE;
        redRespawnPointGameObject.GetComponent<SpawnPoint>().team = Actuator.Team.RED;

		GameObject agent1 = (GameObject) Instantiate (pentagonAgentGameObjectStock, blueRespawnPointGameObject.transform.position, blueRespawnPointGameObject.transform.rotation);
		GameObject agent2 = (GameObject) Instantiate (pentagonAgentGameObjectStock, redRespawnPointGameObject.transform.position, redRespawnPointGameObject.transform.rotation);

		agent1.AddComponent<PolygonHero>();
		agent2.AddComponent<PolygonHero>();

		agent1.GetComponent<PolygonHero>().team = Actuator.Team.BLUE;
		agent1.GetComponent<PolygonHero>().sides = 5;
		agent2.GetComponent<PolygonHero>().team = Actuator.Team.RED;
		agent2.GetComponent<PolygonHero>().sides = 5;

		agent1.AddComponent<Player>();

        MainCamera.mainCamera.playerTransform = agent1.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
