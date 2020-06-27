using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : CharacterHealth
{
    protected override void CharacterDeath()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
