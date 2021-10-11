using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObjectProperty : MaterialNodeObject
{
    string TreeType = "Oak";
    [SerializeField] GameObject TreeStump;

    public void Harvest(float DamadgeAmount)
    {
        Debug.Log(DamadgeAmount);
        if(DropHitPoint(DamadgeAmount))
        {
            //to be replaced with item's spawn function
            Death();
        }
    }

    public override void Death()
    {
        Drop().spawnItemInWorld(6, MainPointerObject.transform.position + Vector3.up * 1.3f);
        Instantiate(TreeStump, MainPointerObject.transform.position,MainPointerObject.transform.rotation);
        Destroy(MainPointerObject);
    }
}
