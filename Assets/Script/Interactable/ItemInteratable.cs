using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteratable : Interactable {
    [SerializeField] itemStack thisItem;

    public override void f2Interaction(characterController actionController) {
        actionController.getPlayerInventorySystem().IntroduceToInventory(thisItem);
        Destroy(objectPointer);
    }


}
