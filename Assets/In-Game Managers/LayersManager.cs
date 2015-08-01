using UnityEngine;
using System.Collections;

public class LayersManager : MonoBehaviour {

	public static LayersManager layersManager;

	public int blueAgentLayer;
	public int blueProjectileLayer;
	public int redAgentLayer;
	public int redProjectileLayer;

	void Awake () {
		if (layersManager == null) {//like a singleton
			DontDestroyOnLoad (gameObject);
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

		Physics2D.IgnoreLayerCollision (blueAgentLayer, blueAgentLayer, true);
		Physics2D.IgnoreLayerCollision (blueAgentLayer, blueProjectileLayer, true);
		Physics2D.IgnoreLayerCollision (blueProjectileLayer, blueProjectileLayer, true);

		Physics2D.IgnoreLayerCollision (redAgentLayer, redAgentLayer, true);
		Physics2D.IgnoreLayerCollision (redAgentLayer, redProjectileLayer, true);
		Physics2D.IgnoreLayerCollision (redProjectileLayer, redProjectileLayer, true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
