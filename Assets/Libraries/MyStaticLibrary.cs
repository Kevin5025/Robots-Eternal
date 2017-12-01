using UnityEngine;
using System.Collections;

/**
 * Basic math operations. 
 */
public static class MyStaticLibrary {

    public static System.Random random = new System.Random();

	public static float GetDistance(GameObject a, GameObject b) {
		Vector3 directionToB = b.transform.position - a.transform.position;
		return directionToB.magnitude;
	}

    public static float GetDistance(Vector2 a, Vector2 b) {
        Vector3 directionToB = b - a;
        return directionToB.magnitude;
    }

}
