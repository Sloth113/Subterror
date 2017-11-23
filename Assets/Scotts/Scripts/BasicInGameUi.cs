using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicInGameUi : MonoBehaviour {
    private GameObject m_playerObj;
    private CharacterControllerTest m_player;
    //UI
    public Image m_healthBar;
    public Image m_rangeCooldown;
    public Image m_meleeCooldown;
    public Image m_blockCooldown;
    public Text m_scrapCount;
    public Text m_mutagenCount;

	// Use this for initialization
	void Start () {        
	}
	
	// Update is called once per frame
	void Update () {
        if (m_player != null) {
            //HP
            m_healthBar.fillAmount = m_player.m_hp / m_player.maxHp;
            //Cooldowns make function that return a %
            m_scrapCount.text = m_player.m_scrap.ToString();
            m_mutagenCount.text = m_player.m_mutagen.ToString();

        }
	}

    public void SetPlayer(GameObject player) {
        m_playerObj = player;
        m_player = m_playerObj.GetComponent<CharacterControllerTest>(); //player script 
    }
}
