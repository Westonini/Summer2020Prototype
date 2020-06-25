using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : GunItem
{
    public Sprite rifleFront;
    public Sprite rifleBack;

    void OnEnable()
    {
        PlayerListener._playerFlippedVert += ChangeRifleSprite;
    }

    void OnDisable()
    {
        PlayerListener._playerFlippedVert -= ChangeRifleSprite;
    }

    protected override void Awake()
    {
        base.Awake();
        base.SetItemName("Rifle");
        base.SetReloadability(true);
    }

    public override void EquipItem()
    {
        base.EquipItem();
        PR.SetSprites(PR.GetCurrentCharacterSprites().frontTwoArms, PR.GetCurrentCharacterSprites().backTwoArms);
    }

    public override void UnequipItem()
    {
        base.UnequipItem();
        PR.SetSprites(PR.GetCurrentCharacterSprites().frontIdle, PR.GetCurrentCharacterSprites().backIdle);
    }

    private void ChangeRifleSprite()
    {
        if (itemSR.sprite == rifleFront)
            itemSR.sprite = rifleBack;
        else
            itemSR.sprite = rifleFront;
    }
}
