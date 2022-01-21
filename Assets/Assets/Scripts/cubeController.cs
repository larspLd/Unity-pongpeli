using UnityEngine;

public class cubeController : MonoBehaviour
{
    Vector3 position;

    public float speed;
    private float perlin;

    GameObject tempCamera;
    BlitTest cameraObject;

    void Start() {

        tempCamera = GameObject.Find("Camera");
        cameraObject = tempCamera.GetComponent<BlitTest>();
        cameraObject.mat.SetFloat("_Zoom", 60f);
        perlin = Random.Range(0.7f, 1.3f);
    }

    void Update()
    {
        position = new Vector3(transform.position.x, (20 * Mathf.Sin(speed * Time.timeSinceLevelLoad + transform.position.z * 0.03f + transform.position.x * 0.03f)) + 1, transform.position.z);

        transform.position = position;
    }
}
