using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRender : MonoBehaviour
{
    protected SpriteRenderer sr;
    private Sprite currentFront;
    private Sprite currentBack;

    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetSprites(Sprite newFront, Sprite newBack)
    {
        currentFront = newFront;
        currentBack = newBack;
    }

    public Sprite GetFrontSprite()
    {
        return currentFront;
    }
    public Sprite GetBackSprite()
    {
        return currentBack;
    }
}
