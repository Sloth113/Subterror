﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
public class GameManager : MonoBehaviour {
    //Game states 
    private enum State {
        Title,
        InGame,
        Pause,
        Settings,
        Upgrades,
        GameOver
    }
    //Singleton approach
    private static GameManager m_instance = null;
    public static GameManager Instance
    {
        get
        {
            if(m_instance == null) {
                GameObject gm = new GameObject();
                gm.AddComponent<GameManager>();
                m_instance = gm.GetComponent<GameManager>();
            }
            return m_instance;
        }
    }

    private GameObject m_player;
    private List<iUpgrade> m_playersUpgrades = new List<iUpgrade>();
    private Inventory m_playersInventory;
    private float m_timer;
    private Stack<State> m_state;
    private string m_level = "Title";

    [Header("UI Elements")]
    [Tooltip("UI prefabs")]
    public GameObject m_titleMenuUI;
    public GameObject m_loadingUI;
    public GameObject m_pauseMenuUI;
    public GameObject m_inGameUI;
    public GameObject m_settingsUI;
    public GameObject m_mutagenMenuUI;
    public GameObject m_scrapMenuUI;
    public GameObject m_winUI;
    public GameObject m_deadUI;

    //Upgrades
    private Dictionary<string, iUpgrade> m_upgradeDictionary;

    //Set up instances and initiate state
    void Awake() {
        if (GameManager.m_instance == null) {
            GameManager.m_instance = this;
            m_instance.m_state = new Stack<State>();
            m_playersInventory = new Inventory();
            m_playersInventory.keys = new List<Key>();
            m_playersUpgrades = new List<iUpgrade>();
            m_instance.m_state.Push(State.Title);
            DontDestroyOnLoad(this.gameObject);
        } else if(GameManager.m_instance != this) {
            Destroy(this.gameObject);
        }           
    }
    
    // Put things into dont destroy 
    void Start () {
        m_titleMenuUI.SetActive(true);
        //Set menus not to destroy
        DontDestroyOnLoad(m_titleMenuUI);
        DontDestroyOnLoad(m_loadingUI);
        DontDestroyOnLoad(m_pauseMenuUI);
        DontDestroyOnLoad(m_inGameUI);
        DontDestroyOnLoad(m_settingsUI);
        DontDestroyOnLoad(m_mutagenMenuUI);
        DontDestroyOnLoad(m_scrapMenuUI);
        DontDestroyOnLoad(m_winUI);
        DontDestroyOnLoad(m_deadUI);
        //Load available upgrades
        m_upgradeDictionary = new Dictionary<string, iUpgrade>();
        m_upgradeDictionary.Add("HP1", new IncreaseHealthUpgrade());
    }
	
	// Check for pause and select button press
	void Update () {
        if (Input.GetButtonDown("Cancel") && m_state.Peek() == State.InGame) {
            m_state.Push(State.Pause);
            m_inGameUI.SetActive(false);
            m_pauseMenuUI.SetActive(true);
            Pause();
         }

        else if (Input.GetKeyDown(KeyCode.Q) && m_state.Peek() == State.InGame) {
            m_state.Push(State.Upgrades);
            m_inGameUI.SetActive(false);
            m_mutagenMenuUI.SetActive(true);
            Pause();
        }

        else if (Input.GetButtonDown("Cancel") && (m_state.Peek() == State.Upgrades || m_state.Peek() == State.Settings || m_state.Peek() == State.Pause)) {
            m_state.Pop(); //Out of upgrades/options to inGame
            if (m_state.Peek() == State.Title) {
                m_titleMenuUI.SetActive(true);
                m_settingsUI.SetActive(false);
            }else if (m_state.Peek() == State.Pause) {
                m_pauseMenuUI.SetActive(true);
                m_settingsUI.SetActive(false);
            } else {
                m_inGameUI.SetActive(true);
                //set all possibilities to false
                m_mutagenMenuUI.SetActive(false);
                m_scrapMenuUI.SetActive(false);
                m_pauseMenuUI.SetActive(false);
                m_settingsUI.SetActive(false);
                //Unpause
                UnPause();
            }
        }
        if(m_state.Peek() == State.InGame) {
            m_timer += Time.deltaTime;
        }
	}

    //Manage adds upgrades to player 
    public void AddUpgrade(iUpgrade upgrade) {
        m_playersUpgrades.Add(upgrade);
        upgrade.Apply(m_player);
    }

    //Load game
    public void StartGame() {
        m_titleMenuUI.SetActive(false);
        m_state.Pop();
        m_state.Push(State.InGame);
        m_level = "level_1-1";
        m_timer = 0;
        
        SceneManager.LoadScene(m_level);
        m_inGameUI.SetActive(true);
    }

    //Load level using string (mostly used in testing)
    public void LoadLevel(string level) {
        m_titleMenuUI.SetActive(false);
        m_state.Pop();
        m_state.Push(State.InGame);
        m_level = level;
        m_timer = 0; //Maybe change

        SceneManager.LoadScene(m_level);
        m_inGameUI.SetActive(true);
    }

