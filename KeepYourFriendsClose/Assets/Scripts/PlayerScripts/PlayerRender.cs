using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRender : CharacterRender
{
    public int chosenChar;

    public bool facingRight = true;
    public bool facingDown = true;

    public Transform characterTrans;
    public PlayerMovement PM;
    public HumanCharacters characters;

    private HumanCharacters.SpriteList currentCharacter;

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

    private void Start()
    {
        currentCharacter = characters.humanCharacters[chosenChar];
        SetSprites(currentCharacter.frontIdle, currentCharacter.backIdle);
    }

    void Update()
    {
        //SPRITE CHANGES
        if (facingDown)
            sr.sprite = GetFrontSprite();
        else
            sr.sprite = GetBackSprite();
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

    public HumanCharacters.SpriteList GetCurrentCharacterSprites()
    {
        return currentCharacter;
    }
}
