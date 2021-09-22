﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventorySystem : inventorySystem
{
    [SerializeField] GameObject uiPrefab;
    [SerializeField] GameObject CanvasPointer;

    //the hotbar in the inventory takes up index: 0 to this number
    int hotBarEndIndex = 3;

    //selectedItem
    int selectionIndex;

    //the item being held by the mouse;
    GameObject MouseInventory;

    private void Start()
    {
        selectionIndex = 0;
    }

    protected bool PriorityIntroduce(item newItem, int i)
    {
        //prioritise selected hotbar location
        if(base.getInventory(selectionIndex) == null)
        {
            base.introduce(newItem, selectionIndex);
            return true;
        }
        //if hotbar location full, prioritise based on index
        else
        {
            int max = base.getSize();
            for (int j = 0; j <= max; j++)
            {
                if (base.getInventory(j) == null)
                {
                    base.introduce(newItem, j);
                    return true;
                }
            }
            //return false, item failed to introduce to inventory;
            return false;

        }
    }

    public void incrementSelection()
    {
        selectionIndex = ++selectionIndex % hotBarEndIndex;
    }

    public void decrementSelection()
    {
        selectionIndex = ++selectionIndex % hotBarEndIndex;
    }

    public void SwitchSelection(int newindex)
    {
        selectionIndex = newindex % hotBarEndIndex;
    }

    public void spawnUI()
    {
        GameObject InventoryUI = Instantiate(uiPrefab, CanvasPointer.transform);
    }
}