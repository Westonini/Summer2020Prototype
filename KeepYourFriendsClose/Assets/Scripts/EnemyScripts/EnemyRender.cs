using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyRender : CharacterRender
{
    public EnemyCharacters characters;
    public AIPath aiPath;
    private Animator anim;
    private EnemyController EC;

    private EnemyCharacters.SpriteList currentCharacter;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        EC = GetComponentInParent<EnemyController>();
    }

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

        if ((aiPath.desiredVelocity.x != 0 || aiPath.desiredVelocity.y != 0) && !EC.isDead)
            anim.SetBool("Walking", true);
        else
            anim.SetBool("Walking", false);
    }

    public EnemyCharacters.SpriteList GetCurrentCharacterSprites()
    {
        return currentCharacter;
    }
}
