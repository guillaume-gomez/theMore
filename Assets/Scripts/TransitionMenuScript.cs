using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TransitionMenuScript : MonoBehaviour
{
    private float restardLevelDelay = 200f; // temporary
    private float delayShowText = 5f;

    public GameObject textCanvas;
    public GameObject weaponSelectionCanvas;

    void Start() {
        //ShowTextCanvas();
        ShowWeaponSelection();
        //Invoke("LoadingLevel", restardLevelDelay);
    }

    private void ShowTextCanvas() {
      textCanvas.SetActive(true);
      weaponSelectionCanvas.SetActive(false);
      Invoke("ShowWeaponSelection", delayShowText);
    }

    private void ShowWeaponSelection() {
      textCanvas.SetActive(false);
      weaponSelectionCanvas.SetActive(true);
      Invoke("LoadingLevel", restardLevelDelay);
    }

    private void LoadingLevel()
    {
        SceneManager.LoadScene((int)ScreensEnum.GameScreen,  LoadSceneMode.Single);
    }
}
