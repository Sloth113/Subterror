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

    [Tooltip("UI prefabs")]
    public GameObject titleMenuUI;
    [Tooltip("UI prefabs")]
    public GameObject pauseMenuUI;
    [Tooltip("UI prefabs")]
    public GameObject inGameUI;
    [Tooltip("UI prefabs")]
    public GameObject settingsUI;
    [Tooltip("UI prefabs")]
    public GameObject mutagenMenuUI;
    [Tooltip("UI prefabs")]
    public GameObject scrapMenuUI;

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
        titleMenuUI.SetActive(true);
        //Set menus not to destroy
        DontDestroyOnLoad(titleMenuUI);
        DontDestroyOnLoad(pauseMenuUI);
        DontDestroyOnLoad(inGameUI);
        DontDestroyOnLoad(mutagenMenuUI);
        DontDestroyOnLoad(scrapMenuUI);
        //Player
        //DontDestroyOnLoad(m_player);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel") && m_state.Peek() == State.InGame) {
            m_state.Push(State.Pause);
            inGameUI.SetActive(false);
            pauseMenuUI.SetActive(true);

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
        titleMenuUI.SetActive(false);
        m_state.Pop();
        m_state.Push(State.InGame);
        m_level = "level_1-1";
        m_timer = 0;
        
        SceneManager.LoadScene(m_level);
        inGameUI.SetActive(true);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void ExitSettings() {
        m_state.Pop();
        pauseMenuUI.SetActive(false);
        if(m_state.Peek() == State.InGame) {
            inGameUI.SetActive(true);
        }else if(m_state.Peek() == State.Title) {
            titleMenuUI.SetActive(true);
        }
    }

    public void PauseToIngame() {
        m_state.Pop();//Pause
        m_state.Push(State.InGame);
        pauseMenuUI.SetActive(false);
        inGameUI.SetActive(true);

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
        titleMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;

        SceneManager.LoadScene("TitleScreen");
    }

    public void InGameToDead() {

    }

    public void NextLevel(string name) {
        SceneManager.LoadScene(name);
    }

    public void NewPlayer(GameObject player) {
        m_player = player;
        foreach(iUpgrade upgrade in m_playersUpgrades) {
            upgrade.Apply(m_player);
        }
    }
}
