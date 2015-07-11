using UnityEngine;
using System.Collections;

public static class MyStaticLibrary {

	public static float sqrt2over2 = 0.70710678118f;

	public static float getDistance(GameObject a, GameObject b){
		Vector3 directionToB = b.transform.position - a.transform.position;
		return directionToB.magnitude;
	}

}
