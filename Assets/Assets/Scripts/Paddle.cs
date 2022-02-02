using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Paddle : MonoBehaviour
{

    // Paddle movement.

    public bool isPlayer2;
    public float speed;
    
    private float movement;
    public Vector4 playerColor;

    public GameObject textObject;
    TMP_Text readyText;

    public bool ready;

    private void Start() {
        ready = false;

        if (isPlayer2) {
            playerColor = StateController.player2Color;
        } else {
            playerColor = StateController.player1Color;
        }

        readyText = textObject.GetComponent<TMP_Text>();
    }


    // FixedUpdat() toimii 60fps vaikka fps ei olisi 60. Sen takia movement pitää laittaa FixedUpdate() ja ei Update()
    private void FixedUpdate() {
        if (isPlayer2) {
            movement = Input.GetAxisRaw("Vertical");
        } else {
            movement = Input.GetAxisRaw("Vertical2");
        }

        if(!ready) {
            if (movement > 0) {
                ready = true;
                readyText.text = "VALMIS!";
            }

        } else {
            if(transform.position.z + movement > -80 && transform.position.z + movement < 80) {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + movement * speed * Time.deltaTime);
            }
        }
    }
}
