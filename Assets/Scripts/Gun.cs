﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon {
  // Use this for initialization
  void Start () {
    timer = 0.0f;
  }

  void Shoot() {
    // see base for the code commented
    Instantiate(bulletPrefab, transform.position/*bulletSpawn.position*/, transform.rotation);
    base.Shoot();
  }
}
