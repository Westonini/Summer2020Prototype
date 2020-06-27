using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Space]
    [Header("Movement Settings:")]
    public float movementSpd;
    private float horizontalInput;
    private float verticalInput;

    public Rigidbody2D rb;

    //GETTERS & SETTERS
    public float getHorizontalInput() { return horizontalInput; }
    public float getVerticalInput() { return verticalInput; }

    void FixedUpdate()
    {
        //MOVEMENT
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(horizontalInput, verticalInput).normalized * movementSpd * Time.deltaTime;
    }
}
