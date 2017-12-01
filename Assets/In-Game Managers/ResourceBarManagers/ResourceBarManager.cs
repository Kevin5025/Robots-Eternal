using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBarManager : MonoBehaviour {

    public static ResourceBarManager resourceBarManager;

    public GameObject resourceBarCanvasGameObject;
    public GameObject healthBarContainerPrefab;
    public GameObject experienceBarContainerPrefab;

    void Awake () {
        if (resourceBarManager == null) {//like a singleton
            //DontDestroyOnLoad (gameObject);
            resourceBarManager = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        resourceBarCanvasGameObject.GetComponent<Canvas>().sortingOrder = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
