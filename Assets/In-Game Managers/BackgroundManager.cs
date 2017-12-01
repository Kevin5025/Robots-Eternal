using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/**
 * Manages the background / map ground sprite. 
 */
public class BackgroundManager : MonoBehaviour {

	public static BackgroundManager backgroundManager;

	protected GameObject background;

	void Awake () {
		if (backgroundManager == null) {
			//DontDestroyOnLoad (gameObject);
			backgroundManager = this;
		} else {
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		background = (GameObject)Instantiate(PrefabReferences.prefabReferences.block, Vector3.zero, Quaternion.identity);
        background.name = "background";
		SpriteRenderer spriteRenderer = background.GetComponent<SpriteRenderer>();
		spriteRenderer.sortingLayerName = "Background";
		spriteRenderer.color = new Color(193f / 255, 154f / 255, 107f / 255);//https://en.wikipedia.org/wiki/Desert_sand_(color)

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level 0") {
            background.transform.localScale = new Vector3(50f, 50f, 1f);
        } else if (scene.name == "Level 1") {
            background.transform.localScale = new Vector3(80f, 80f, 1f);
        }
	}

	// Update is called once per frame
	void Update () {

	}
}
