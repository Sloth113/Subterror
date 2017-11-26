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
		SoundManager.instance.PlaySfx (sound.clip);
    }
    
}
