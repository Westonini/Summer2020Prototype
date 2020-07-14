using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform trans;

    void LateUpdate()
    {
        transform.position = new Vector3(trans.position.x, trans.position.y, 1);
    }
}
