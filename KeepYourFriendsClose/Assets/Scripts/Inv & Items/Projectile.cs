﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;

    [Range(1, 10)]
    public int maxPenetrateCount = 1;
    private int currentPenetrateCount = 0;

    private Camera mainCam;
    Vector2 mouseScreenPosition;
    Vector2 direction;

    void Awake()
    {
        mainCam = Camera.main;
    }

    void Start()
    {
        mouseScreenPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        direction = (mouseScreenPosition - (Vector2)transform.position).normalized;
        transform.up = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyHealth EH = collision.gameObject.GetComponent<EnemyHealth>();
            EH.TakeDamage(damage);

            currentPenetrateCount++;

            if (currentPenetrateCount >= maxPenetrateCount)
                Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("NonDestructable"))
        {
            Destroy(gameObject);
        }
    }
}
