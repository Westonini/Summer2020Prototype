using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    private EnemyAggro EA;

    void Awake()
    {
        EA = gameObject.transform.parent.GetComponentInChildren<EnemyAggro>();
    }

    void Update()
    {
        if (EA.IsTargetFound())
            transform.up = (EA.GetTarget().transform.position - transform.position).normalized;
    }
}
