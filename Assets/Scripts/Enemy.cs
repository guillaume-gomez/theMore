using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour {

    public float minTarX = 1f;
    public float maxTarX = 10f;
    public float minTarY = 1f;
    public float maxTarY = 10f;
    public float speed = 0.8f;

    public Vector3 target;

    void Start() {
        CreateTarPoint();
    }

    void CreateTarPoint() {
        int dampX = Random.Range(1, 3);
        int dampY = Random.Range(1, 3);

        float tarX = Random.Range(minTarX, maxTarX) - dampX;
        float tarY = Random.Range(minTarY, maxTarY) - dampY;
        target = new Vector3(tarX + transform.position.x, tarY + transform.position.y, 0.0f);
    }

    void Update() {
        if(Vector3.Distance(transform.position, target) <= 0.5) {
            CreateTarPoint();
        } else {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
    }

}