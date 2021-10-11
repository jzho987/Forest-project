using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlintRockNodeInteractable : Interactable
{
    FlintRockObject objProperty;

    public void Start()
    {
        objProperty = objectPointer.GetComponent<FlintRockObject>();
    }

    //harvest tree
    public override void f1Interaction(characterController actionController)
    {
        objProperty.Harvest(actionController.getHarvestStrength());
        actionController.SwingAnimation();
    }

    public override void f2Interaction(characterController actionController)
    {
        Debug.Log("this is " + objProperty.getObjectName());
    }
}
