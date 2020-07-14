using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIMeleeAttack : AIAttack
{
    [Space]
    public AIPath aiPath;
    public AITargetDetector detectorScript;
    private float baseSpeed;
    private CharacterHealth characterHealth;
    private bool withinRange;

    public delegate void Attacked();
    public event Attacked _attacked;

    private void Awake()
    {
        baseSpeed = aiPath.maxSpeed;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetLayer == (targetLayer | 1 << collision.gameObject.layer) && stateManager.GetState() != AIStateManager.State.Dead && !onCooldown)
        {
            characterHealth = collision.gameObject.GetComponent<CharacterHealth>();
            withinRange = true;
            Attack();
        }
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (targetLayer == (targetLayer | 1 << collision.gameObject.layer) && stateManager.GetState() != AIStateManager.State.Dead && !onCooldown && !withinRange)
        {
            characterHealth = collision.gameObject.GetComponent<CharacterHealth>();
            withinRange = true;
            Attack();
        }
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (targetLayer == (targetLayer | 1 << collision.gameObject.layer))
        {
            withinRange = false;
            characterHealth = null;
        }
    }

    protected override void Attack()
    {
        StartCoroutine(AttackCooldown());
    }

    protected override IEnumerator AttackCooldown()
    {
        if (_attacked != null) { _attacked(); }
        onCooldown = true;
        aiPath.maxSpeed = 0.5f;
        characterHealth.TakeDamage(damage);
        yield return new WaitForSeconds(cooldownDuration);
        onCooldown = false;
        aiPath.maxSpeed = baseSpeed;

        if (withinRange && stateManager.GetState() != AIStateManager.State.Dead && characterHealth.GetHealth() != 0)
            StartCoroutine(AttackCooldown());
    }
}
