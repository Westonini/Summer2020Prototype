using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : CharacterHealth
{
    private EnemyStateManager ESM;
    public Transform inactiveEnemies;
    public string[] hurtSounds;

    protected override void Awake()
    {
        base.Awake();
        ESM = GetComponentInParent<EnemyStateManager>();
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        AudioManager.instance.PlayOneShot(hurtSounds);
    }

    protected override void CharacterDeath()
    {
        ESM.SetState(EnemyStateManager.State.Dead);
        transform.parent.SetParent(inactiveEnemies);
        transform.parent.gameObject.SetActive(false);
    }
}
