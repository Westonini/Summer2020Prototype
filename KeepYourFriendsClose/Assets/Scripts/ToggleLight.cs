using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class ToggleLight : MonoBehaviour
{
    private Light2D objLight;

    private void OnEnable()
    {
        DayNightCycle._dayTime += ToggleTheLight;
        DayNightCycle._nightTime += ToggleTheLight;
    }

    private void OnDisable()
    {
        DayNightCycle._dayTime -= ToggleTheLight;
        DayNightCycle._nightTime -= ToggleTheLight;
    }

    void Awake()
    {
        objLight = GetComponent<Light2D>();
    }

    private void ToggleTheLight()
    {
        if (!objLight.enabled) { objLight.enabled = true; }
        else { objLight.enabled = false; }
    }
}
