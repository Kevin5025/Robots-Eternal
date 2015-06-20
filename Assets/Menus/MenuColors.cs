using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuColors : MonoBehaviour {

	public static MenuColors menuColors;//accessible from anywhere without doing Gameobject.Find()

	static float normalColorPrimary = 0.875f;
	static float normalColorSecondary = 0.21875f;
	static float highlightedColorPrimary = 1f;
	static float highlightedColorSecondary = 0.25f;
	static float pressedColorPrimary = 0.75f;
	static float pressedColorSecondary = 0.1875f;
	static float disabledColorPrimary = 0.5f;
	static float disabledColorSecondary = 0.375f;

	public static ColorBlock whiteColor = ColorBlock.defaultColorBlock;
	public static ColorBlock redColor = ColorBlock.defaultColorBlock;
	public static ColorBlock yellowColor = ColorBlock.defaultColorBlock;
	public static ColorBlock greenColor = ColorBlock.defaultColorBlock;
	public static ColorBlock cyanColor = ColorBlock.defaultColorBlock;
	public static ColorBlock blueColor = ColorBlock.defaultColorBlock;
	public static ColorBlock magentaColor = ColorBlock.defaultColorBlock;

	void Awake () {
		if (menuColors == null) {//like a singleton
			DontDestroyOnLoad (gameObject);
			menuColors = this;
		} else { //if (menuColors != null)
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		whiteColor.normalColor = new Color (normalColorPrimary, normalColorPrimary, normalColorPrimary);
		whiteColor.highlightedColor = new Color (highlightedColorPrimary, highlightedColorPrimary, highlightedColorPrimary);
		whiteColor.pressedColor = new Color (pressedColorPrimary, pressedColorPrimary, pressedColorPrimary); 
		whiteColor.disabledColor = new Color (disabledColorPrimary, disabledColorPrimary, disabledColorPrimary);
		
		redColor.normalColor = new Color (normalColorPrimary, normalColorSecondary, normalColorSecondary);
		redColor.highlightedColor = new Color (highlightedColorPrimary, highlightedColorSecondary, highlightedColorSecondary);
		redColor.pressedColor = new Color (pressedColorPrimary, pressedColorSecondary, pressedColorSecondary); 
		redColor.disabledColor = new Color (disabledColorPrimary, disabledColorSecondary, disabledColorSecondary);
		
		yellowColor.normalColor = new Color (normalColorPrimary, normalColorPrimary, normalColorSecondary);
		yellowColor.highlightedColor = new Color (highlightedColorPrimary, highlightedColorPrimary, highlightedColorSecondary);
		yellowColor.pressedColor = new Color (pressedColorPrimary, pressedColorPrimary, pressedColorSecondary); 
		yellowColor.disabledColor = new Color (disabledColorPrimary, disabledColorPrimary, disabledColorSecondary);
		
		greenColor.normalColor = new Color (normalColorSecondary, normalColorPrimary, normalColorSecondary);
		greenColor.highlightedColor = new Color (highlightedColorSecondary, highlightedColorPrimary, highlightedColorSecondary);
		greenColor.pressedColor = new Color (pressedColorSecondary, pressedColorPrimary, pressedColorSecondary); 
		greenColor.disabledColor = new Color (disabledColorSecondary, disabledColorPrimary, disabledColorSecondary);
		
		cyanColor.normalColor = new Color (normalColorSecondary, normalColorPrimary, normalColorPrimary);
		cyanColor.highlightedColor = new Color (highlightedColorSecondary, highlightedColorPrimary, highlightedColorPrimary);
		cyanColor.pressedColor = new Color (pressedColorSecondary, pressedColorPrimary, pressedColorPrimary); 
		cyanColor.disabledColor = new Color (disabledColorSecondary, disabledColorPrimary, disabledColorPrimary);
		
		blueColor.normalColor = new Color (normalColorSecondary, normalColorSecondary, normalColorPrimary);
		blueColor.highlightedColor = new Color (highlightedColorSecondary, highlightedColorSecondary, highlightedColorPrimary);
		blueColor.pressedColor = new Color (pressedColorSecondary, pressedColorSecondary, pressedColorPrimary); 
		blueColor.disabledColor = new Color (disabledColorSecondary, disabledColorSecondary, disabledColorPrimary);
		
		magentaColor.normalColor = new Color (normalColorPrimary, normalColorSecondary, normalColorPrimary);
		magentaColor.highlightedColor = new Color (highlightedColorPrimary, highlightedColorSecondary, highlightedColorPrimary);
		magentaColor.pressedColor = new Color (pressedColorPrimary, pressedColorSecondary, pressedColorPrimary); 
		magentaColor.disabledColor = new Color (disabledColorPrimary, disabledColorSecondary, disabledColorPrimary);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
