using UnityEngine;
using System.Collections;

public class ResourceBar : MonoBehaviour {
	
	public Transform healthBarContainerTransform;
	public Transform targetTransform;
    private float width;
	public Entity targetEntity;

	// Use this for initialization
	void Start () {
        width = targetEntity.maxHealth / 115;
		healthBarContainerTransform = transform.parent;//or I could just assign in Editor
		healthBarContainerTransform.SetParent(HUDManager.hUDManager.hUDCanvas.transform);
		healthBarContainerTransform.localScale = new Vector3 (width, 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		//healthBarContainerTransform.position = Camera.main.WorldToScreenPoint (new Vector2 (targetTransform.position.x, targetTransform.position.y + 0.6f));
		Vector3 forwardPosition = Camera.main.transform.TransformPoint (new Vector3 (0, 0.6f));
		//Debug.Log (forwardPosition);//adds the camera's position, see Shoot class
		healthBarContainerTransform.position = Camera.main.WorldToScreenPoint (targetTransform.position + forwardPosition - Camera.main.transform.position);
		//resourceBarContainerTransform.rotation = targetTransform.rotation;//as if the Entity is still the parent
		transform.localScale = new Vector3 (targetEntity.health / targetEntity.maxHealth, 1f, 1f);
	}
}
