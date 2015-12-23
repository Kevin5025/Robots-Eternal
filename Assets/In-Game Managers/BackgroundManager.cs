using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {

	public static BackgroundManager backgroundManager;

	protected GameObject background;

	void Awake() {
		if (backgroundManager == null) {//like a singleton
			//DontDestroyOnLoad (gameObject);
			backgroundManager = this;
		} else { //if (menuColors != null)
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		background = (GameObject)Instantiate(StockReferences.stockReferences.block, Vector3.zero, Quaternion.identity);
		SpriteRenderer spriteRenderer = background.GetComponent<SpriteRenderer>();
		spriteRenderer.sortingLayerName = "Background";
		spriteRenderer.color = new Color(193f/255, 154f/255, 107f/255);//https://en.wikipedia.org/wiki/Desert_sand_(color)

		background.transform.localScale = new Vector3(40f, 40f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
