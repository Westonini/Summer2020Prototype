﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public AITargetDetector detectorScript;

    void Update()
    {
        if (detectorScript.IsTargetFound())
            transform.up = (detectorScript.GetTarget().position - transform.position).normalized;
    }
}
