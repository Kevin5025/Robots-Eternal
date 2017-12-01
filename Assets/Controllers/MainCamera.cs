using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

    public static MainCamera mainCamera;
    public Transform playerTransform;

    void Awake () {
        if (mainCamera == null) {
            //DontDestroyOnLoad (gameObject);
            mainCamera = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        //playerTransform = PlayerController.player.agent.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update () {
        if (playerTransform) {
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, -10f);

            //if (Player.player.cameraScheme == 0) {
            transform.rotation = Quaternion.identity;
            //} else if (Player.player.cameraScheme == 1) {
            //	transform.rotation = playerTransform.rotation;
            //}
        }
    }
}
