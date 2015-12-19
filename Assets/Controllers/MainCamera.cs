using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	public static MainCamera mainCamera;
	public Transform playerTransform;

	void Awake () {
		if (mainCamera == null) {//like a singleton
			//DontDestroyOnLoad (gameObject);
			mainCamera = this;
		} else { //if (menuColors != null)
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (playerTransform) {
			transform.position = new Vector3 (playerTransform.position.x, playerTransform.position.y, -10f);

			if (Player.player.cameraScheme == 0) {
				transform.rotation = Quaternion.identity;
			} else if (Player.player.cameraScheme == 1) {
				transform.rotation = playerTransform.rotation;
			}
		}
	}
}
