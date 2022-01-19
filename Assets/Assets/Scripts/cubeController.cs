using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeController : MonoBehaviour
{
    Vector3 position;
    float distance;

    GameObject tempPlayer;
    GameObject tempCamera;
    BlitTest cameraObject;

    Shader cameraShader;

    Transform player;   
    MeshRenderer meshRenderer;

    public float speed;

    void Start() {
        tempPlayer = GameObject.Find("Capsule");
        player = tempPlayer.GetComponent<Transform>();


        /*tempCamera = GameObject.Find("Camera");
        cameraObject = tempCamera.GetComponent<BlitTest>();
        cameraObject.mat.SetFloat("_Zoom", 60f);*/
        Debug.Log(StateController.randomNumber);
    }

    void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);
        position = new Vector3(transform.position.x, (20 * Mathf.Sin(speed * Time.timeSinceLevelLoad)), transform.position.z);

        transform.position = position;
    }
}
