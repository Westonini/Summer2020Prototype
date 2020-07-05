using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyRender : CharacterRender
{
    public EnemyCharacters characters;
    public AIPath aiPath;

    private EnemyCharacters.SpriteList currentCharacter;

    void Start()
    {
        int randomInt = Random.Range(0, characters.enemyCharacters.Length);

        currentCharacter = characters.enemyCharacters[randomInt];
        SetSprites(currentCharacter.frontIdle, currentCharacter.backIdle);
        sr.sprite = GetFrontSprite();
    }

    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
            transform.parent.transform.localScale = new Vector3(1f, 1f, 1f);
        else if (aiPath.desiredVelocity.x <= -0.01f)
            transform.parent.transform.localScale = new Vector3(-1f, 1f, 1f);

        if (aiPath.desiredVelocity.y >= 0.01f)
            sr.sprite = GetBackSprite();
        else if (aiPath.desiredVelocity.y <= 0.01f)
            sr.sprite = GetFrontSprite();
    }

    public EnemyCharacters.SpriteList GetCurrentCharacterSprites()
    {
        return currentCharacter;
    }
}
