using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIRender : CharacterRender
{
    public AIPath aiPath;
    protected Animator anim;
    protected AIController controller;

    public delegate void CharFlippedVert();
    public event CharFlippedVert _charFlippedVert;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        controller = GetComponentInParent<AIController>();
    }

    void Update()
    {
        //Horizontal Flip
        if (aiPath.desiredVelocity.x >= 0.01f)
            transform.parent.transform.localScale = new Vector3(1f, 1f, 1f);
        else if (aiPath.desiredVelocity.x <= -0.01f)
            transform.parent.transform.localScale = new Vector3(-1f, 1f, 1f);

        //Vertical Flip
        if (aiPath.desiredVelocity.y >= 0.01f && sr.sprite == GetFrontSprite())
        {
            if (_charFlippedVert != null) { _charFlippedVert(); }
            sr.sprite = GetBackSprite();
        }
        else if (aiPath.desiredVelocity.y <= 0.01f && sr.sprite == GetBackSprite())
        {
            if (_charFlippedVert != null) { _charFlippedVert(); }
            sr.sprite = GetFrontSprite();
        }

        //Movement
        if ((aiPath.desiredVelocity.x != 0 || aiPath.desiredVelocity.y != 0) && !controller.isDead)
            anim.SetBool("Walking", true);
        else
            anim.SetBool("Walking", false);
    }
}
