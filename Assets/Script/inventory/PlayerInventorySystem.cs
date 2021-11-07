using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class processes player's inventory and interactions
 * 
 * certain changes will make event calls to UI script
 * 
 * The calls are made here because this processes input given to it and determines which ones are valid, which then are used to make event calls
 * 
 * author: JZ
 */
public class PlayerInventorySystem : MonoBehaviour
{

    [SerializeField] initializeCharacter initialize;

    //event
    characterEvent events;
    CharacterStatistics stats;
    public Inventory playerInventory;

    //universal objects
    //the hotbar in the inventory takes up index: 0 to this number
    int hotBarEndIndex = 4;
    //selectedItem
    int selectionIndex;
    //the item being held by the mouse;
    itemStack MouseInventory;

    void Start()
    {
        //initiate
        selectionIndex = 0;
        MouseInventory = null;

        initialize.onInitializeCharacter += initializeCharacterCallback;
    }

    private void initializeCharacterCallback(characterEvent events, CharacterStatistics stats)
    {
        Debug.Log("initialized hehe");
        this.events = events;
        this.stats = stats;
        this.playerInventory = stats.playerInventory;

        //subscribe inventory controller to events
        events.onSlotLeftClicked += slotLeftClicked;
        events.onSlotRightClicked += slotRightClicked;
        events.OnExitUIMenus += relaxMouseInventory;
        events.onMouseScrollUp += incrementSelection;
        events.onMouseScrollDown += decrementSelection;
        events.onInventoryOpen += updateInventoryUI;
        events.OnExitUIMenus += updateHotBarUI;
    }

    public item getHoldingItem()
    {
        if (playerInventory.getInventory(selectionIndex) != null)
            return playerInventory.getInventory(selectionIndex).getItem();
        else
            return null;
    }

    /**
     * triggered when exiting the inventory
     * this will introduce the mouse inventory to the inventory
     * using standard inventory introduction method
     */
    public void relaxMouseInventory()
    {
        if (MouseInventory != null)
        {
            IntroduceToInventory(MouseInventory);
            MouseInventory = null;
        }
    }

    /**
     * return true if there are enough items in the character's inventory to be taken
     * then takes the items away
     * 
     * return false if there are not enough items in the character's inventory
     */
    public bool checkItems(item type, int count)
    {
        int countTracker = count;
        //search inventory
        itemStack[] stackList = playerInventory.getInventory().ToArray();
        foreach(itemStack stack in stackList)
        {
            if(stack != null && stack.getItem().Equals(type))
            {
                countTracker -= stack.getCount();
                if(countTracker <= 0)
                {
                    return true;
                }
            }
        }
        //if it reach this point it means dont have enough
        return false;
    }

    /**
     * can only be used to remove a certain amount of a type of item, when there are enough of that type of item
     */
    public void removeItemWhenEnough(item type, int count) {
        itemStack[] stackList = playerInventory.getInventory().ToArray();
        for (int i = 0; i < stackList.Length; i++)
        {
            if (stackList[i] != null && stackList[i].getItem().Equals(type))
            {
                int lastCount = count;
                count -= stackList[i].getCount();
                if(count <= 0)
                {
                    //set this stack to have count of leftover
                    playerInventory.getInventory(i).setCount(stackList[i].getCount() - lastCount);
                }
                else
                {
                    //this means the stack is drained empty
                    playerInventory.introduce(null,i);
                }
            }
        }
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
            int leftOver = playerInventory.getInventory(newLocation).MergeStack(newItem.getCount());
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
        if (playerInventory.getInventory(selectionIndex) == null)
        {
            playerInventory.introduce(newItem, selectionIndex);
            updateHotBarUI();
            //return true to signify item placed
            return true;
        }
        //if hotbar location full, prioritise based on index
        else
        {
            int max = playerInventory.getSize();
            for (int j = 0; j <= max; j++)
            {
                if (playerInventory.getInventory(j) == null)
                {
                    playerInventory.introduce(newItem, j);
                    updateHotBarUI();
                    return true;
                }
            }
            //return false, item failed to introduce to inventory;
            return false;

        }
    }

