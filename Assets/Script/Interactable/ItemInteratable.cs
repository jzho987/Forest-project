using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteratable : Interactable {
    [SerializeField] itemStack thisItem;

    public override void f2Interaction(characterController actionController) {
        actionController.getPlayerInventorySystem().PriorityIntroduce(thisItem, 1);
        Destroy(objectPointer);
    }


}
