using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light lightComponent;
    public GameObject otherLight;
    public Light otherLightComponent;

    [Range(0,100)] 
    public float intensityBias;

    void Update() {
        changeIntensity();

        lightComponent.intensity = intensityBias  / 10;
    }

    public void changeIntensity() {
        otherLightComponent.intensity = (100 - intensityBias) / 10;
    }
}
