using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon {

  // Use this for initialization
  void Start () {
    timer = 0.5f;
  }

  new void Shoot() {
    // see base for the code commented
    GameObject bullet = Instantiate(bulletPrefab, transform.position/*bulletSpawn.position*/, transform.rotation);
    bullet.gameObject.tag = this.bulletTag;
    bullet.layer = LayerMask.NameToLayer(this.bulletTag);
    base.Shoot();
  }
}
