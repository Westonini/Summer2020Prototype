using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    private EnemyStateManager ESM;
    private Transform target;
    private bool targetInRange;

    private void Awake()
    {
        ESM = GetComponentInParent<EnemyStateManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Human") && ESM.GetState() != EnemyStateManager.State.Dead)
        {
            target = collision.gameObject.transform;
            targetInRange = true;
            ESM.SetState(EnemyStateManager.State.Attack);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Human") && target == collision.gameObject.transform && ESM.GetState() != EnemyStateManager.State.Dead)
        {
            target = null;
            targetInRange = false;
            ESM.SetState(EnemyStateManager.State.Aggro);
        }
    }

    public bool IsTargetFound() { return targetInRange; }
    public Transform GetTarget() { return target; }
    public CharacterHealth GetCharHealth() { return target.GetComponent<CharacterHealth>(); }
}
