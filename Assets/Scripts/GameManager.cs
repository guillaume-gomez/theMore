using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager boardScript;

    private int level = 1;
    private bool doingSetup;
    private bool needUpdate;
    private float restardLevelDelay = 1f;

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
        InitGame();
    }

    static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        instance.InitGame();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
     static public void CallbackInitialization()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void InitGame()
    {
        doingSetup = true;
        needUpdate = true;
        //Invoke("HideLevelImage", levelStartDelay);
        boardScript.SetupScene(level);
    }

    private void HideLevelImage()
    {
        doingSetup = false;
    }

    public void GameOver()
    {
        // DO SOME STUFF
        Invoke("LoadingLevel", restardLevelDelay);
        enabled = false;
    }

    public void NextLevel() {
        if(needUpdate) {
            level++;
            Invoke("LoadingLevel", restardLevelDelay);
            needUpdate = false;
        }
    }

    private void LoadingLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    void Update()
    {
    }
}


public enum GameStates {
    Intro,
    Mainmenu,
    LevelSelection
}