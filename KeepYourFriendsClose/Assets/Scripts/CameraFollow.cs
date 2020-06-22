﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTrans;

    void LateUpdate()
    {
        transform.position = new Vector3(playerTrans.position.x, playerTrans.position.y, -13);
    }
}
