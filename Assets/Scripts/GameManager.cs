using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager boardScript;

    public string GameOverDie = "you died";
    public string GameOverKillInnocent = "you killed an honest citizen";

    private int level = 1;
    private bool needUpdate;
    private float restardLevelDelay = 2f;
    private int nbWeaponsAvailable = 1;
    private int nbLevelBeforeNextWeapon = 5;
    private int currentIndexWeaponHero = 0;
    private GameObject gameOverImage;
    private Text gameOverText;
    private GameObject restardButton;

    private bool doingSetup;

    public int NbWeaponsAvailable {
        get
        {
            if(instance == null) {
                instance = this;
            }
            return instance.nbWeaponsAvailable;
        }
    }

    public int NbLevelBeforeNextWeapon {
        get
        {
            if(instance == null) {
                instance = this;
            }
            return instance.nbLevelBeforeNextWeapon;
        }
    }
    public int Level {
        get {
            if(instance == null) {
                instance = this;
            }
            return instance.level;
        }
    }

    public int CurrentIndexWeaponHero { get; set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        // to enable only when mainScene is launch as standalone
        InitGame();
    }

    static private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        //do not load in other scene
        if(scene.buildIndex == (int)ScreensEnum.GameScreen) {
            instance.InitGame();
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
     static public void CallbackInitialization()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void InitGame()
    {
        Debug.Log("InitGame level: " + level);
        gameOverImage = GameObject.Find("GameOverImage");
        gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();

        GameObject.Find("RestartButton").GetComponent<Button>().onClick.AddListener(() => OnRestartClick());
        gameOverImage.SetActive(false);

        needUpdate = true;
        int size =  50 + (level % nbLevelBeforeNextWeapon) * 10;
        boardScript.SetupScene(level, size, size);
    }

    private void OnRestartClick() {
        level = 1;
        ReloadCurrentLevel();
    }

    public void GameOver(string reason)
    {
        gameOverText.text = "After " + level + " days, " + reason + ".";
        gameOverImage.SetActive(true);

    }

    public void NextLevel() {
        if(needUpdate) {
            GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("BulletEnemy");
            for(int i = 0; i < enemyBullets.Length; ++i) {
                Destroy(enemyBullets[i]);
            }
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
            for(int i = 0; i < enemys.Length; ++i) {
                Debug.Log("forbidden");
                //Destroy(enemys[i]);
                enemys[i].GetComponent<Enemy>().HasWeapon = false;
            }
            level++;
            if(level % nbLevelBeforeNextWeapon == 0) {
                nbWeaponsAvailable++;
            }
            Invoke("LoadingLevel", restardLevelDelay);
            needUpdate = false;
        }
    }

    private void LoadingLevel()
    {
        SceneManager.LoadScene((int)ScreensEnum.WinScreen, LoadSceneMode.Single);
    }

    private void ReloadCurrentLevel() {
        gameOverImage.SetActive(true);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

}