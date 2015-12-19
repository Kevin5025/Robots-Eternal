using UnityEngine;
using System.Collections;

public class StockReferences : MonoBehaviour {

	public static StockReferences stockReferences;
	public GameObject circleSmall;
	public GameObject circleMedium;
	public GameObject circleLarge;
	public GameObject circleSmall2;//mass = 0.037845 in inspector
	public GameObject circleMedium2;
	public GameObject circleLarge2;

	protected virtual void Awake () {
		if (stockReferences == null) {//like a singleton
			//DontDestroyOnLoad (gameObject);
			stockReferences = this;
		} else { //if (menuColors != null)
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	protected virtual void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

}
