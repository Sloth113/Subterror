using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnDestroy : MonoBehaviour {
	GameObject soundmanager;
	AudioSource sound;

	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource>();
	}

    public void CreateTempSoundObj() {
        soundmanager = new GameObject();
        AudioSource audioSource = soundmanager.AddComponent<AudioSource>() as AudioSource;
        audioSource.clip = sound.clip;
        audioSource.Play();
        Destroy(soundmanager, 5.0f);
    }
    
}