    int FindEmptyStackLocation(item Item, int start)
    {
        int count = playerInventory.getSize();
        for (int i = start; i < count; i++)
        {
            if (playerInventory.getInventory(i) != null && playerInventory.getInventory(i).getItem().Equals(Item) && !playerInventory.getInventory(i).isFull())
            {
                return i;
            }
        }

        return -1;
    }

    public void incrementSelection()
    {
        selectionIndex = ++selectionIndex % (hotBarEndIndex + 1);

        //this will make a call to update hotbar selection from the UIscript
        events.callOnHotbarSelectionChanged(selectionIndex);
    }

    public void decrementSelection()
    {
        selectionIndex = (--selectionIndex + hotBarEndIndex + 1) % (hotBarEndIndex + 1);

        //this will make a call to update hotbar selection from the UIscript
        events.callOnHotbarSelectionChanged(selectionIndex);
    }

    public void SwitchSelection(int newindex)
    {
        selectionIndex = newindex % (hotBarEndIndex + 1);

        //this will make a call to update hotbar selection from the UIscript
        events.callOnHotbarSelectionChanged(selectionIndex);
    }

    public void updateHotBarUI()
    {
        //get item list
        itemStack[] hotbarArray = new itemStack[hotBarEndIndex + 1];
        for(int i = 0; i <= hotBarEndIndex; i++)
        {
            hotbarArray[i] = playerInventory.getInventory(i);
        }
        //update ui
        events.callOnUpdatedHotbarContent(hotbarArray);
    }

    public void updateInventoryUI()
    {
        //update ui
        events.callOnUpdatedInventorySlotsContent(playerInventory.getInventory().ToArray());
    }

    //This is inventory controller code
    //this might be refactored later but keep here for now

    /**
     * This is triggered by event call when inventory slot is pressed
     * 
     * If the item in mouse inventory can be merged with the item in the slot, then merge with remains
     * 
     * If not, then swap
     * 
     * this should also call the update inventory event
     */
    public void slotLeftClicked(int id)
    {
        //check for merge
        if (playerInventory.getInventory(id) != null && MouseInventory != null && playerInventory.getInventory(id).getItem().Equals(MouseInventory.getItem()) && !playerInventory.getInventory(id).isFull())
        {
            //this means it can be merged
            int leftOver = playerInventory.getInventory(id).MergeStack(MouseInventory.getCount());
            if(leftOver == -1)
            {
                //this means nothing left in mouse inventory
                MouseInventory = null;
            }
            else
            {
                MouseInventory.setCount(leftOver);
            }
        }
        else
        {
            //cant be merged, swap stack
            itemStack hold = playerInventory.getInventory(id);
            //introduce should be able to handle null
            playerInventory.introduce(MouseInventory, id);
            MouseInventory = hold;
        }

        //update UI
        events.callOnInventoryStackPickedUpByMouse(playerInventory.getInventory(id), MouseInventory, id);
    }

    public void slotRightClicked(int id)
    {
        Debug.Log("slot clicked at: " + id);
        if (MouseInventory != null && playerInventory.getInventory(id) == null)
        {
            playerInventory.introduce(new itemStack(MouseInventory.getItem(), 1), id);
            if (!MouseInventory.decrementCount())
                MouseInventory = null;
        }
        else if (MouseInventory != null && playerInventory.getInventory(id).getItem().Equals(MouseInventory.getItem()))
        {
            playerInventory.getInventory(id).incrementCount();
            if (!MouseInventory.decrementCount())
                MouseInventory = null;
        }
        events.callOnInventoryStackPickedUpByMouse(playerInventory.getInventory(id), MouseInventory, id);
    }

    public void CraftCallback(Recipe recipe)
    {
        foreach(recipeList entry in recipe.getIngredientsList())
        {
            if(!checkItems(entry.type, entry.count))
            {
                return;
            }
        }
        //if it reachs this point, that means you can craft
        foreach (recipeList entry in recipe.getIngredientsList())
        {
            removeItemWhenEnough(entry.type, entry.count);
        }
        foreach (recipeList entry in recipe.getOutputList())
        {
            IntroduceToInventory(new itemStack(entry.type, entry.count));
        }
    }
}
