// inspired from http://blog.huthaifa-abd.com/gamedevelopment/attach-weapon-to-character.html
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
  public GameObject bulletPrefab;
  public Transform bulletSpawn;
  public float timer = 1.0f;

  private bool onceShootFunctionCalled = false;

  void Start () {
  }

  void Update () {
  }

  public void Fire()
  {
    Debug.Log("Fire");
    if(!onceShootFunctionCalled) {
        Invoke("Shoot", timer);
        onceShootFunctionCalled = true;
    }
  }

  void Shoot() {
    Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
    onceShootFunctionCalled = false;
  }
}
