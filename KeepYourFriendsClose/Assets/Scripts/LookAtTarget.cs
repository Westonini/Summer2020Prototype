using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    private AITargetDetector detectorScript;

    void Awake()
    {
        detectorScript = gameObject.transform.parent.GetComponentInChildren<AITargetDetector>();
    }

    void Update()
    {
        if (detectorScript.IsTargetFound())
            transform.up = (detectorScript.GetTarget().transform.position - transform.position).normalized;
    }
}
