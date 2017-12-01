using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

/**
 * Initializes spawn points and heroes. 
 */
public class SpawnManager : MonoBehaviour {

    public static SpawnManager spawnManager;

    public GameObject spawnPointGameObjectPrefab;//keeps position and rotation
    public GameObject circleAgentGameObjectPrefab;
    public GameObject circleAgentGameObjectPrefab2;

    //TODO: loop over color
    public List<GameObject> blueHeroSpawnPointGameObjectList;
    public List<GameObject> blueMinionSpawnPointGameObjectList;
    public List<GameObject> redHeroSpawnPointGameObjectList;
    public List<GameObject> redMinionSpawnPointGameObjectList;
    public List<GameObject> brownHeroSpawnPointGameObjectList;
    public List<GameObject> brownMinionSpawnPointGameObjectList;

    public GameObject blueTowerGameObject;//Set in Unity
    public GameObject redTowerGameObject;//Set in Unity
    public GameObject blueObjectiveGameObject;//Set in Unity
    public GameObject redObjectiveGameObject;//Set in Unity

    void Awake () {
        if (spawnManager == null) {//like a singleton
                                   //DontDestroyOnLoad (gameObject);
            spawnManager = this;
        } else {
            Destroy(gameObject);
        }
    }

    /**
     * Initializes spawn points and spawns heroes
     */
    void Start () {
        InitializeHeroSpawnPoints();
        InitializeHeroes();
        InitializeMinionSpawnPoints();
    }

