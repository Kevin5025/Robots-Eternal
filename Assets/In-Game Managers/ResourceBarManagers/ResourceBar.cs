using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBar : MonoBehaviour {

    public Transform resourceBarContainerTransform;
    protected float resourceBarWidth;//relative to 1.0f
    public Transform targetTransform;

    // Use this for initialization
    protected virtual void Start () {
        resourceBarContainerTransform = transform.parent;//or I could just assign in Editor
        resourceBarContainerTransform.SetParent(ResourceBarManager.resourceBarManager.resourceBarCanvasGameObject.transform);
        resourceBarContainerTransform.localScale = new Vector3(resourceBarWidth, 1f, 1f);
    }
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}
}
