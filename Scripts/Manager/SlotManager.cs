using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlotManager : Singleton<SlotManager>
{
    //这个是所有UI的Slot
    public List<UIItemSlot> itemSlots = new List<UIItemSlot>();
    public UIItemSlot currentItemSlot;
    private void Start()
    {
        //itemSlots.
    }

    public void TestAddFunction()
    {
        byte index = 1;
        foreach (UIItemSlot s in itemSlots) {

            ItemStack stack = new ItemStack((byte)Random.Range(0,3), Random.Range (2, 65));
            ItemSlot slot = new ItemSlot(s, stack);
            index++;

        }
    }
    public void AddItemSlot(UIItemSlot itemSlot)
    {
        itemSlots.Add(itemSlot);
    }

    public  void RemoveItemSlot(UIItemSlot itemSlot)
    {
        itemSlots.Remove(itemSlot);
    }

    public void SelectItemSlot(UIItemSlot itemSlot)
    {
        foreach (UIItemSlot slot in itemSlots)
        {
            slot.HasSelected(false);
        }
        currentItemSlot = itemSlot;
        itemSlot.HasSelected(true);
    }
}