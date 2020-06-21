using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRender : MonoBehaviour
{
    public int chosenChar;
    private SpriteRenderer sr;

    public bool facingRight = true;
    public bool facingDown = true;

    public Transform characterTrans;
    public PlayerMovement PM;
    public Characters characters;

    private Characters.SpriteList currentCharacter;

    public bool getFacingRight() { return facingRight; }
    public bool getFacingDown() { return facingDown; }

    void OnEnable()
    {
        PlayerListener._playerFlippedHoriz += FlipHoriz;
        PlayerListener._playerFlippedVert += FlipVert;
    }

    void OnDisable()
    {
        PlayerListener._playerFlippedHoriz -= FlipHoriz;
        PlayerListener._playerFlippedVert -= FlipVert;
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentCharacter = characters.playerCharacters[chosenChar];
    }

    void Update()
    {
        //SPRITE CHANGES
        if (facingDown)
            sr.sprite = currentCharacter.frontIdle;
        else
            sr.sprite = currentCharacter.backIdle;
    }

    void FlipHoriz()
    {
        //Flip function used if the player looks the other direction (horizontally).
        facingRight = !facingRight;

        //Flip the player
        characterTrans.Rotate(0f, 180f, 0f);
    }

    void FlipVert()
    {
        //Flip function used if the player looks the other direction (vertically).
        facingDown = !facingDown;
    }
}
