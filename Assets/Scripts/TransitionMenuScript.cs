using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionMenuScript : MonoBehaviour
{
    private float delayShowText = 5f;

    public string[] textContents;
    public GameObject textCanvas;
    public GameObject weaponSelectionCanvas;
    public Text contentText;

    void Start() {
        ShowTextCanvas();
    }

    private void ShowTextCanvas() {
      textCanvas.SetActive(true);
      int index = GameManager.instance.Level / GameManager.instance.NbLevelBeforeNextWeapon;
      contentText.text = textContents[index];
      weaponSelectionCanvas.SetActive(false);
      Invoke("ShowWeaponSelection", delayShowText);
    }

    private void ShowWeaponSelection() {
      textCanvas.SetActive(false);
      weaponSelectionCanvas.SetActive(true);
    }
}
