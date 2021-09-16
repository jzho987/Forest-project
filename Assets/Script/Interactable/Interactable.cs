using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] public GameObject objectPointer;

    public virtual void f1Interaction() 
    {
        Debug.Log("player interacted with " + this.gameObject.name + " with fire1");
    }

    public virtual void f1Interaction(int amount)
    {
        Debug.Log("player interacted with " + this.gameObject.name + " with fire1 >" + amount);
    }

    public virtual void f2Interaction()
    {
        Debug.Log("player interacted with " + this.gameObject.name + " with fire2");
    }

    public virtual void HoverInteraction()
    {
        Debug.Log("player interacted with " + this.gameObject.name + " with hover");
    }

    public virtual string[] Interactions()
    {
        return null;
    }

    public virtual void PickUpInteraction() { }
}
