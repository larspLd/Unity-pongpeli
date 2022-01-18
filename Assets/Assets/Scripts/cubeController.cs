using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeController : MonoBehaviour
{
    Vector3 position;
    float distance;

    GameObject tempPlayer;
    Transform player;   

    public float speed;

    void Start() {
        tempPlayer = GameObject.Find("Capsule");
        player = tempPlayer.GetComponent<Transform>();
    }

    void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);
        position = new Vector3(transform.position.x, (20 * Mathf.Sin(speed * Time.timeSinceLevelLoad)), transform.position.z);

        transform.position = position;
    }
}
