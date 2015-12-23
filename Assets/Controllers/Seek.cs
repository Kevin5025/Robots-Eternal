using UnityEngine;
using System.Collections;

public class Seek : MonoBehaviour {
    public GameObject target;
    public float distance = 0;
	
    // Update is called once per frame
	void Update () {
        var dir = target.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
