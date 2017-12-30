using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour {

    public float speed = 0.8f;
    public LayerMask blockingLayer;
    public Transform playerTransform;

    private Vector3 target;
    private float tarX = 0.0f;
    private float tarY = 0.0f;

    void Start() {
        CreateTarPoint();
    }

    void CreateTarPoint() {
        float offset = 5;
        float minTarX = -offset;
        float maxTarX = offset;
        float minTarY = -offset;
        float maxTarY = offset;

        int dampX = Random.Range(1, 3);
        int dampY = Random.Range(1, 3);

        tarX = Random.Range(minTarX, maxTarX) - dampX;
        tarY = Random.Range(minTarY, maxTarY) - dampY;
        target = new Vector3(tarX + transform.position.x, tarY + transform.position.y, 0.0f);
    }

    void Update() {
        if(Vector3.Distance(transform.position, target) <= 0.05) {
            CreateTarPoint();
            return;
        }
        RaycastHit2D hit;
        hit = Physics2D.Linecast(transform.position, target, blockingLayer);
        if(hit.transform == null) {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        } else {
            target = new Vector3(-tarX + transform.position.x, -tarY + transform.position.y, 0.0f);
            //Debug.Log(target);
        }

    }

}