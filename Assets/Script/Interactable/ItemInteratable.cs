using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteratable : Interactable {
    itemStack thisItem;

    public void setNewStack(itemStack newItemStack)
    {
        thisItem = newItemStack;
    }

    public override void f2Interaction(characterController actionController) {
        actionController.getPlayerInventorySystem().IntroduceToInventory(thisItem);
        Destroy(objectPointer);
    }
}
