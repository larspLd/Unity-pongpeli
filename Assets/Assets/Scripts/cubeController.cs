using UnityEngine;

public class cubeController : MonoBehaviour
{
    Vector3 position;

    public float speed;

    void Update()
    {
        position = new Vector3(transform.position.x, (20 * Mathf.Sin(speed * Time.timeSinceLevelLoad + transform.position.z * 0.03f + transform.position.x * 0.03f)) + 1, transform.position.z);
        transform.position = position;
    }
}
