using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uzi : Weapon {
  public int nbBulletsShoot = 5;
  public float timerBetweenBullet = 1.0f;
  // Use this for initialization
  void Start () {
    timer = 0.0f;
  }

  new void Shoot() {
    for(int i = 0; i < nbBulletsShoot; ++i) {
      Invoke("CreateBullet", timerBetweenBullet * (i + 1));
    }
    base.Shoot();
  }

  void CreateBullet() {
    GameObject bullet = Instantiate(bulletPrefab, transform.position/*bulletSpawn.position*/, transform.rotation);
    bullet.gameObject.tag = this.bulletTag;
    bullet.layer = LayerMask.NameToLayer(this.bulletTag);
  }
}
