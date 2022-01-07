using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This is to process UI updates and display ONLY
 * 
 * All UI functionality calls should be made in the respective inventory system
 * 
 */
[RequireComponent(typeof(PlayerInventorySystem))]
public class UIscript : MonoBehaviour
{
    [SerializeField] initializeCharacter initialize;

    characterEvent events;

    inventorySlot[] hotbarSlots;
    inventorySlot[] inventorySlots;
    [SerializeField] inventorySlot mouseSlot;

    [SerializeField] GameObject hotbarSlotsPointer;
    [SerializeField] GameObject inventorySlotsPointer;
    [SerializeField] GameObject inventoryPointer;
    [SerializeField] GameObject craftingTabPointer;

    //hotbar selection pointers
    [SerializeField] RectTransform[] HotbarPositionTransform;
    [SerializeField] RectTransform selectionTransform;
    [SerializeField] Vector3 HotbarSelectionOffset;

    /**
     * initialize inventory display pointers
     * 
     * this is all obtained from the children of the slot pointers
     */
    private void Start()
    {
        hotbarSlots = new inventorySlot[5];
        inventorySlots = new inventorySlot[15];
        int i = 0;
        foreach(Transform hotbarTransform in hotbarSlotsPointer.transform)
        {
            hotbarSlots[i++] = hotbarTransform.GetComponent<inventorySlot>();
        }

        i = 0;
        foreach (Transform inventorySlotTransform in inventorySlotsPointer.transform)
        {
            inventorySlots[i++] = inventorySlotTransform.GetComponent<inventorySlot>();
        }

        //subscribe to initialize
        initialize.onInitializeCharacter += initializeCharacterCallback;
    }

    private void initializeCharacterCallback(characterEvent events, CharacterStatistics stats)
    {
        Debug.Log("initialized hehe 1");
        this.events = events;

        //subscribe hotbar UI update to selection change event
        events.onHotbarSelectionChanged += updateHotBarSelection;
        events.onUpdatedHotbarContent += UpdateHotbarUI;
        events.onUpdatedInventorySlotsContent += UpdateInventorySlotsUI;
        events.onInventoryStackPickedUpByMouse += updateMouseSlotUI;

        //triggered from controller directly
        events.onInventoryOpen += showPlayerInventory;
        events.onCraftingTabOpen += showCraftingTab;
        events.OnExitUIMenus += hidePlayerInventory;
        events.OnExitUIMenus += hideCraftingTab;
    }

    //update hotbar selection UI
    public void updateHotBarSelection(int newPos)
    {
        selectionTransform.position = HotbarPositionTransform[newPos].position + HotbarSelectionOffset;
    }

    /**
     * This updates the whole hotbar UI
     * This makes call to the individual inventory slot and makes update call;
     * 
     */
    public void UpdateHotbarUI(itemStack[] slotItems)
    {
        int size = hotbarSlots.Length;
        for(int i = 0; i < size; i++)
        {
            if (!(slotItems[i] == null))
            {
                hotbarSlots[i].updateDisplay(slotItems[i].getItem().getItemSprite(), slotItems[i].getCount());
            }
            else
            {
                hotbarSlots[i].setEmpty();
            }
        }
    }

    //only update the slots from toUpdate array
    public void UpdateHotbarUI(itemStack[] slotItems, int[] toUpdate)
    {
        foreach(int slotNum in toUpdate) {
            if (!(slotItems[slotNum] == null))
            {
                hotbarSlots[slotNum].updateDisplay(slotItems[slotNum].getItem().getItemSprite(), slotItems[slotNum].getCount());
            }
            else
            {
                hotbarSlots[slotNum].setEmpty();
            }
        }
    }

    /**
     * This updates the whole inventory UI
     * This makes call to the individual inventory slot and makes update call;
     * 
     */
    public void UpdateInventorySlotsUI(itemStack[] slotItems)
    {
        int size = inventorySlots.Length;
        for (int i = 0; i < size; i++)
        {
            if (!(slotItems[i] == null))
            {
                inventorySlots[i].updateDisplay(slotItems[i].getItem().getItemSprite(), slotItems[i].getCount());
            }
            else
            {
                inventorySlots[i].setEmpty();
            }
        }
    }

    //only update the slots from toUpdate array
    public void UpdateInventorySlotsUI(itemStack[] slotItems, int[] toUpdate)
    {
        foreach (int slotNum in toUpdate)
        {
            if (!(slotItems[slotNum] == null))
            {
                inventorySlots[slotNum].updateDisplay(slotItems[slotNum].getItem().getItemSprite(), slotItems[slotNum].getCount());
            }
            else
            {
                inventorySlots[slotNum].setEmpty();
            }
        }
    }

    public void updateMouseSlotUI(itemStack stackItem, itemStack mouseItem, int effectedSlotIndex)
    {

        if (stackItem != null)
        {
            //update the effected slot
            inventorySlots[effectedSlotIndex].updateDisplay(stackItem.getItem().getItemSprite(), stackItem.getCount());
        }
        else
        {
            inventorySlots[effectedSlotIndex].setEmpty();
        }

        if (mouseItem != null)
        {
            Debug.Log("NOT NULL");
            //update the mouse
            mouseSlot.updateDisplay(mouseItem.getItem().getItemSprite(), mouseItem.getCount());
        }
        else
        {
            Debug.Log("NULL");
            mouseSlot.setEmpty();
        }

    }

    /**
     * These 2 functions controls opening and closing the inventory
     */
    public void showPlayerInventory()
    {
        inventoryPointer.SetActive(true);
    }
    public void hidePlayerInventory()
    {
        inventoryPointer.SetActive(false);
    }

    /**
     * These 2 functions controls opening and closing the crafting tab
     */
    public void showCraftingTab()
    {
        craftingTabPointer.SetActive(true);
    }
    public void hideCraftingTab()
    {
        craftingTabPointer.SetActive(false);
    }
}
