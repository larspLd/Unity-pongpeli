using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{

    // Menu väri vaihtaja.

    public Slider Player1Red;
    public Slider Player1Green;
    public Slider Player1Blue;
    public Slider Player1Alpha;

    public Slider Player2Red;
    public Slider Player2Green;
    public Slider Player2Blue;
    public Slider Player2Alpha;

    Vector4 Player1Color;
    Vector4 Player2Color;

    void Update()
    {
        Player1Color = new Color(Player1Red.value, Player1Green.value, Player1Blue.value, Player1Alpha.value);
        StateController.player1Color = Player1Color;

        Player2Color = new Color(Player2Red.value, Player2Green.value, Player2Blue.value, Player2Alpha.value);
        StateController.player2Color = Player2Color;
    }
}