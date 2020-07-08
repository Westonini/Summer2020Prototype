using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSpriteOnCharFlip : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        if (player.transform.localRotation == Quaternion.Euler(0, -180, 0))
        {
            transform.localScale = new Vector3(-1.25f, 1.25f, 1.25f);
        }
        else
            transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
    }
}
