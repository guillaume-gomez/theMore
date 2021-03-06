﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public LayerMask blockingLayer;

    private Weapon weapon;
    private Rigidbody2D rb2D;
    private BoxCollider2D boxCollider;
    private bool isInvisible;

    // Use this for initialization
    void Start () {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<Weapon>();
        isInvisible = false;
    }

    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        Move(movement);
    }

    void Update() {
        weapon.RotateToMouse();
    }

    bool Move(Vector2 movement) {
        RaycastHit2D hit;
        Vector2 start = transform.position;
        Vector2 end = start + (movement * speed * Time.fixedDeltaTime);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;
        if(hit.transform == null) {
            rb2D.MovePosition(end);
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Finish")
        {
            GameManager.instance.NextLevel();
            isInvisible = true;
            StartCoroutine(EndingAnimation(other.transform.position));
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "BulletEnemy") {
            Destroy(other.gameObject);
            //Camera.main.CameraShake(2.0f, 1.0f);
            if(!isInvisible) {
                GameManager.instance.GameOver(GameManager.instance.GameOverDie);
                gameObject.SetActive(false);
            }
        }
    }

    protected IEnumerator EndingAnimation(Vector3 end) {
        float step = ( speed / 2 ) * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, end, step);
        yield return null;
    }
}
