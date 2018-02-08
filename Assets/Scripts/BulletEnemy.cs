using UnityEngine;
using System.Collections;

public class BulletEnemy : MonoBehaviour
{
    public float speed = 15.0f;
    private Rigidbody2D rb2D;

    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = transform.up * speed;
        rb2D.rotation = -90.0f;
        Destroy(gameObject, 2.0f);
    }
}