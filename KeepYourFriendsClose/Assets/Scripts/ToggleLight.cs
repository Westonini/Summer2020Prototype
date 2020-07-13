using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class ToggleLight : MonoBehaviour
{
    private Light2D objLight;
    public bool flicker;
    private float baseRange;

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

    void Start()
    {
        baseRange = objLight.pointLightOuterRadius;
    }

    private void ToggleTheLight()
    {
        if (!objLight.enabled)
        {
            objLight.enabled = true;

            if (flicker)
            {
                objLight.pointLightOuterRadius = baseRange / 2;
                StartCoroutine(Flicker(Random.Range(0.1f, 0.25f)));
            }
        }
        else
        {
            StopCoroutine("Flicker");
            objLight.enabled = false;
        }
    }

    private IEnumerator Flicker(float time)
    {
        objLight.enabled = false;
        yield return new WaitForSeconds(time);
        objLight.enabled = true;
        yield return new WaitForSeconds(time);
        StartCoroutine(Flicker(Random.Range(0f, 0.25f)));
    }
}
