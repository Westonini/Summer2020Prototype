using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSwing : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyHealth EH = collision.gameObject.GetComponent<EnemyHealth>();
            EH.TakeDamage(damage);
        }
    }
}
