using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public LayerMask blockingLayer;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    private Rigidbody2D rb2D;
    private BoxCollider2D boxCollider;
    private float restardLevelDelay = 1f;

    // Use this for initialization
    void Start () {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Fire();
        }
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

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Finish")
        {
            GameManager.instance.NextLevel();
            StartCoroutine(EndingAnimation(other.transform.position));
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "BulletEnemy") {
            Destroy(other.gameObject);
            //Camera.main.CameraShake(2.0f, 1.0f);
            GameManager.instance.GameOver();
            gameObject.SetActive(false);
        }
    }

    protected IEnumerator EndingAnimation(Vector3 end) {
        float step = ( speed / 2 ) * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, end, step);
        yield return null;
    }

    void Fire()
    {
        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
    }
}
