using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Interactables control the interaction between the usually the player and the object its targeting
 * The object pointer is retrived in start so there will always be a object pointer instead of 
 * serializing and getting assigned manually.
 * 
 * author: Jiazhi Zhou
 */
[RequireComponent(typeof(ObjectProperty))]
public class Interactable : MonoBehaviour
{
    [SerializeField] protected GameObject objectPointer;

    //triggered when players left click on the hitbox, the action controller is passed in to retrieve certain attributes
    public virtual void f1Interaction(characterController actionController) { }


    //triggered when players right click on the hitbox, the action controller is passed in to retrieve certain attributes
    public virtual void f2Interaction(characterController actionController) { }


    //triggered when players hover over hitbox, the action controller is passed in to retrieve certain attributes
    public virtual void HoverInteraction(characterController actionController) { }
}
