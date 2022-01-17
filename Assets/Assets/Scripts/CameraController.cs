using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Quaternion rotation;
    public float rotationSpeed = 1f;

    GameObject tempPlayer;
    Transform player;

    Vector3 position;

    void Start() {
        tempPlayer = GameObject.Find("Capsule");
        player = tempPlayer.GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKey("w")) {
            rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + rotationSpeed);
            transform.rotation = rotation;
            
        } else if(Input.GetKey("s")) {
            rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - rotationSpeed);
            transform.rotation = rotation;
        }
    }
}
