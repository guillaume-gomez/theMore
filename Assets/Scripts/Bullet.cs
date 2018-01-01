using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 15.0f;
    private Rigidbody2D rb2D;

    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        Vector2 target = Camera.main.ScreenToWorldPoint( new Vector2(Input.mousePosition.x, Input.mousePosition.y) );
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = target - myPos;
        direction.Normalize();

        rb2D.velocity = direction * speed;
        Destroy(gameObject, 2.0f);
    }


    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy" /*|| other.gameObject.tag == "Player" */) {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}