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

    private Transform shooter;

    private bool wallColDisabled;

    void Awake()
    {
        mainCam = Camera.main;
    }

    void Start()
    {
        StartCoroutine(TurnOnWallCollision());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && currentPenetrateCount < maxPenetrateCount)
        {
            AIHealth healthScript = collision.gameObject.GetComponent<AIHealth>();
            healthScript.TakeDamage(damage);

            AIStateManager stateManager = collision.gameObject.GetComponentInParent<AIStateManager>();

            if (stateManager.GetState() == AIStateManager.State.Idle)
            {
                AITargetDetector detectorScript = collision.gameObject.transform.parent.GetComponentInChildren<AITargetDetector>();
                detectorScript.SetTarget(shooter);
            }

            currentPenetrateCount++;

            if (currentPenetrateCount >= maxPenetrateCount)
                Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("NonDestructable") && !wallColDisabled)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("NonDestructable"))
        {
            Destroy(gameObject);
        }
    }

    public void SetShooter(Transform _shooter) { shooter = _shooter; }

    //This IEnumerator is made to ensure that when the projectile first spawns it doesnt immediately get destroyed by a wall
    private IEnumerator TurnOnWallCollision()
    {
        wallColDisabled = true;
        yield return new WaitForSeconds(0.01f);
        wallColDisabled = false;
    }
}
