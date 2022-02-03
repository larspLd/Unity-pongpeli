using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuCameraMover : MonoBehaviour
{
    Vector3 position;
    void Update()
    {

        position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f);
        
        if(position.z > 500) {
            position.z = -1000;
        }

        transform.position = position;
    }
}
