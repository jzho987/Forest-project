using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteratable : Interactable { 
    public override void PickUpInteraction() {
        Debug.Log("Player tried to pick up " + Property.getName());
    }

    static class Property
    {
        static string ItemName = "oak log";

        public static string getName()
        {
            return ItemName;
        }
    }
}
