using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 cursorPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
    }
}
