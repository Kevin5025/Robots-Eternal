using UnityEngine;
using System.Collections;

public abstract class PolygonEntity : Entity {

	public const float sidelength = 0.41f;

	public int sides;//assign in inspector or in instantiation script
	public float inradius;
	public float circumradius;
	public float area;

	public float mass;
	public float force;
	public float torque;

	// Use this for initialization
	protected override void Start () {
		base.Start();

		inradius = InRadius(sides, sidelength);
		circumradius = CircumRadius(sides, sidelength);
		area = Area(sides, sidelength);

		GetComponent<Rigidbody2D>().mass = area;
		force = area * 25f;
		torque = area * 2.5f;//* 25f when there's no collider
	}

	public static float InRadius (int sides, float sidelength) {//Apothem
		return 0.5f * sidelength / Mathf.Tan(Mathf.PI / sides);//0.68819f * 0.41f if sides = 5
	}
	public static float CircumRadius (int sides, float sidelength) {//Radius
		return 0.5f * sidelength / Mathf.Sin(Mathf.PI / sides);//0.85065f * 0.41f if sides = 5
	}
	public static float Area (int sides, float sidelength) {//can also be in terms of the number of sides and the Apothem
		//1/2 * apothem * perimeter
		return 0.25f * sidelength * sidelength * sides / Mathf.Tan(Mathf.PI / sides);//1.705f if sides = 5
	}
}
