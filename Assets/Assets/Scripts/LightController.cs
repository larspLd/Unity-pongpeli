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

    [Range(0,20)]
    public int intensityScale;

    void Update() {
        changeIntensity();
    }

    public void changeIntensity() {
        otherLightComponent.intensity = (100 - intensityBias) / intensityScale;
        lightComponent.intensity = intensityBias  / intensityScale;
    }
}
