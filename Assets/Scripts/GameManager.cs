using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager boardScript;

    private int level = 1;
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
        needUpdate = true;
        boardScript.SetupScene(level);
    }

    public void GameOver()
    {
        // DO SOME STUFF
        Invoke("ReloadCurrentLevel", restardLevelDelay);
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
        SceneManager.LoadScene((int)ScreensEnum.WinScreen, LoadSceneMode.Single);
    }

    private void ReloadCurrentLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

}