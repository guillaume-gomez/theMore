// inspired from http://blog.huthaifa-abd.com/gamedevelopment/attach-weapon-to-character.html
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttachment : MonoBehaviour {
  public GameObject[] weapons;
  public bool forPlayer = false;

  void Start () {
    GameObject weapon = null;
    if(GameManager.instance.NbWeaponsAvailable > weapons.Length) {
      weapon = weapons[0];//weapons[Random.Range(0, weapons.Length)];
    } else if (!forPlayer) {
      weapon = weapons[Random.Range(0, GameManager.instance.NbWeaponsAvailable)];
    } else {
      weapon = weapons[GameManager.instance.CurrentIndexWeaponHero];
    }
    //Create an instance of the weapon and set the state to idle
    GameObject weaponSlot = Instantiate(weapon, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
    //Attach the weapon to the desired bone or submesh make sure its a child to the player object
    weaponSlot.transform.parent = transform;
    //Set the desired idle position for the weapon
    weaponSlot.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
    //set the desired idle rotation for the weapon
    //weaponSlot.transform.localEulerAngles  = new Vector3(6.97f,42.36f,90f);
  }

  void Update () {
  }
}
