using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AllyFlashlight : MonoBehaviour
{
    public AIPath aiPath;
    public AITargetDetector detector;
    private Animator flashlightAnim;
    private Vector3 startingPos;

    private void OnEnable()
    {
        detector._targetDetected += ToggleFocus;
        detector._targetKilled += ToggleFocus;
    }
    private void OnDisable()
    {
        detector._targetDetected -= ToggleFocus;
        detector._targetKilled -= ToggleFocus;
    }

    private void Awake()
    {
        flashlightAnim = GetComponentInChildren<Animator>();    
    }

    private void Start()
    {
        startingPos = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50));
        transform.up = (startingPos - transform.position);

        flashlightAnim.SetBool("Patrol", true);
        flashlightAnim.Play("FlashlightPatrol", -1, Random.Range(0f, 1f));
    }

    void Update()
    {
        if (detector.IsTargetFound())
            transform.up = (detector.GetTarget().transform.position - transform.position).normalized;
        else if (aiPath.desiredVelocity.x != 0 && aiPath.desiredVelocity.y != 0)
            transform.up = (aiPath.destination - transform.position);
    }

    void ToggleFocus()
    {
        if (flashlightAnim.GetBool("Patrol"))
        {
            flashlightAnim.SetBool("Patrol", false);
        }
        else
        {
            flashlightAnim.SetBool("Patrol", true);
            flashlightAnim.Play("FlashlightPatrol", -1, Random.Range(0f, 1f));
        }
    }
}
