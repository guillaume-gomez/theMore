using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
    public float cameraZ = 10.0f;
    public GameObject bulletPrefab;

    private float radius = 0.5f;
    private float currentAngle = 0.0f;

    void Start() {
        InvokeRepeating("Shoot", 2.0f, 1.0f);
    }

    void Update() {
        Vector3 mouse_pos = Input.mousePosition;
        mouse_pos.z = cameraZ; //The distance between the camera and object

        Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;

        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        currentAngle = angle;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }

    void Shoot() {
        Vector3 offset = new Vector3(Mathf.Sin(currentAngle), Mathf.Cos(currentAngle), 0.0f) * radius;
        Vector3 bulletPosition = transform.position;// + offset;
        //Debug.Log(bulletPosition);
        GameObject bullet = (GameObject)Instantiate (bulletPrefab, bulletPosition, Quaternion.Euler(new Vector3(0,0,0)));
    }

}
