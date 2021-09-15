using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public virtual void f1Interaction() 
    {
        Debug.Log("player interacted with " + this.gameObject.name + " with fire1");
    }

    public virtual void f2Interaction()
    {
        Debug.Log("player interacted with " + this.gameObject.name + " with fire2");
    }

    public virtual void HoverInteraction()
    {
        Debug.Log("player interacted with " + this.gameObject.name + " with hover");
    }
}
