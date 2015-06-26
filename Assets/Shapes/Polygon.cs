using UnityEngine;
using System.Collections;

public class Polygon : MonoBehaviour {

	public float area;
	public float mass;
	public float force;
	public float torque;

	// Use this for initialization
	void Start () {
		area = 1.705f;
		GetComponent<Rigidbody2D>().mass = area;
		force = area * 15f;
		torque = area * 15f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
