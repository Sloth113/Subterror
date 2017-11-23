using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
public class GameManager : MonoBehaviour {
    private enum State {
        Title,
        InGame,
        Pause,
        Options,
        GameOver
    }

    private static GameManager m_instance = null;
    public static GameManager Instance
    {
        get
        {
            if(m_instance == null) {
                m_instance = new GameManager();
            }
            return m_instance;
        }
    }

    private GameObject m_player;
    private List<iUpgrade> m_playersUpgrades = new List<iUpgrade>();
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

    void Awake() {
        if (GameManager.m_instance == null) {
            GameManager.m_instance = this;
            m_instance.m_state = new Stack<State>();
            m_instance.m_state.Push(State.Title);
            DontDestroyOnLoad(this.gameObject);
        } else if(GameManager.m_instance != this) {
            Destroy(this.gameObject);
        }           
    }
    
    // Use this for initialization
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
        //Player
        //DontDestroyOnLoad(m_player);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel") && m_state.Peek() == State.InGame) {
            m_state.Push(State.Pause);
            m_inGameUI.SetActive(false);
            m_pauseMenuUI.SetActive(true);

            Time.timeScale = 0;
            //Disable Player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<CharacterControllerTest>().enabled = false;
         }
        if(m_state.Peek() == State.InGame) {
            m_timer += Time.deltaTime;
        }
	}

    public void AddUpgrade(iUpgrade upgrade) {
        m_playersUpgrades.Add(upgrade);
        upgrade.Apply(m_player);
    }

    public void StartGame() {
        m_titleMenuUI.SetActive(false);
        m_state.Pop();
        m_state.Push(State.InGame);
        m_level = "level_1-1";
        m_timer = 0;
        
        SceneManager.LoadScene(m_level);
        m_inGameUI.SetActive(true);
    }

    public void ExitGame() {
        Application.Quit();
    }
    public void ToSettings() {
        //Check what ui to disable
        if(m_state.Peek() == State.Pause) {
            m_pauseMenuUI.SetActive(false);
        }else if(m_state.Peek() == State.Title) {
            m_titleMenuUI.SetActive(false);
        }
        //set ui to active and update state
        m_settingsUI.SetActive(true);
        m_state.Push(State.Options);
        
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

        Time.timeScale = 1;
        //Enable Player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterControllerTest>().enabled = true;
        /*
        //Enable Player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterControllerTest>().enabled = true;
        */
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

    public void NextLevel(string name) {
        if(name == "Win") {
            InGameToWin();
        }
        SceneManager.LoadScene(name);
    }

    public void NewPlayer(GameObject player) {
        m_player = player;
        foreach(iUpgrade upgrade in m_playersUpgrades) {
            upgrade.Apply(m_player);
        }
        m_inGameUI.GetComponent<BasicInGameUi>().SetPlayer(m_player);//Set player on UI controller
    }

    
}
