using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolygonAgent : PolygonEntity {

	protected float fadeTime;
	protected float fadeTimeConstant;

	public List<Ability> abilityList;

	// Use this for initialization
	protected override void Start() {
		base.Start();

		if (team == Team.BLUE) {
			gameObject.layer = LayersManager.layersManager.blueAgentLayer;
		}
		else if (team == Team.RED) {
			gameObject.layer = LayersManager.layersManager.redAgentLayer;
		}

		maxHealth = area * 300f;
		health = maxHealth;
		mechanicalArmor = 20f;

		fadeTime = 6f;
		fadeTimeConstant = 0.25f / fadeTime;

		abilityList = new List<Ability>();
		abilityList.Add(new Shoot());
	}

	protected override IEnumerator Fade() {
		for (float f = 0.25f; f > 0; f -= Time.deltaTime * fadeTimeConstant) {
			spriteRenderer.color = new Color(r, g, b, f);
			//yield return new WaitForSeconds(1f);//3f? //is this consistent? 
			yield return null;
		}
		EliminateSelf();
	}
}
