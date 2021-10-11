using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(inventorySystem))]
public class UIscript : MonoBehaviour
{
    [SerializeField] inventorySlot[] hotbarSlots;
    [SerializeField] inventorySlot[] inventorySlots;

    [SerializeField] GameObject hotbarSlotsPointer;
    [SerializeField] GameObject inventorySlotsPointer;
    [SerializeField] GameObject inventoryPointer;

    private void Start()
    {
        hotbarSlots = new inventorySlot[5];
        inventorySlots = new inventorySlot[20];
        int i = 0;
        foreach(Transform hotbarTransform in hotbarSlotsPointer.transform)
        {
            hotbarSlots[i++] = hotbarTransform.GetComponent<inventorySlot>();
        }

        i = 0;
        foreach (Transform hotbarTransform in inventorySlotsPointer.transform)
        {
            inventorySlots[i++] = hotbarTransform.GetComponent<inventorySlot>();
        }
    }

    public void UpdateHotbarUI(itemStack[] slotItems)
    {
        int size = hotbarSlots.Length;
        for(int i = 0; i < size; i++)
        {
            if (!(slotItems[i] == null))
            {
                hotbarSlots[i].updateDisplay(slotItems[i].getItem().getItemSprite(), slotItems[i].getCount());
            }
        }
    }

    public void UpdateInventorySlotUI(itemStack[] slotItems)
    {
        int size = inventorySlots.Length;
        for (int i = 0; i < size; i++)
        {
            if (!(slotItems[i] == null))
            {
                inventorySlots[i].updateDisplay(slotItems[i].getItem().getItemSprite(), slotItems[i].getCount());
            }
        }
    }

    public void showPlayerInventory()
    {
        inventoryPointer.SetActive(true);
    }

    public void hidePlayerInventory()
    {
        inventoryPointer.SetActive(false);
    }
}
