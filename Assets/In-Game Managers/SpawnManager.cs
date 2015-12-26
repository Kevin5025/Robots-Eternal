using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour {

	public static SpawnManager spawnManager;

	public GameObject spawnPointGameObjectStock;//keeps position and rotation
	public GameObject pentagonAgentGameObjectStock;
	public GameObject triangleAgentGameObjectStock;

	public List<GameObject> blueRespawnPointGameObjectList = new List<GameObject>();//TODO: make not public and use a public method instead
	public List<GameObject> redRespawnPointGameObjectList = new List<GameObject>();

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
		blueRespawnPointGameObjectList.Add((GameObject)Instantiate(spawnPointGameObjectStock, new Vector2(0f, -1f), Quaternion.identity));
		blueRespawnPointGameObjectList[0].GetComponent<SpawnPoint>().team = Actuator.Team.BLUE;

		redRespawnPointGameObjectList.Add((GameObject)Instantiate(spawnPointGameObjectStock, new Vector2(0f, 1f), Quaternion.Euler(0, 0, 180)));
		redRespawnPointGameObjectList[0].GetComponent<SpawnPoint>().team = Actuator.Team.RED;
        //GameObject.Find

        GameObject blueHero = (GameObject)Instantiate(pentagonAgentGameObjectStock, blueRespawnPointGameObjectList[0].transform.position, blueRespawnPointGameObjectList[0].transform.rotation);
		blueHero.AddComponent<PolygonHero>();
		blueHero.GetComponent<PolygonHero>().team = Actuator.Team.BLUE;
		blueHero.GetComponent<PolygonHero>().sides = 5;

		blueHero.AddComponent<Player>();
		MainCamera.mainCamera.playerTransform = blueHero.transform;

		GameObject redHero = (GameObject)Instantiate(pentagonAgentGameObjectStock, redRespawnPointGameObjectList[0].transform.position, redRespawnPointGameObjectList[0].transform.rotation);
		redHero.AddComponent<PolygonHero>();
		redHero.GetComponent<PolygonHero>().team = Actuator.Team.RED;
		redHero.GetComponent<PolygonHero>().sides = 5;
        redRespawnPointGameObjectList[0].GetComponent<SpawnPoint>().hero = redHero;
        blueRespawnPointGameObjectList[0].GetComponent<SpawnPoint>().hero = blueHero;
    }

    // Update is called once per frame
    void Update () {

	}

	public GameObject getBlueSpawnPointGameObject () {
		return blueRespawnPointGameObjectList[0];
	}
	public GameObject getRedSpawnPointGameObject () {
		return redRespawnPointGameObjectList[0];
	}
}