    public void LoadSave() {
        m_titleMenuUI.SetActive(false);
        m_state.Pop();
        m_state.Push(State.InGame);
        /*
        m_level =  SAVEDATA.levelName;
        m_timer = SAVEDATA.timer; //Maybe change

        m_playersUpgrades = SAVEDATA.upgrades;
        m_playersInventory = SAVEDATA.inventory;
        */

        SceneManager.LoadScene(m_level);
        m_inGameUI.SetActive(true);
    }

    //Quit
    public void ExitGame() {
        Application.Quit();
    }

    //Settings from title or pause
    public void ToSettings() {
        //Check what ui to disable
        if(m_state.Peek() == State.Pause) {
            m_pauseMenuUI.SetActive(false);
        }else if(m_state.Peek() == State.Title) {
            m_titleMenuUI.SetActive(false);
        }
        //set ui to active and update state
        m_settingsUI.SetActive(true);
        m_state.Push(State.Settings);
        
    }
    public void ExitSettings() {
        //Remove settings state
        m_state.Pop();
        m_settingsUI.SetActive(false);
        if(m_state.Peek() == State.InGame) {
            m_inGameUI.SetActive(true);
        }else if(m_state.Peek() == State.Title) {
            m_titleMenuUI.SetActive(true);
        }
    }

    public void PauseToIngame() {
        m_state.Pop();//Pause
        m_state.Push(State.InGame);
        m_pauseMenuUI.SetActive(false);
        m_inGameUI.SetActive(true);
        UnPause();
    }

    public void PauseToTitle() {
        m_state.Pop();//Pause
        m_state.Pop();//InGame
        m_state.Push(State.Title);
        m_titleMenuUI.SetActive(true);
        m_pauseMenuUI.SetActive(false);
        Time.timeScale = 1;

        SceneManager.LoadScene("TitleScreen");
    }

    private void Pause() {
        //Stop most things 
        Time.timeScale = 0;
        //Disable Player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterControllerTest>().enabled = false;
        //Disable Enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //MIGHT BREAK
        foreach (GameObject enemy in enemies) {
            if (enemy.GetComponent<EnemyV2>() != null) {
                enemy.GetComponent<EnemyV2>().enabled = false;
            }
        }
    }
    private void UnPause() {
        Time.timeScale = 1;
        //Enable Player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterControllerTest>().enabled = true;
        //Enable enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //MIGHT BREAK
        foreach (GameObject enemy in enemies) {
            if (enemy.GetComponent<EnemyV2>() != null) {
                enemy.GetComponent<EnemyV2>().enabled = true;
            }
        }
    }

    public void InGameToDead() {
        m_state.Pop();
        m_state.Push(State.GameOver);
        m_inGameUI.SetActive(false);
        m_deadUI.SetActive(true);
    }

    public void InGameToWin() {
        m_state.Pop();
        m_state.Push(State.GameOver);
        m_inGameUI.SetActive(false);
        m_winUI.SetActive(true);
    }

    public void OverToMenu() {
        m_state.Pop();//Should pop over
        m_state.Push(State.Title);
        m_deadUI.SetActive(false);
        m_winUI.SetActive(false);
        m_titleMenuUI.SetActive(true);
    }

    public void ScrapMutaMenuToggle() {
        m_scrapMenuUI.SetActive(!m_scrapMenuUI.activeSelf);
        m_mutagenMenuUI.SetActive(!m_mutagenMenuUI.activeSelf);
    }

    public void NextLevel(string name) {
        if(name == "Win") {
            InGameToWin();
        }
        SceneManager.LoadScene(name);
    }

    //Called when the new scene is loaded with a player
    public void NewPlayer(GameObject player) {
        m_player = player;
        foreach(iUpgrade upgrade in m_playersUpgrades) {
            upgrade.Apply(m_player);
        }
        m_inGameUI.GetComponent<BasicInGameUi>().SetPlayer(m_player);//Set player on UI controller
    }
    
    //Players inventory changes
    public void AddInventory(Inventory toAdd) {
        //Add keys
        for(int i = 0; i < toAdd.keys.Count; i++)
            m_playersInventory.keys.Add(toAdd.keys[i]);
        //Muta
        m_playersInventory.mutagen += toAdd.mutagen;
        //Scrap 
        m_playersInventory.scrap += toAdd.scrap;
    }

    public void AddKey(Key key) {
        m_playersInventory.keys.Add(key);
    }

    public void ChangeMutaGen(int amount) {
        if ((m_playersInventory.mutagen + amount) >= 0)
            m_playersInventory.mutagen += amount;
    }

    public void ChangeScrap(int amount) {
        if ((m_playersInventory.scrap + amount) >= 0)
            m_playersInventory.scrap += amount;
    }

    public int MutaGenAmount() {
        return m_playersInventory.mutagen;
    }
    public int ScrapAmount() {
        return m_playersInventory.scrap;
    }
    public List<Key> PlayerKeys() {
        return m_playersInventory.keys;
    }


}
