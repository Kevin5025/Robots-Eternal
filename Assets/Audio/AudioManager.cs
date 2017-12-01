using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager audioManager;

    public AudioSource silencerAudioSource;//set in Unity
    public AudioSource shotgunAudioSource;//set in Unity
    public AudioSource pickaxeAudioSource;//set in Unity

    void Awake () {
        if (audioManager == null) {
            audioManager = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
