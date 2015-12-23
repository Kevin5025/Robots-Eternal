using UnityEngine;
using System.Collections;

public class ResourceBar : MonoBehaviour {
	
	public Transform healthBarContainerTransform;
	public Transform targetTransform;
    private float healthBarWidth;//relative to 1.0f
	public Entity targetEntity;

	// Use this for initialization
	void Start () {
        healthBarWidth = targetEntity.maxHealth / 115;//TODO: consider LoL style
		healthBarContainerTransform = transform.parent;//or I could just assign in Editor
		healthBarContainerTransform.SetParent(HUDManager.hUDManager.hUDCanvas.transform);
		healthBarContainerTransform.localScale = new Vector3 (healthBarWidth, 1f, 1f);
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
