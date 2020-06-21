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

    public override void ItemLeftClick()
    {
        //Melee logic
        Debug.Log("Melee Attack");
    }
}
