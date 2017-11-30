using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
//Loads the title screen, used to display intro scene. Attach to object with video player
public class LoadTitleOnFinish : MonoBehaviour {
    VideoPlayer m_intro;
	// Use this for initialization
	void Start () {
        m_intro = GetComponent<VideoPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (m_intro.frame == (long) m_intro.frameCount) {
            //Video has finshed playing..
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
