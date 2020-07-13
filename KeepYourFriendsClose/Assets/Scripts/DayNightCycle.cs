using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class DayNightCycle : MonoBehaviour
{
    private Light2D sun;
    public bool turnCycleOn = false;
    public float periodDuration = 120f;

    [Space]
    public int timeOfDay = 0;
    private int period;

    public delegate void DayTime();
    public static event DayTime _dayTime;

    public delegate void NightTime();
    public static event NightTime _nightTime;

    private void Awake()
    {
        sun = GetComponent<Light2D>();
    }

    private void Start()
    {
        if (turnCycleOn)
            StartCoroutine(ProgressCycle());
        else
        {
            if (timeOfDay == 0)
                sun.intensity = 1f;
            if (timeOfDay == 1)
                sun.intensity = 0.8f;
            if (timeOfDay == 2)
                sun.intensity = 0.6f;
            if (timeOfDay == 3)
            {
                sun.intensity = 0.4f;
                if (_nightTime != null) { _nightTime(); }
            }
            if (timeOfDay == 4)
            {
                sun.intensity = 0.2f;
                if (_nightTime != null) { _nightTime(); }
            }
            if (timeOfDay == 5)
            {
                sun.intensity = 0.17f;
                if (_nightTime != null) { _nightTime(); }
            }
        }
    }

    private IEnumerator ProgressCycle()
    {
        yield return new WaitForSeconds(periodDuration);

        period++;
        sun.intensity -= 0.2f;
        if (period == 3)
        {
            if (_nightTime != null) { _nightTime(); }
        }
        if (period == 5)
        {
            if (_dayTime != null) { _dayTime(); }
            sun.intensity = 1f;
            period = 0;
        }

        StartCoroutine(ProgressCycle());
    }
}
