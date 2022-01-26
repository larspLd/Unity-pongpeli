using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLightController : MonoBehaviour
{

    public Light lightComponent;
    public Light otherLightComponent;
    // Update is called once per frame
    void Update()
    {
        lightComponent.color = StateController.player1Color;
        otherLightComponent.color = StateController.player2Color;
    }
}
