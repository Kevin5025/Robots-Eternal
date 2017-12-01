using UnityEngine;

/**
 * This is anything that is functional enough to have a team affiliation. 
 */
public abstract class Actuator : MonoBehaviour {

    public enum Team { RED, GREEN, BLUE, YELLOW, MAGENTA, CYAN, BROWN, NONE };//TODO: GREY for zombies
    public Team team;

    protected SpriteRenderer spriteRenderer;
    protected float r; protected float g; protected float b;

    protected virtual void Awake() {

    }

	/**
     * Assign sprite color based on team. 
     */
    protected virtual void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = GetTeamColor(spriteRenderer.color, team);
        r = spriteRenderer.color.r; g = spriteRenderer.color.g; b = spriteRenderer.color.b;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	    
	}

    protected virtual void FixedUpdate() {

    }

    public Color GetTeamColor (Color color, Team team) {
        if (team == Team.RED) {
            return new Color(color.r, 0, 0);
        } else if (team == Team.GREEN) {
            return new Color(0, color.g, 0);
        } else if (team == Team.BLUE) {
            return new Color(0, 0, color.b);
        } else if (team == Team.YELLOW) {
            return new Color(color.r, color.g, 0);
        } else if (team == Team.MAGENTA) {
            return new Color(color.r, 0, color.b);
        } else if (team == Team.CYAN) {
            return new Color(0, color.g, color.b);
        } else if (team == Team.BROWN) {//saddle brown = (139,69,19)/256
            return new Color(139f/255f * color.r, 69f/255f * color.g, 19f/255f * color.b);
        } else {
            return color;
        }
    }

    public Team GetOpponentTeam (Team team) {
        if (team == Team.BLUE) {
            return Team.RED;
        } else if (team == Team.RED) {
            return Team.BLUE;
        } else {
            return Team.NONE;
        }
    }
}
