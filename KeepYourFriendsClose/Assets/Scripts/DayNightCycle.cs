using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class DayNightCycle : MonoBehaviour
{
    private Light2D sun;
    public bool turnCycleOn = false;
    public float periodDuration = 120f;
    private int period;
    private float timer = 0f;

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
