using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventorySystem : inventorySystem
{
    //selection
    [SerializeField] RectTransform[] HotbarPositionTransform;
    [SerializeField] RectTransform selectionTransform;
    [SerializeField] Vector3 HotbarSelectionOffset;

    //universal objects
    //the hotbar in the inventory takes up index: 0 to this number
    int hotBarEndIndex = 4;
    //selectedItem
    int selectionIndex;
    //the item being held by the mouse;
    itemStack MouseInventory;

    //attached ui script
    UIscript playerUI;

    private void Start()
    {
        selectionIndex = 0;
        playerUI = this.GetComponent<UIscript>();
    }

    public item getHoldingItem()
    {
        return itemList[selectionIndex].getItem();
    }

    public bool IntroduceToInventory(itemStack newItem)
    {
        int leftOver = PriorityAdd(newItem, 0);
        updateHotBarUI();
        if (leftOver == -1)
        {
            return true;
        }
        newItem.setCount(leftOver);
        return PriorityIntroduce(newItem);
    }

    public int PriorityAdd(itemStack newItem, int location)
    {
        //find the next location
        int newLocation = FindEmptyStackLocation(newItem.getItem(), location);

        if (newLocation == -1)
        {
            //return leftOver amount if no more empty spaces to look for
            return newItem.getCount();
        }
        else
        {
            //merge with that stack
            int leftOver = itemList[newLocation].MergeStack(newItem.getCount());
            //return if no leftOver
            if (leftOver == -1)
            {
                return - 1;
            }
            //keep iterating if leftover
            else
            {
                return PriorityAdd(new itemStack(newItem.getItem(), leftOver), newLocation + 1);
            }
        }
    }

    public bool PriorityIntroduce(itemStack newItem)
    {
        //prioritise selected hotbar location
        if (getInventory(selectionIndex) == null)
        {
            introduce(newItem, selectionIndex);
            updateHotBarUI();
            //return true to signify item placed
            return true;
        }
        //if hotbar location full, prioritise based on index
        else
        {
            int max = getSize();
            for (int j = 0; j <= max; j++)
            {
                if (getInventory(j) == null)
                {
                    introduce(newItem, j);
                    updateHotBarUI();
                    return true;
                }
            }
            //return false, item failed to introduce to inventory;
            return false;

        }
    }

    int FindEmptyStackLocation(item Item)
    {
        int count = itemList.Count;
        for(int i = 0; i < count; i++)
        {
            if(itemList[i].getItem().Equals(Item) && !itemList[i].isFull())
            {
                return i;
            }
        }

        return -1;
    }

    int FindEmptyStackLocation(item Item, int start)
    {
        int count = itemList.Count;
        for (int i = start; i < count; i++)
        {
            if (itemList[i] != null && itemList[i].getItem().Equals(Item) && !itemList[i].isFull())
            {
                return i;
            }
        }

        return -1;
    }

    public void incrementSelection()
    {
        selectionIndex = ++selectionIndex % (hotBarEndIndex + 1);
        updateHotBarSelection();
    }

    public void decrementSelection()
    {
        selectionIndex = (--selectionIndex + hotBarEndIndex + 1) % (hotBarEndIndex + 1);
        updateHotBarSelection();
    }

    public void SwitchSelection(int newindex)
    {
        selectionIndex = newindex % (hotBarEndIndex + 1);
        updateHotBarSelection();
    }

    public void updateHotBarSelection()
    {
        selectionTransform.position = HotbarPositionTransform[selectionIndex].position + HotbarSelectionOffset;
    }

    public void updateHotBarUI()
    {
        //get item list
        itemStack[] hotbarArray = new itemStack[hotBarEndIndex + 1];
        for(int i = 0; i <= hotBarEndIndex; i++)
        {
            hotbarArray[i] = getInventory(i);
        }
        //update ui
        playerUI.UpdateHotbarUI(hotbarArray);
    }

    public void spawnUI()
    {
        playerUI.showPlayerInventory();
    }

    public void despawnUI()
    {
        playerUI.hidePlayerInventory();
    }
}
