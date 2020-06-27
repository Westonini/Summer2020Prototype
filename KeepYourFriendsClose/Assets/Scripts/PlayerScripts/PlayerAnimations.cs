using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        PlayerListener._playerWalking += PlayWalkAnim;
        PlayerListener._playerStopWalking += StopWalkAnim;
    }

    void OnDisable()
    {
        PlayerListener._playerWalking -= PlayWalkAnim;
        PlayerListener._playerStopWalking -= StopWalkAnim;
    }

    public void PlayWalkAnim()
    {
        anim.SetBool("Walking", true);
        //AudioManager.instance.Play("StompWalk");
    }

    public void StopWalkAnim()
    {
        anim.SetBool("Walking", false);
        //AudioManager.instance.Stop("StompWalk");
    }
}
