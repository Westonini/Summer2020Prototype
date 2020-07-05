using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public int damage;
    public float attackCooldown;
    public Animator anim;

    private EnemyStateManager ESM;
    private EnemyAggro EA;
    private EnemyMeleeAttack EMA;

    private AIPath aiPath;
    private AIDestinationSetter aiDestSetter;

    private bool attackOnCD;
    private float baseSpeed;

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
        baseSpeed = aiPath.maxSpeed;
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
        if (!attackOnCD)
            StartCoroutine(AttackCooldown());
    }

    public void Dead()
    {
        aiPath.canMove = false;
        aiDestSetter.target = null;
        anim.SetBool("Walking", false);
    }

    private IEnumerator AttackCooldown()
    {
        attackOnCD = true;
        aiPath.maxSpeed = 0.5f;
        EMA.GetCharHealth().TakeDamage(damage);
        yield return new WaitForSeconds(attackCooldown);
        attackOnCD = false;
        aiPath.maxSpeed = baseSpeed;

        if (EMA.IsTargetFound())
            StartCoroutine(AttackCooldown());
    }
}
