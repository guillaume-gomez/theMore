using UnityEngine;
using System.Collections;

public class BulletEnemy : MonoBehaviour
{
    public float speed = 15.0f;
    private Rigidbody2D rb2D;
    private GameObject player;

    void Start() {
        player = GameObject.FindWithTag("Player");
        rb2D = GetComponent<Rigidbody2D>();
        Vector2 target = Camera.main.ScreenToWorldPoint( new Vector2(player.transform.position.x, player.transform.position.y) );
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = target - myPos;
        direction.Normalize();

        rb2D.velocity = direction * speed;
        Destroy(gameObject, 2.0f);
    }

}