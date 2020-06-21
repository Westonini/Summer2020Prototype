using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Item
{
    protected override void Awake()
    {
        base.Awake();
        base.SetItemName("Knife");
    }

    public override void EquipItem()
    {
        base.EquipItem();
        PR.SetSprites(PR.GetCurrentCharacterSprites().frontOneArm1, PR.GetCurrentCharacterSprites().backOneArm1);
    }

    public override void UnequipItem()
    {
        base.UnequipItem();
        PR.SetSprites(PR.GetCurrentCharacterSprites().frontIdle, PR.GetCurrentCharacterSprites().backIdle);
    }

    public override void ItemLeftClick()
    {
        //Melee logic
        Debug.Log("Melee Attack");
    }
}
