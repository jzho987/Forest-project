using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventorySystem : inventorySystem
{
    //selection
    [SerializeField] RectTransform[] HotbarPositionTransform;
    [SerializeField] RectTransform selectionTransform;

    //prefabs
    [SerializeField] GameObject uiPrefab;
    [SerializeField] GameObject CanvasPointer;
    [SerializeField] GameObject HotBarUI;

    //universal objects
    //the hotbar in the inventory takes up index: 0 to this number
    int hotBarEndIndex = 4;
    //selectedItem
    int selectionIndex;
    //the item being held by the mouse;
    GameObject MouseInventory;
    //uiElement
    GameObject InventoryUI;

    private void Start()
    {
        selectionIndex = 0;
    }

    public bool PriorityIntroduce(itemStack newItem, int i)
    {
        Debug.Log(selectionIndex);
        //prioritise selected hotbar location
        if (getInventory(selectionIndex) == null)
        {
            introduce(newItem, selectionIndex);
            updateHotBarUI();
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

    public void spawnUI()
    {
        InventoryUI = Instantiate(uiPrefab, CanvasPointer.transform);
    }

    public void killUI()
    {
        Destroy(InventoryUI);
    }

    public void updateHotBarSelection()
    {
        selectionTransform.position = HotbarPositionTransform[selectionIndex].position;
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
        HotBarUI.GetComponent<UIscript>().UpdateUI(hotbarArray);
    }
}
