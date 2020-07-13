using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyRender : AIRender
{
    public EnemyCharacters characters;

    private EnemyCharacters.SpriteList currentCharacter;

    void Start()
    {
        int randomInt = Random.Range(0, characters.enemyCharacters.Length);

        currentCharacter = characters.enemyCharacters[randomInt];
        SetSprites(currentCharacter.frontIdle, currentCharacter.backIdle);
        sr.sprite = GetFrontSprite();
    }
    public EnemyCharacters.SpriteList GetCurrentCharacterSprites()
    {
        return currentCharacter;
    }
}
