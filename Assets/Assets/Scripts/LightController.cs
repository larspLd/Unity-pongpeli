using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    // Peli väri controller. Kun pelaaja saa pisteitä vaihda kirkkaus.

    public Light lightComponent;
    public Light otherLightComponent;

    [Range(0,100)] 
    public float intensityBias;

    [Range(0,20)]
    public int intensityScale;

    private void Start() {
        lightComponent.color = StateController.player1Color;
        otherLightComponent.color = StateController.player2Color;
    }

    void Update() {
        changeIntensity();
    }

    public void changeIntensity() {
        otherLightComponent.intensity = (100 - intensityBias) / intensityScale;
        lightComponent.intensity = intensityBias  / intensityScale;
    }
}
