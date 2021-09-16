using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteractable : Interactable
{
    TreeObjectProperty objProperty;

    public void Start()
    {
        objProperty = objectPointer.GetComponent<TreeObjectProperty>();
    }

    //harvest tree
    public override void f1Interaction(int amount)
    {
        objProperty.Harvest(amount);
    }

    public override void f2Interaction()
    {
        Debug.Log("this is " + objProperty.getObjectName());
    }
}
