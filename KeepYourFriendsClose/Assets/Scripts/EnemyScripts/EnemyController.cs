using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public int damage;
    public Animator anim;

    private EnemyStateManager ESM;
    private EnemyAggro EA;
    private EnemyMeleeAttack EMA;

    private AIPath aiPath;
    private AIDestinationSetter aiDestSetter;

    private void Awake()
    {
        ESM = GetComponent<EnemyStateManager>();
        aiPath = GetComponent<AIPath>();
        aiDestSetter = GetComponent<AIDestinationSetter>();

        EA = GetComponentInChildren<EnemyAggro>();
        EMA = GetComponentInChildren<EnemyMeleeAttack>();
    }

    private void Start()
    {
        ESM.SetState(EnemyStateManager.State.Idle);
        Idle();
    }

    public void Idle()
    {
        aiPath.canMove = false;
        aiDestSetter.target = null;
        anim.SetBool("Walking", false);
    }

    public void Aggro()
    {
        aiPath.canMove = true;
        aiDestSetter.target = EA.GetTarget();
        anim.SetBool("Walking", true);
    }

    public void Attack()
    {
        EMA.GetCharHealth().TakeDamage(damage);
    }

    public void Dead()
    {
        aiPath.canMove = false;
        aiDestSetter.target = null;
        anim.SetBool("Walking", false);
    }
}
