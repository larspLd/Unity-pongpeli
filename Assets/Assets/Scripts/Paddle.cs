using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public bool isPlayer1;
    public float speed;
    
    private float movement;

    void Update()
    {
        if (isPlayer1) {
            movement = Input.GetAxisRaw("Vertical");
        } else {
            movement = Input.GetAxisRaw("Vertical2");
        }

        if(transform.position.z + movement > -80 && transform.position.z + movement < 80) {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + movement * speed);
        }
    }
}
