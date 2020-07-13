using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyRender : AIRender
{
    public HumanCharacters characters;

    private HumanCharacters.SpriteList currentCharacter;

    public int characterSelect = -1;
    private int randomInt;

    void Start()
    {
        if (characterSelect == -1)
        {
            randomInt = Random.Range(0, characters.humanCharacters.Length);
            currentCharacter = characters.humanCharacters[randomInt];
        }
        else
            currentCharacter = characters.humanCharacters[characterSelect];

        SetSprites(currentCharacter.frontIdle, currentCharacter.backIdle);
        sr.sprite = GetFrontSprite();
    }
    public HumanCharacters.SpriteList GetCurrentCharacterSprites()
    {
        return currentCharacter;
    }
}
