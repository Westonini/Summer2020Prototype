using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVertWeaponSwap : MonoBehaviour
{
    public Sprite weapFront;
    public Sprite weapBack;
    public SpriteRenderer itemSR;
    public AIRender render;

    void OnEnable()
    {
        render._charFlippedVert += ChangeSprite;
    }

    void OnDisable()
    {
        render._charFlippedVert -= ChangeSprite;
    }

    private void ChangeSprite()
    {
        if (itemSR.sprite == weapFront)
            itemSR.sprite = weapBack;
        else
            itemSR.sprite = weapFront;
    }
}
