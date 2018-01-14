// inspired from http://blog.huthaifa-abd.com/gamedevelopment/attach-weapon-to-character.html
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
  public GameObject bulletPrefab;
  //temporary disable it
  //public Transform bulletSpawn;
  public float timer = 1.0f;

  private bool onceShootFunctionCalled = false;

  void Start () {
  }

  void Update () {
  }

  public void RotateWeapon() {
     Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
     Vector3 dir = Input.mousePosition - pos;
     float angle = - 90 + Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
     transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
  }

  public void Fire()
  {
    if(!onceShootFunctionCalled) {
        Invoke("Shoot", timer);
        onceShootFunctionCalled = true;
    }
  }

  protected void Shoot() {
    onceShootFunctionCalled = false;
  }
}
