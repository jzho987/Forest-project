using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected GameObject objectPointer;

    public virtual void f1Interaction(characterController actionController) 
    {
        Debug.Log("player interacted with " + this.gameObject.name + " with fire1");
    }

    public virtual void f2Interaction(characterController actionController)
    {
        Debug.Log("player interacted with " + this.gameObject.name + " with fire2");
    }

    public virtual void HoverInteraction(characterController actionController)
    {
        Debug.Log("player interacted with " + this.gameObject.name + " with hover");
    }

    public virtual string[] Interactions(characterController actionController)
    {
        return null;
    }
}
