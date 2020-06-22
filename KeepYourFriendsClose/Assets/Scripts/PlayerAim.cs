using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Camera mainCam;
    private AimQuadrant currentQuadrant;

    public enum AimQuadrant
    {
        TopLeft,
        BottomLeft,
        BottomRight,
        TopRight
    }

    void Awake()
    {
        mainCam = Camera.main;    
    }

    public AimQuadrant GetQuadrant()
    {
        return currentQuadrant;
    }

    void Update()
    {
        Vector2 mouseScreenPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;
        transform.up = direction;

        if (transform.rotation.eulerAngles.z > 0 && transform.rotation.eulerAngles.z <= 89.5f)
            currentQuadrant = AimQuadrant.TopLeft;
        if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z <= 179.5f)
            currentQuadrant = AimQuadrant.BottomLeft;
        if (transform.rotation.eulerAngles.z > 180 && transform.rotation.eulerAngles.z <= 269.5f)
            currentQuadrant = AimQuadrant.BottomRight;
        if (transform.rotation.eulerAngles.z > 270 && transform.rotation.eulerAngles.z <= 359.5f)
            currentQuadrant = AimQuadrant.TopRight;

        //NOTE: The values are set at <= 89.5, 179.5, 269.5, and 359.5 instead of 90, 180, 270, and 360 because the mouse shakes and may cause the sprite to spasm if the mouse is right between two quadrants.
    }
}
