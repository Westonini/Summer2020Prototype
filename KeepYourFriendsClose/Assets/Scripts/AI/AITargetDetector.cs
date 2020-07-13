using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITargetDetector : MonoBehaviour
{
    public LayerMask targetLayer;
    public AIStateManager stateManager;
    private Transform target;

    public Transform GetTarget()
    {
        return target;
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        stateManager.SetState(AIStateManager.State.Aggro);
    }

    public bool IsTargetFound()
    {
        if (target != null)
            return true;
        else
            return false;
    }

    public void TargetDied()
    {
        stateManager.SetState(AIStateManager.State.Idle);
        target = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetLayer == (targetLayer | 1 << collision.gameObject.layer) && stateManager.GetState() != AIStateManager.State.Dead && !IsTargetFound())
        {
            target = collision.gameObject.transform;
            stateManager.SetState(AIStateManager.State.Aggro);
        }
    }
}
