using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuColors : MonoBehaviour {

	static float normalColorPrimary = 0.875f;
	static float normalColorSecondary = 0.21875f;
	static float highlightedColorPrimary = 1f;
	static float highlightedColorSecondary = 0.25f;
	static float pressedColorPrimary = 0.75f;
	static float pressedColorSecondary = 0.1875f;
	static float disabledColorPrimary = 0.5f;
	static float disabledColorSecondary = 0.375f;

	public static ColorBlock buttonWhite = ColorBlock.defaultColorBlock;
	public static ColorBlock buttonRed = ColorBlock.defaultColorBlock;
	public static ColorBlock buttonYellow = ColorBlock.defaultColorBlock;
	public static ColorBlock buttonGreen = ColorBlock.defaultColorBlock;
	public static ColorBlock buttonCyan = ColorBlock.defaultColorBlock;
	public static ColorBlock buttonBlue = ColorBlock.defaultColorBlock;
	public static ColorBlock buttonMagenta = ColorBlock.defaultColorBlock;

	void Awake () {
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () {
		buttonWhite.normalColor = new Color (normalColorPrimary, normalColorPrimary, normalColorPrimary);
		buttonWhite.highlightedColor = new Color (highlightedColorPrimary, highlightedColorPrimary, highlightedColorPrimary);
		buttonWhite.pressedColor = new Color (pressedColorPrimary, pressedColorPrimary, pressedColorPrimary); 
		buttonWhite.disabledColor = new Color (disabledColorPrimary, disabledColorPrimary, disabledColorPrimary);
		
		buttonRed.normalColor = new Color (normalColorPrimary, normalColorSecondary, normalColorSecondary);
		buttonRed.highlightedColor = new Color (highlightedColorPrimary, highlightedColorSecondary, highlightedColorSecondary);
		buttonRed.pressedColor = new Color (pressedColorPrimary, pressedColorSecondary, pressedColorSecondary); 
		buttonRed.disabledColor = new Color (disabledColorPrimary, disabledColorSecondary, disabledColorSecondary);
		
		buttonYellow.normalColor = new Color (normalColorPrimary, normalColorPrimary, normalColorSecondary);
		buttonYellow.highlightedColor = new Color (highlightedColorPrimary, highlightedColorPrimary, highlightedColorSecondary);
		buttonYellow.pressedColor = new Color (pressedColorPrimary, pressedColorPrimary, pressedColorSecondary); 
		buttonYellow.disabledColor = new Color (disabledColorPrimary, disabledColorPrimary, disabledColorSecondary);
		
		buttonGreen.normalColor = new Color (normalColorSecondary, normalColorPrimary, normalColorSecondary);
		buttonGreen.highlightedColor = new Color (highlightedColorSecondary, highlightedColorPrimary, highlightedColorSecondary);
		buttonGreen.pressedColor = new Color (pressedColorSecondary, pressedColorPrimary, pressedColorSecondary); 
		buttonGreen.disabledColor = new Color (disabledColorSecondary, disabledColorPrimary, disabledColorSecondary);
		
		buttonCyan.normalColor = new Color (normalColorSecondary, normalColorPrimary, normalColorPrimary);
		buttonCyan.highlightedColor = new Color (highlightedColorSecondary, highlightedColorPrimary, highlightedColorPrimary);
		buttonCyan.pressedColor = new Color (pressedColorSecondary, pressedColorPrimary, pressedColorPrimary); 
		buttonCyan.disabledColor = new Color (disabledColorSecondary, disabledColorPrimary, disabledColorPrimary);
		
		buttonBlue.normalColor = new Color (normalColorSecondary, normalColorSecondary, normalColorPrimary);
		buttonBlue.highlightedColor = new Color (highlightedColorSecondary, highlightedColorSecondary, highlightedColorPrimary);
		buttonBlue.pressedColor = new Color (pressedColorSecondary, pressedColorSecondary, pressedColorPrimary); 
		buttonBlue.disabledColor = new Color (disabledColorSecondary, disabledColorSecondary, disabledColorPrimary);
		
		buttonMagenta.normalColor = new Color (normalColorPrimary, normalColorSecondary, normalColorPrimary);
		buttonMagenta.highlightedColor = new Color (highlightedColorPrimary, highlightedColorSecondary, highlightedColorPrimary);
		buttonMagenta.pressedColor = new Color (pressedColorPrimary, pressedColorSecondary, pressedColorPrimary); 
		buttonMagenta.disabledColor = new Color (disabledColorPrimary, disabledColorSecondary, disabledColorPrimary);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
