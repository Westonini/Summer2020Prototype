using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private Camera mainCam;
    Vector2 mouseScreenPosition;
    Vector2 direction;

    void Awake()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        mouseScreenPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        direction = (mouseScreenPosition - (Vector2)transform.position).normalized;
        transform.up = direction;
    }
}
