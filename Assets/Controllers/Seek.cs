using UnityEngine;
using System.Collections;

public class Seek : MonoBehaviour {
    public PolygonAgent target;
    public float distance = 0;
	private Rigidbody2D rb;
    private PolygonAgent me;

    public void Start()
    {
        me = GetComponent<PolygonAgent>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate () {
        Vector3 dir = target.transform.position - transform.position;
        float currentRotation = transform.eulerAngles.z;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 270;
        float offsetRotation = Mathf.Abs((angle - currentRotation) % 360);

        if (offsetRotation > 0f && offsetRotation <= 90f)
            rb.AddTorque(me.torque * offsetRotation / 90f);//turn left slowly
        else if (offsetRotation > 270f && offsetRotation < 360f)
            rb.AddTorque(me.torque * (offsetRotation - 360f) / 90f);//turn right slowly
        else if (offsetRotation > 0f && offsetRotation <= 180f)
            rb.AddTorque(me.torque);//turn left max
        else if (offsetRotation > 180f && offsetRotation < 360f)
            rb.AddTorque(-me.torque);//turn right max
	}
}
