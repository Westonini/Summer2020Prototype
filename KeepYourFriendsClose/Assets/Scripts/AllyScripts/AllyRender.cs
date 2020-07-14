using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyRender : AIRender
{
    public HumanCharacters characters;
    public bool oneArm1;
    public bool oneArm2;
    public bool oneArmUp1;
    public bool oneArmUp2;
    public bool twoArms;

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

        if (oneArm1)
            SetSprites(currentCharacter.frontOneArm1, currentCharacter.backOneArm1);
        else if (oneArm2)
            SetSprites(currentCharacter.frontOneArm2, currentCharacter.backOneArm2);
        else if (oneArmUp1)
            SetSprites(currentCharacter.frontOneArmUp1, currentCharacter.backOneArmUp1);
        else if (oneArmUp2)
            SetSprites(currentCharacter.frontOneArmUp2, currentCharacter.backOneArmUp2);
        else if (twoArms)
            SetSprites(currentCharacter.frontTwoArms, currentCharacter.backTwoArms);
        else
            SetSprites(currentCharacter.frontIdle, currentCharacter.backIdle);

        sr.sprite = GetFrontSprite();
    }
    public HumanCharacters.SpriteList GetCurrentCharacterSprites()
    {
        return currentCharacter;
    }
}
