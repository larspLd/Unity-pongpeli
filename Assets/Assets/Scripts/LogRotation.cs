using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRotation : MonoBehaviour
{
    public float angle;
    public float movingSpeed = 2.0f;
    
    Vector3 position;
    Vector3 rotation;
    float moveSideways;

    // Update is called once per frame
    void Update()
    {
        rotation = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));

        Debug.DrawRay(transform.position, rotation * 5, Color.green);

        moveSideways = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate() {
        position = transform.position;
        position.x = position.x + movingSpeed * moveSideways * Time.deltaTime;
        transform.position = position;

        if(Input.GetKey("up") || Input.GetKey("down")) {
            if(Input.GetKey("up")) {
                angle -= 3;
            } else if (Input.GetKey("down")) {
                angle += 3;
            }
        }
    }
}
