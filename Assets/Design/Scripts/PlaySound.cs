using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlaySound : MonoBehaviour {
    AudioSource[] audio;

    // Use this for initialization
    void Start () {
        audio = GetComponents<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Play()
    {
        audio[0].Play();
    }

    public void PlayDetail(AnimationEvent anievent)
    {
        Debug.Log(anievent.intParameter);
        audio[anievent.intParameter].Play();
    }

}
