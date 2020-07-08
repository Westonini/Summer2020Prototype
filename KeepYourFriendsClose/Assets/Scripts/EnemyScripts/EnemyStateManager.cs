using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    private EnemyController EC;

    public enum State
    {
        Idle,
        Aggro,
        Attack,
        Dead
    }

    private State currentState;

    private void Awake()
    {
        EC = GetComponent<EnemyController>();
    }

    public State GetState() { return currentState; }

    public void SetState(State newState)
    {
        currentState = newState;

        if (!EC.isDead)
        {
            if (currentState == State.Idle)
                EC.Idle();
            else if (currentState == State.Aggro)
                EC.Aggro();
            else if (currentState == State.Attack)
                EC.Attack();
        }

        if (currentState == State.Dead)
            EC.Dead();
    }
}
