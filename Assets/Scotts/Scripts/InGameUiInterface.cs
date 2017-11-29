using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUiInterface : MonoBehaviour {
    public Image m_healthBar;
    public Image m_healthTotal;
    public Image m_rangeBar;
    public Image m_blockBar;
    public Image m_healBar;
    public Text m_scrapCount;
    public Text m_mutagenCount;
    
    public GameObject m_explosiveIcon;
    public GameObject m_corrosiveIcon;
    public GameObject m_shotgunIcon;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PlayerController playerScript = GameManager.Instance.Player.GetComponent<PlayerController>();

        m_healthBar.fillAmount = (float)playerScript.m_hp / playerScript.m_maxTotalHp;
        m_healthTotal.fillAmount = (float)(playerScript.m_maxTotalHp - playerScript.m_maxHp) / playerScript.m_maxTotalHp;
        //Cooldowns make function that return a %
        m_scrapCount.text = GameManager.Instance.ScrapAmount().ToString("00");
        m_mutagenCount.text = GameManager.Instance.MutaGenAmount().ToString("00");
        //
        m_rangeBar.fillAmount = (float)playerScript.m_rangeTimer/ playerScript.m_rangeCooldown;
        m_blockBar.fillAmount = (float)playerScript.m_blockTimer / playerScript.m_blockCooldown;
        m_healBar.fillAmount = (float)playerScript.m_healTimer / playerScript.m_healCooldown;


        switch (playerScript.m_bulletPrefabs[playerScript.m_bulletIndex].transform.name) {
            case "ExplosiveBullet":
                {
                  //  m_explosiveIcon.SetActive(true);
                    m_corrosiveIcon.SetActive(false);
                    m_shotgunIcon.SetActive(false);
                }
            break;
            case "CorrosiveBullet":
                {
                    //  m_explosiveIcon.SetActive(false);
                    m_corrosiveIcon.SetActive(true);
                    m_shotgunIcon.SetActive(false);
                }
                break;
            case "ShotgunShot":
                {
                   // m_explosiveIcon.SetActive(false);
                    m_corrosiveIcon.SetActive(false);
                    m_shotgunIcon.SetActive(true);
                }
                break;
            default:
                {
                    //  m_explosiveIcon.SetActive(false);
                    m_corrosiveIcon.SetActive(false);
                    m_shotgunIcon.SetActive(false);
                }
                break;
        }
        if(playerScript.m_bulletPrefabs[playerScript.m_bulletIndex].transform.name == "ExplosiveBullet") {

        }
    }
}
