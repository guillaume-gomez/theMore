using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon {

  // Use this for initialization
  void Start () {
    timer = 0.5f;
  }

  void Shoot() {
    // see base for the code commented
    Instantiate(bulletPrefab, transform.position/*bulletSpawn.position*/, transform.rotation);
    base.Shoot();
  }
}