    /**
     * Instantiates hero spawn points
     */
    private void InitializeHeroSpawnPoints () {
        Vector2[] blueHeroSpawnPointVector2Array;
        Quaternion[] blueHeroSpawnPointQuaternionArray;
        Vector2[] redHeroSpawnPointVector2Array;
        Quaternion[] redHeroSpawnPointQuaternionArray;
        Vector2[] brownHeroSpawnPointVector2Array;
        Quaternion[] brownHeroSpawnPointQuaternionArray;

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level 0") {
            blueHeroSpawnPointVector2Array = new Vector2[] { new Vector2(-18f, -14f), new Vector2(-18f, -16f), new Vector2(-18f, -18f), new Vector2(-16f, -18f), new Vector2(-14f, -18f) };
            blueHeroSpawnPointQuaternionArray = new Quaternion[] { Quaternion.Euler(0, 0, 270), Quaternion.Euler(0, 0, 270), Quaternion.Euler(0, 0, 315), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0) };
            redHeroSpawnPointVector2Array = new Vector2[] { new Vector2(18f, 14f), new Vector2(18f, 16f), new Vector2(18f, 18f), new Vector2(16f, 18f), new Vector2(14f, 18f) };
            redHeroSpawnPointQuaternionArray = new Quaternion[] { Quaternion.Euler(0, 0, 90), Quaternion.Euler(0, 0, 90), Quaternion.Euler(0, 0, 135), Quaternion.Euler(0, 0, 180), Quaternion.Euler(0, 0, 180) };
            brownHeroSpawnPointVector2Array = new Vector2[] { new Vector2(-40f, 40f), new Vector2(40f, -40f) };
            brownHeroSpawnPointQuaternionArray = new Quaternion[] { Quaternion.Euler(0, 0, 225), Quaternion.Euler(0, 0, 135) };
        } else {//if (scene.name == "Level 1") {
            blueHeroSpawnPointVector2Array = new Vector2[] { new Vector2(-33f, -29f), new Vector2(-33f, -31f), new Vector2(-33f, -33f), new Vector2(-31f, -33f), new Vector2(-29f, -33f) };
            blueHeroSpawnPointQuaternionArray = new Quaternion[] { Quaternion.Euler(0, 0, 270), Quaternion.Euler(0, 0, 270), Quaternion.Euler(0, 0, 315), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0) };
            redHeroSpawnPointVector2Array = new Vector2[] { new Vector2(33f, 29f), new Vector2(33f, 31f), new Vector2(33f, 33f), new Vector2(31f, 33f), new Vector2(29f, 33f) };
            redHeroSpawnPointQuaternionArray = new Quaternion[] { Quaternion.Euler(0, 0, 90), Quaternion.Euler(0, 0, 90), Quaternion.Euler(0, 0, 135), Quaternion.Euler(0, 0, 180), Quaternion.Euler(0, 0, 180) };
            brownHeroSpawnPointVector2Array = new Vector2[] { new Vector2(-70f, 70f), new Vector2(70f, -70f) };
            brownHeroSpawnPointQuaternionArray = new Quaternion[] { Quaternion.Euler(0, 0, 225), Quaternion.Euler(0, 0, 135) };
        }

        blueHeroSpawnPointGameObjectList = new List<GameObject>();
        for (int b = 0; b < blueHeroSpawnPointVector2Array.Length; b++) {
            blueHeroSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, blueHeroSpawnPointVector2Array[b], blueHeroSpawnPointQuaternionArray[b]));
            blueHeroSpawnPointGameObjectList[b].AddComponent<SpawnPoint>();
            blueHeroSpawnPointGameObjectList[b].GetComponent<SpawnPoint>().team = Actuator.Team.BLUE;
        }

        redHeroSpawnPointGameObjectList = new List<GameObject>();
        for (int r = 0; r < redHeroSpawnPointVector2Array.Length; r++) {
            redHeroSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, redHeroSpawnPointVector2Array[r], redHeroSpawnPointQuaternionArray[r]));
            redHeroSpawnPointGameObjectList[r].AddComponent<SpawnPoint>();
            redHeroSpawnPointGameObjectList[r].GetComponent<SpawnPoint>().team = Actuator.Team.RED;
        }

        brownHeroSpawnPointGameObjectList = new List<GameObject>();
        for (int r = 0; r < brownHeroSpawnPointVector2Array.Length; r++) {
            brownHeroSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, brownHeroSpawnPointVector2Array[r], brownHeroSpawnPointQuaternionArray[r]));
            brownHeroSpawnPointGameObjectList[r].AddComponent<SpawnPoint>();
            brownHeroSpawnPointGameObjectList[r].GetComponent<SpawnPoint>().team = Actuator.Team.BROWN;
        }
    }

    /**
     * Instantiates circle heroes
     */
    private void InitializeHeroes () {
        Type[] blueHeroControllerTypeArray = new Type[] { typeof(PlayerController), typeof(HeroController) };
        for (int b=0; b<blueHeroControllerTypeArray.Length; b++) {
            GameObject blueHeroSpawnPointGameObject = GetTeamHeroSpawnPointGameObject(Actuator.Team.BLUE);
            GameObject blueHero = Instantiate(circleAgentGameObjectPrefab, blueHeroSpawnPointGameObject.transform.position, blueHeroSpawnPointGameObject.transform.rotation);
            blueHero.AddComponent<CircleHero>();
            blueHero.GetComponent<CircleHero>().team = Actuator.Team.BLUE;
            blueHero.AddComponent(blueHeroControllerTypeArray[b]);
        }

        Type[] redHeroControllerTypeArray = new Type[] { typeof(HeroController), typeof(HeroController) };
        for (int r=0; r<redHeroControllerTypeArray.Length; r++) {
            GameObject redHeroSpawnPointGameObject = GetTeamHeroSpawnPointGameObject(Actuator.Team.RED);
            GameObject redHero = Instantiate(circleAgentGameObjectPrefab, redHeroSpawnPointGameObject.transform.position, redHeroSpawnPointGameObject.transform.rotation);
            redHero.AddComponent<CircleHero>();
            redHero.GetComponent<CircleHero>().team = Actuator.Team.RED;
            redHero.AddComponent(redHeroControllerTypeArray[r]);
        }

        Type[] brownHeroControllerTypeArray = new Type[] { typeof(PirateController), typeof(PirateController), typeof(VikingController), typeof(VikingController) };
        for (int r = 0; r < brownHeroControllerTypeArray.Length; r++) {
            GameObject brownHeroSpawnPointGameObject = GetTeamHeroSpawnPointGameObject(Actuator.Team.BROWN);
            GameObject brownHero = Instantiate(circleAgentGameObjectPrefab, brownHeroSpawnPointGameObject.transform.position, brownHeroSpawnPointGameObject.transform.rotation);
            brownHero.AddComponent<CircleHero>();
            brownHero.GetComponent<CircleHero>().team = Actuator.Team.BROWN;
            brownHero.AddComponent(brownHeroControllerTypeArray[r]);
        }
    }

    /**
     * Instantiates and initializes minion spawn points
     */
    private void InitializeMinionSpawnPoints () {
        blueMinionSpawnPointGameObjectList = new List<GameObject>();
        redMinionSpawnPointGameObjectList = new List<GameObject>();
        brownMinionSpawnPointGameObjectList = new List<GameObject>();

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level 0") {
            blueMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(-12f, -8f), Quaternion.identity));
            blueMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(-8f, -12f), Quaternion.Euler(0, 0, 270)));
            redMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(12f, 8f), Quaternion.Euler(0, 0, 180)));
            redMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(8f, 12f), Quaternion.Euler(0, 0, 90)));
            brownMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(-40f, 38f), Quaternion.Euler(0, 0, 225)));
            brownMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(40f, -38f), Quaternion.Euler(0, 0, 225)));
            //brownMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(-38f, 40f), Quaternion.Euler(0, 0, 135)));
            //brownMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(38f, -40f), Quaternion.Euler(0, 0, 135)));
        } else {//if (scene.name == "Level 1") {
            blueMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(-27f, -23f), Quaternion.identity));
            blueMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(-23f, -27f), Quaternion.Euler(0, 0, 270)));
            blueMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(-5f, -30f), Quaternion.Euler(0, 0, 315)));
            //blueMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(-30f, 5f), Quaternion.Euler(0, 0, 270)));

            redMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(27f, 23f), Quaternion.Euler(0, 0, 180)));
            redMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(23f, 27f), Quaternion.Euler(0, 0, 90)));
            redMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(5f, 30f), Quaternion.Euler(0, 0, 135)));
            //redMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(30f, -5f), Quaternion.Euler(0, 0, 90)));

            brownMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(-70f, 68f), Quaternion.Euler(0, 0, 225)));
            brownMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(70f, -68f), Quaternion.Euler(0, 0, 225)));
            brownMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(-68f, 70f), Quaternion.Euler(0, 0, 135)));
            brownMinionSpawnPointGameObjectList.Add(Instantiate(spawnPointGameObjectPrefab, new Vector2(68f, -70f), Quaternion.Euler(0, 0, 135)));
        }

        for (int bm=0; bm<blueMinionSpawnPointGameObjectList.Count; bm++) {
            blueMinionSpawnPointGameObjectList[bm].AddComponent<MinionSpawnPoint>();
            blueMinionSpawnPointGameObjectList[bm].GetComponent<MinionSpawnPoint>().team = Actuator.Team.BLUE;
            blueMinionSpawnPointGameObjectList[bm].GetComponent<MinionSpawnPoint>().minionController = typeof(MinionController);
        }

        for (int rm=0; rm<redMinionSpawnPointGameObjectList.Count; rm++) {
            redMinionSpawnPointGameObjectList[rm].AddComponent<MinionSpawnPoint>();
            redMinionSpawnPointGameObjectList[rm].GetComponent<MinionSpawnPoint>().team = Actuator.Team.RED;
            redMinionSpawnPointGameObjectList[rm].GetComponent<MinionSpawnPoint>().minionController = typeof(MinionController);
        }

        for (int bm=0; bm<brownMinionSpawnPointGameObjectList.Count; bm++) {
            brownMinionSpawnPointGameObjectList[bm].AddComponent<MinionSpawnPoint>();
            brownMinionSpawnPointGameObjectList[bm].GetComponent<MinionSpawnPoint>().team = Actuator.Team.BROWN;
            brownMinionSpawnPointGameObjectList[bm].GetComponent<MinionSpawnPoint>().minionController = typeof(PirateController);
        }
    }

    // Update is called once per frame
    void Update () {

	}

    public GameObject GetTeamHeroSpawnPointGameObject (Actuator.Team team) {
        if (team == Actuator.Team.BLUE) {
            return blueHeroSpawnPointGameObjectList[MyStaticLibrary.random.Next(blueHeroSpawnPointGameObjectList.Count)];
        } else if (team == Actuator.Team.RED) {
            return redHeroSpawnPointGameObjectList[MyStaticLibrary.random.Next(redHeroSpawnPointGameObjectList.Count)];
        } else if (team == Actuator.Team.BROWN) {
            return brownHeroSpawnPointGameObjectList[MyStaticLibrary.random.Next(brownHeroSpawnPointGameObjectList.Count)];
        }
        return null;
    }

    public GameObject GetOpponentTowerGameObject (Actuator.Team team) {
        if (team == Actuator.Team.BLUE) {
            return redTowerGameObject;
        } else if (team == Actuator.Team.RED) {
            return blueTowerGameObject;
        } else {
            return null;
        }
    }

    public GameObject GetOpponentObjectiveGameObject (Actuator.Team team) {
        if (team == Actuator.Team.BLUE) {
            return redObjectiveGameObject;
        } else if (team == Actuator.Team.RED) {
            return blueObjectiveGameObject;
        } else {
            return null;
        }
    }
}
