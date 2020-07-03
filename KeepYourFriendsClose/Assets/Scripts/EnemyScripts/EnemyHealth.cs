using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : CharacterHealth
{
    private EnemyStateManager ESM;
    public Transform inactiveEnemies;

    protected override void Awake()
    {
        base.Awake();
        ESM = GetComponentInParent<EnemyStateManager>();
    }

    protected override void CharacterDeath()
    {
        ESM.SetState(EnemyStateManager.State.Dead);
        transform.parent.SetParent(inactiveEnemies);
        transform.parent.gameObject.SetActive(false);
    }
}
