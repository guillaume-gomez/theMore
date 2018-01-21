// inspired from http://blog.huthaifa-abd.com/gamedevelopment/attach-weapon-to-character.html
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
  public GameObject bulletPrefab;
  //temporary disable it
  //public Transform bulletSpawn;
  public float timer = 1.0f;
  public AudioClip shootSound;
  protected string bulletTag = "Bullet";


  private bool onceShootFunctionCalled = false;

  void Start () {
  }

  void Update () {
  }

  public void RotateToMouse() {
     Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
     Vector3 dir = Input.mousePosition - pos;
     float angle = - 90 + Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
     transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
  }

  public void RotateTo(GameObject targetObj) {
    Vector2 target = new Vector2(targetObj.transform.position.x, targetObj.transform.position.y);
    Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
    Vector2 dir = target - myPos;
    dir.Normalize();
    float angle = - 90 + Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
  }

  public void Fire(string bulletTag = "Bullet")
  {
    if(!onceShootFunctionCalled) {
        Invoke("Shoot", timer);
        onceShootFunctionCalled = true;
    }
    this.bulletTag = bulletTag;
  }

  protected void Shoot() {
    onceShootFunctionCalled = false;
    SoundManager.instance.PlaySingle(shootSound);
  }

  public void Disable() {
    gameObject.SetActive(false);
  }

  public void Enable() {
    gameObject.SetActive(true);
  }
}
