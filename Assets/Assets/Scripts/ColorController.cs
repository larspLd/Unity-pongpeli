using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{

    // Menu pelaaja väri kuva.
    Image Player1Image;

    public GameObject Player2Object;
    Image Player2Image;


    private void Start() {
        Player1Image = GetComponent<Image>();
        Player2Image = Player2Object.GetComponent<Image>();
    }

    private void Update() {
        Player1Image.color = StateController.player1Color;
        Player2Image.color = StateController.player2Color;
    }
}