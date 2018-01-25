using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TransitionMenuScript : MonoBehaviour
{
    private float restardLevelDelay = 2f;

    void Start() {
        Invoke("LoadingLevel", restardLevelDelay);
    }

    private void LoadingLevel()
    {
        SceneManager.LoadScene((int)ScreensEnum.GameScreen,  LoadSceneMode.Single);
    }
}
