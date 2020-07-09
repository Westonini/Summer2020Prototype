using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void DoScreenShake(int intensity)
    {
        if (intensity == 1)
            anim.SetTrigger("Shake01");
        else if (intensity == 2)
            anim.SetTrigger("Shake02");
    }
}
