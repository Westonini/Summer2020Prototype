using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected PlayerRender PR;
    protected SpriteRenderer itemSR;
    private string itemName;
    private bool canBeReloaded;

    protected virtual void Awake()
    {
        itemSR = gameObject.GetComponent<SpriteRenderer>();
        PR = gameObject.GetComponentInParent<PlayerRender>();
    }

    protected virtual void SetItemName(string name)
    {
        itemName = name;
    }

    protected virtual void SetReloadability(bool reloadable)
    {
        canBeReloaded = reloadable;
    }

    protected string GetItemName()
    {
        return itemName;
    }

    public virtual void EquipItem()
    {
        itemSR.enabled = true;
    }

    public virtual void UnequipItem()
    {
        itemSR.enabled = false;
    }

    public virtual void ItemLeftClick()
    {
        //Do the item's left click ability
    }

    public virtual bool IsItemReloadable()
    {
        //May need to override in child class
        return false;
    }

    public virtual void Reload()
    {
        //Reload item if possible
    }
}
