﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Item
{
    protected override void Awake()
    {
        base.Awake();
        base.SetItemName("Pistol");
        base.SetReloadability(true);
    }

    public override void ItemLeftClick()
    {
        //Shoot logic
        Debug.Log("Shoot");
    }

    public override bool IsItemReloadable()
    {
        return true;
    }

    public override void Reload()
    {
        //Reload logic
        Debug.Log("Reload");
    }
}
