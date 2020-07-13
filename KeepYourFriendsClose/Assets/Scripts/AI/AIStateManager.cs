using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateManager : MonoBehaviour
{
    private AIController controller;

    public enum State
    {
        Idle,
        Aggro,
        Dead
    }

    public enum MovementType
    {
        Stationary,
        AlwaysStationary,
        Follower,
        Roamer
    }

    private State currentState;
    public MovementType movementType;

    private void Awake()
    {
        controller = GetComponent<AIController>();
    }

    public State GetState() { return currentState; }
    public MovementType GetMovementType() { return movementType; }

    public void SetState(State newState)
    {
        if (!controller.isDead)
        {
            currentState = newState;

            if (currentState == State.Idle)
                controller.Idle();
            else if (currentState == State.Aggro)
                controller.Aggro();
        }

        if (newState == State.Dead)
        {
            currentState = newState;
            controller.Dead();
        }
    }
    public void SetMovementType(MovementType newMovementType) { movementType = newMovementType; }
}
