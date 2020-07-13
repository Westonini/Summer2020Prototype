using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAttack : MonoBehaviour
{
    public int damage;
    public float cooldownDuration;

    public LayerMask targetLayer;
    public AIStateManager stateManager;
    protected bool onCooldown;

    //To be scripted in child classes
    protected virtual void Attack() { }
    protected virtual IEnumerator AttackCooldown() { yield return new WaitForSeconds(cooldownDuration); }
}
