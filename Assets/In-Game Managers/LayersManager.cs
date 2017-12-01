using UnityEngine;
using System.Collections;

/**
 * Manages collisions. 
 */
public class LayersManager : MonoBehaviour {

	public static LayersManager layersManager;

	public int blueAgentLayer;
	public int blueProjectileLayer;
	public int redAgentLayer;
	public int redProjectileLayer;
    public int brownAgentLayer;
    public int brownProjectileLayer;

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

	/**
     * No friendly fire
     */
	void Start () {
		blueAgentLayer = 8;
		blueProjectileLayer = 9;
		redAgentLayer = 10;
		redProjectileLayer = 11;
        brownAgentLayer = 12;
        brownProjectileLayer = 13;

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

        //brown
        Physics2D.IgnoreLayerCollision(brownAgentLayer, brownAgentLayer, true);
        Physics2D.IgnoreLayerCollision(brownAgentLayer, brownProjectileLayer, true);
        Physics2D.IgnoreLayerCollision(brownProjectileLayer, brownProjectileLayer, true);

        //wall
        Physics2D.IgnoreLayerCollision (wallAgentLayer, blueAgentLayer, true);
        Physics2D.IgnoreLayerCollision (wallAgentLayer, redAgentLayer, true);
        Physics2D.IgnoreLayerCollision (wallProjectileLayer, blueProjectileLayer, true);
        Physics2D.IgnoreLayerCollision (wallProjectileLayer, redProjectileLayer, true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int getTeamAgentLayer(Actuator.Team team) {
        if (team == Actuator.Team.BLUE) {
            return blueAgentLayer;
        } else if (team == Actuator.Team.RED) {
            return redAgentLayer;
        } else if (team == Actuator.Team.BROWN) {
            return brownAgentLayer;
        } else {
            return -1;
        }
    }

    public int getTeamProjectileLayer(Actuator.Team team) {
        if (team == Actuator.Team.BLUE) {
            return blueProjectileLayer;
        } else if (team == Actuator.Team.RED) {
            return redProjectileLayer;
        } else if (team == Actuator.Team.BROWN) {
            return brownProjectileLayer;
        } else {
            return -1;
        }
    }
}
