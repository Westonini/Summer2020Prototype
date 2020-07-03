using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    public EnemyStateManager ESM;
    public EnemyController EC;
    private Transform target;
    private bool targetFound = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Human"))
        {
            target = collision.gameObject.transform;
            targetFound = true;
            ESM.SetState(EnemyStateManager.State.Aggro);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Human") && target == collision.gameObject.transform)
        {
            target = null;
            targetFound = false;
            ESM.SetState(EnemyStateManager.State.Idle);
        }
    }

    public bool IsTargetFound() { return targetFound; }
    public Transform GetTarget() { return target; }
}
