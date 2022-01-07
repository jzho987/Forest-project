using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/**
 * This script should be used for every item that is in world space
 * 
 * This script contains all interactions with the item
 * 
 * right clicking an item should pick up the item
 */
public class ItemInteratable : Interactable {

    itemStack thisItem;

    public void setNewStack(itemStack newItemStack)
    {
        thisItem = newItemStack;
    }

    public override void f2Interaction(characterController actionController) {
        actionController.getPlayerInventorySystem().IntroduceToInventory(thisItem);
        PhotonNetwork.Destroy(objectPointer);
    }
}
