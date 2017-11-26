using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBGM : MonoBehaviour {
	public AudioClip newMusic;

	public void ChangeMusic()
	{
		SoundManager.instance.musicSource.clip = newMusic;
		SoundManager.instance.musicSource.Play ();
	}
}
