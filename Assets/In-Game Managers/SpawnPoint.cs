using UnityEngine;
using System.Collections;

public class SpawnPoint : Actuator {
    public float minionWaveInterval = 20;
    public GameObject hero;

    Vector3 positionLeft;
    Vector3 positionForward;
    Vector3 positionRight;

	// Use this for initialization
	protected override void Start () {
        base.Start();

        positionLeft = transform.rotation * new Vector3(-0.2f, -0.1f, 0f);
        positionForward = transform.rotation * new Vector3(0f, 0.24f, 0f);
        positionRight = transform.rotation * new Vector3(0.2f, -0.1f, 0f);

        StartCoroutine(spawnMinionWaves());
	}

    IEnumerator spawnMinionWaves() {
        while (true) {
            GameObject minionLeft = (GameObject) Instantiate(SpawnManager.spawnManager.triangleAgentGameObjectStock, transform.position + positionLeft, transform.rotation);
            GameObject minionForward = (GameObject) Instantiate(SpawnManager.spawnManager.triangleAgentGameObjectStock, transform.position + positionForward, transform.rotation);
            GameObject minionRight = (GameObject) Instantiate(SpawnManager.spawnManager.triangleAgentGameObjectStock, transform.position + positionRight, transform.rotation);

			minionLeft.AddComponent<PolygonAgent>();
			minionForward.AddComponent<PolygonAgent>();
			minionRight.AddComponent<PolygonAgent>();

			minionLeft.GetComponent<PolygonAgent>().team = GetComponent<SpawnPoint>().team;
			minionLeft.GetComponent<PolygonAgent>().sides = 3;
			minionForward.GetComponent<PolygonAgent>().team = GetComponent<SpawnPoint>().team;
			minionForward.GetComponent<PolygonAgent>().sides = 3;
			minionRight.GetComponent<PolygonAgent>().team = GetComponent<SpawnPoint>().team;
			minionRight.GetComponent<PolygonAgent>().sides = 3;
            minionLeft.GetComponent< UnitySteer2D.Behaviors.SteerForFollow>().Target = hero.transform;
            minionForward.GetComponent<UnitySteer2D.Behaviors.SteerForFollow>().Target = hero.transform;
            minionRight.GetComponent<UnitySteer2D.Behaviors.SteerForFollow>().Target = hero.transform;

            yield return new WaitForSeconds(minionWaveInterval);
        }
    }

	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}
}
