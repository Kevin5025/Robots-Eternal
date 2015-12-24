using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public static SpawnManager spawnManager;

	public GameObject spawnPointGameObjectStock;//keeps position and rotation
	public GameObject pentagonAgentGameObjectStock;
	public GameObject triangleAgentGameObjectStock;

	public GameObject blueRespawnPointGameObject;//TODO: make not public and use a public method instead
	public GameObject redRespawnPointGameObject;

	void Awake () {
		if (spawnManager == null) {//like a singleton
			//DontDestroyOnLoad (gameObject);
			spawnManager = this;
		} else {
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		blueRespawnPointGameObject = (GameObject)Instantiate(spawnPointGameObjectStock, new Vector2(0f, -1f), Quaternion.identity);
		blueRespawnPointGameObject.GetComponent<SpawnPoint>().team = Actuator.Team.BLUE;

		redRespawnPointGameObject = (GameObject)Instantiate(spawnPointGameObjectStock, new Vector2(0f, 1f), Quaternion.Euler(0, 0, 180));
		redRespawnPointGameObject.GetComponent<SpawnPoint>().team = Actuator.Team.RED;


		GameObject blueHero = (GameObject)Instantiate(pentagonAgentGameObjectStock, blueRespawnPointGameObject.transform.position, blueRespawnPointGameObject.transform.rotation);
		blueHero.AddComponent<PolygonHero>();
		blueHero.GetComponent<PolygonHero>().team = Actuator.Team.BLUE;
		blueHero.GetComponent<PolygonHero>().sides = 5;

		blueHero.AddComponent<Player>();
		MainCamera.mainCamera.playerTransform = blueHero.transform;

		GameObject redHero = (GameObject)Instantiate(pentagonAgentGameObjectStock, redRespawnPointGameObject.transform.position, redRespawnPointGameObject.transform.rotation);
		redHero.AddComponent<PolygonHero>();
		redHero.GetComponent<PolygonHero>().team = Actuator.Team.RED;
		redHero.GetComponent<PolygonHero>().sides = 5;
	}

	// Update is called once per frame
	void Update () {

	}
}
