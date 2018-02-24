using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon {
  public int nbBulletsShoot = 5;
  public float rotation = 30; // in degrees

  void Start () {
    timer = 0.5f;
  }

  new void Shoot() {
    float startAngle = -Mathf.FloorToInt((nbBulletsShoot - 1) / 2 ) * rotation;
    for(int i = 0; i < nbBulletsShoot; i++, startAngle += rotation) {
      Debug.Log(transform.rotation);
      GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(startAngle, transform.up) * transform.rotation);
      bullet.gameObject.tag = this.bulletTag;
      bullet.layer = LayerMask.NameToLayer(this.bulletTag);
    }
    base.Shoot();
  }
}
