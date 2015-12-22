using UnityEngine;
using System.Collections;

public class LayersManager : MonoBehaviour {

	public static LayersManager layersManager;

	public int blueAgentLayer;
	public int blueProjectileLayer;
	public int redAgentLayer;
	public int redProjectileLayer;

	public int wallVisionLayer;
	public int wallProjectileLayer;
	public int wallAgentLayer;
	public int wallLayer;

	void Awake () {
		if (layersManager == null) {//like a singleton
			//DontDestroyOnLoad (gameObject);
			layersManager = this;
		} else { //if (menuColors != null)
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		blueAgentLayer = 8;
		blueProjectileLayer = 9;
		redAgentLayer = 10;
		redAgentLayer = 11;

		wallVisionLayer = 28;
		wallProjectileLayer = 29;
		wallAgentLayer = 30;
        wallLayer = 31;

        //blue
        Physics2D.IgnoreLayerCollision (blueAgentLayer, blueAgentLayer, true);
        Physics2D.IgnoreLayerCollision (blueAgentLayer, blueProjectileLayer, true);
        Physics2D.IgnoreLayerCollision (blueProjectileLayer, blueProjectileLayer, true);

        //red
        Physics2D.IgnoreLayerCollision (redAgentLayer, redAgentLayer, true);
        Physics2D.IgnoreLayerCollision (redAgentLayer, redProjectileLayer, true);
        Physics2D.IgnoreLayerCollision (redProjectileLayer, redProjectileLayer, true);

        //wall
        Physics2D.IgnoreLayerCollision (wallAgentLayer, blueAgentLayer, true);
        Physics2D.IgnoreLayerCollision (wallAgentLayer, redAgentLayer, true);
        Physics2D.IgnoreLayerCollision (wallProjectileLayer, blueProjectileLayer, true);
        Physics2D.IgnoreLayerCollision (wallProjectileLayer, redProjectileLayer, true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
