using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestInteractable : Interactable
{
    public override void f2Interaction(characterController actionController)
    {
        actionController.switchState();
    }
}
