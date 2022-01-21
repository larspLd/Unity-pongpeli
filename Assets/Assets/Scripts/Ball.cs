using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public float speed;
    public float maxSpeed;

    void Start()
    {
        Launch();
    }

    private void Launch() {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        Vector3 direction = new Vector3(x, 0, y);
        rb.velocity = new Vector3(speed * x, 0, speed * y);
        
    }

    private void OnCollisionEnter(Collision collider) {
        Paddle paddle = collider.gameObject.GetComponent<Paddle>();

        if (paddle != null) {

            if (speed < maxSpeed) {
                speed += 10f; 
                rb.velocity = new Vector3(rb.velocity.x + speed * 0.005f* rb.velocity.x, 0, rb.velocity.z + speed * 0.005f * rb.velocity.z);
            }
        }
    }
}
