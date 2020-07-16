using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item item1;
    public Item item2;
    public Item item3;

    private int itemEquippedSlot;
    private Item itemEquipped;

    private void Start()
    {
        Equip(1);
    }

    void Update()
    {
        //Item Select
        if (Input.GetButtonDown("Item1"))
        {
            Equip(1);
        }
        else if (Input.GetButtonDown("Item2"))
        {
            Equip(2);
        }
        else if (Input.GetButtonDown("Item3"))
        {
            Equip(3);
        }

        //Item Left Click
        if (Input.GetButtonDown("Fire1"))
        {
            itemEquipped.ItemLeftClick();
        }


        //Item Reload
        if (Input.GetButtonDown("Reload") && itemEquipped.IsItemReloadable())
        {
            itemEquipped.Reload();
        }
    }

    void Equip(int item)
    {
        if (item == 1 && itemEquipped != item1)
        {
            itemEquippedSlot = 1;
            itemEquipped = item1;
            item2.UnequipItem();
            item3.UnequipItem();
            item1.EquipItem();
        }
        else if (item == 2 && itemEquipped != item2)
        {
            itemEquippedSlot = 2;
            itemEquipped = item2;
            item1.UnequipItem();
            item3.UnequipItem();
            item2.EquipItem();
        }
        else if (item == 3 && itemEquipped != item3)
        {
            itemEquippedSlot = 3;
            itemEquipped = item3;
            item1.UnequipItem();
            item2.UnequipItem();
            item3.EquipItem();
        }
    }
}
