using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody2D rb2D;
    private BoxCollider2D boxCollider;
    public LayerMask blockingLayer;
    // Use this for initialization
    void Start () {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        //rb2D.freezeRotation = true;
    }

    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        Move(movement);
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
}
