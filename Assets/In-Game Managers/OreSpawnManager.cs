using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Respawn manager for ores
 * TODO one per scene
 */
public class OreSpawnManager : MonoBehaviour {

    public float stoneOreWaveInterval;
    public float gemstoneOreWaveInterval;

    List<Vector2> stoneOrePositionList;
    List<Vector2> diamondOrePositionList;
    List<Vector2> rubyOrePositionList;
    List<Vector2> emeraldOrePositionList;
    List<Vector2> sapphireOrePositionList;

    public GameObject stoneOrePrefab;//Set in Unity
    public GameObject diamondOrePrefab;//Set in Unity
    public GameObject rubyOrePrefab;//Set in Unity
    public GameObject emeraldOrePrefab;//Set in Unity
    public GameObject sapphireOrePrefab;//Set in Unity

    protected virtual void Start () {
        //base.Start();
        stoneOreWaveInterval = 30f;
        gemstoneOreWaveInterval = 60f;

        stoneOrePositionList = new List<Vector2>();
        diamondOrePositionList = new List<Vector2>();
        rubyOrePositionList = new List<Vector2>();
        emeraldOrePositionList = new List<Vector2>();
        sapphireOrePositionList = new List<Vector2>();

        stoneOrePositionList.Add(new Vector2(-22f, -22f));
        stoneOrePositionList.Add(new Vector2(22f, 22f));
        diamondOrePositionList.Add(new Vector2(0, 0));
        sapphireOrePositionList.Add(new Vector2(-5f, -30f));
        sapphireOrePositionList.Add(new Vector2(5f, 30f));
        emeraldOrePositionList.Add(new Vector2(-30f, 5f));
        emeraldOrePositionList.Add(new Vector2(30f, -5f));
        rubyOrePositionList.Add(new Vector2(-25f, 25f));
        rubyOrePositionList.Add(new Vector2(25f, -25f));

        StartCoroutine(spawnGemStoneOreWaves());
        StartCoroutine(spawnStoneOreWaves());
    }

    /**
     * Instantiates gemstones
     */
    IEnumerator spawnGemStoneOreWaves () {
        while (true) {
            for (int d = 0; d < diamondOrePositionList.Count; d++) {
                Instantiate(diamondOrePrefab, diamondOrePositionList[d], Quaternion.identity);
            }
            for (int r = 0; r < rubyOrePositionList.Count; r++) {
                Instantiate(rubyOrePrefab, rubyOrePositionList[r], Quaternion.identity);
            }
            for (int e = 0; e < emeraldOrePositionList.Count; e++) {
                Instantiate(emeraldOrePrefab, emeraldOrePositionList[e], Quaternion.identity);
            }
            for (int s = 0; s < sapphireOrePositionList.Count; s++) {
                Instantiate(sapphireOrePrefab, sapphireOrePositionList[s], Quaternion.identity);
            }
            yield return new WaitForSeconds(gemstoneOreWaveInterval);
        }
    }

    /**
     * Instantiates gemstones
     */
    IEnumerator spawnStoneOreWaves () {
        while (true) {
            for (int s=0; s<stoneOrePositionList.Count; s++) {
                Instantiate(stoneOrePrefab, stoneOrePositionList[s], Quaternion.identity);
            }
            yield return new WaitForSeconds(stoneOreWaveInterval);
        }
    }
}
