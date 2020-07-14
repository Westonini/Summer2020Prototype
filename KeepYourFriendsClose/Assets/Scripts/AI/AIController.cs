using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIController : MonoBehaviour
{
    public Animator anim;

    [Space]
    public Collider2D hitboxCol;
    public Collider2D movementCol;

    private AIStateManager stateManager;
    private AITargetDetector detectorScript;
    private AIAttack attackScript;

    private AIPath aiPath;
    private AIDestinationSetter aiDestSetter;
    private Patrol aiPatrol;

    private float baseSlowdownDist;
    private float baseEndReachDist;

    [HideInInspector] public bool isDead;

    [Space]
    public Transform followTrans;

    private void Awake()
    {
        stateManager = GetComponent<AIStateManager>();
        aiPath = GetComponent<AIPath>();
        aiDestSetter = GetComponent<AIDestinationSetter>();
        aiPatrol = GetComponent<Patrol>();

        detectorScript = GetComponentInChildren<AITargetDetector>();
        attackScript = GetComponentInChildren<AIAttack>();
    }

    private void Start()
    {
        baseSlowdownDist = aiPath.slowdownDistance;
        baseEndReachDist = aiPath.endReachedDistance;

        stateManager.SetState(AIStateManager.State.Idle);
        Idle();
    }

    public void Idle()
    {
        attackScript.enabled = false;

        if (stateManager.GetMovementType() == AIStateManager.MovementType.Stationary || stateManager.GetMovementType() == AIStateManager.MovementType.AlwaysStationary)
        {
            aiPath.canMove = false;
            aiPatrol.enabled = false;
            aiDestSetter.target = null;
            aiDestSetter.enabled = false;
        }
        else if (stateManager.GetMovementType() == AIStateManager.MovementType.Follower)
        {
            aiPath.endReachedDistance = baseEndReachDist; aiPath.slowdownDistance = baseSlowdownDist;
            aiPath.canMove = true;
            aiPatrol.enabled = false;
            aiDestSetter.target = followTrans;
            aiDestSetter.enabled = true;
        }
        else if (stateManager.GetMovementType() == AIStateManager.MovementType.Roamer)
        {
            aiPath.canMove = true;
            aiPatrol.enabled = true;
            aiDestSetter.target = null;
            aiDestSetter.enabled = false;
        }
    }

    public void Aggro()
    {
        attackScript.enabled = true;
        aiPatrol.enabled = false;

        if (stateManager.GetMovementType() == AIStateManager.MovementType.Stationary || stateManager.GetMovementType() == AIStateManager.MovementType.Roamer)
        {
            aiPath.canMove = true;
            aiDestSetter.target = detectorScript.GetTarget();
            aiDestSetter.enabled = true;
        }
        else if (stateManager.GetMovementType() == AIStateManager.MovementType.AlwaysStationary)
        {
            //Continue to stay stationary
        }
        else if (stateManager.GetMovementType() == AIStateManager.MovementType.Follower)
        {
            if (stateManager.GetAttackType() == AIStateManager.AttackType.Melee)
            {
                aiDestSetter.target = detectorScript.GetTarget();
                aiPath.endReachedDistance = 0.3f; aiPath.slowdownDistance = 0.35f;
            }
        }
    }

    public void Dead()
    {
        attackScript.enabled = false;

        isDead = true;
        ToggleColliders(false);

        aiPath.canMove = false;
        aiDestSetter.target = null;
        aiDestSetter.enabled = false;
        aiPatrol.enabled = false;
    }

    private void ToggleColliders(bool option)
    {
        if (option == true)
        {
            hitboxCol.enabled = true;
            movementCol.enabled = true;
        }
        else
        {
            hitboxCol.enabled = false;
            movementCol.enabled = false;
        }
    }
}
