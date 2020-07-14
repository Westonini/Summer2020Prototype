using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITargetDetector : MonoBehaviour
{
    public LayerMask targetLayer;
    public AIStateManager stateManager;
    private Transform target;
    private CharacterHealth targetHealth;

    public delegate void TargetDetected();
    public event TargetDetected _targetDetected;
    public delegate void TargetKilled();
    public event TargetKilled _targetKilled;

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
        targetHealth = null;

        if (_targetKilled != null)
            _targetKilled();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetLayer == (targetLayer | 1 << collision.gameObject.layer) && stateManager.GetState() != AIStateManager.State.Dead && !IsTargetFound())
        {
            target = collision.gameObject.transform;
            targetHealth = collision.gameObject.GetComponent<CharacterHealth>();
            stateManager.SetState(AIStateManager.State.Aggro);

            if (_targetDetected != null)
                _targetDetected();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (targetLayer == (targetLayer | 1 << collision.gameObject.layer) && stateManager.GetState() != AIStateManager.State.Dead && !IsTargetFound())
        {
            target = collision.gameObject.transform;
            targetHealth = collision.gameObject.GetComponent<CharacterHealth>();
            stateManager.SetState(AIStateManager.State.Aggro);

            if (_targetDetected != null)
                _targetDetected();
        }
    }

    private void Update()
    {
        if (targetHealth != null && targetHealth.GetHealth() == 0)
            TargetDied();
    }
}
