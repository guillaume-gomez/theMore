using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayWeapons : MonoBehaviour {

  public GameObject weaponButtonPrefab;
  public GameObject lockedWeaponButtonPrefab;
  public int nbWeapons = 4;

	// Use this for initialization
	void Start () {
    for(int i = 0; i < nbWeapons; ++i) {
      GameObject button = null;
      if (i < GameManager.instance.NbWeaponsAvailable) {
        button = (GameObject) Instantiate(weaponButtonPrefab);
        // due to closure
        int weaponIndex = i;
        button.GetComponent<Button>().onClick.AddListener(() => OnClickButton(weaponIndex));
      } else {
        button = (GameObject) Instantiate(lockedWeaponButtonPrefab);
      }
      button.transform.position = transform.position;
      button.GetComponent<RectTransform>().SetParent(transform);
      button.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, (110 * (i + 1)) + (150 * i), 60);
      button.GetComponentInChildren<Text>().text = "" + (i + 1);
    }
  }

  private void OnClickButton(int weaponIndex) {
    GameManager.instance.CurrentIndexWeaponHero = weaponIndex;
    SceneManager.LoadScene((int)ScreensEnum.GameScreen,  LoadSceneMode.Single);
  }
}
