using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObjectProperty : MaterialNodeObject
{
    string ObjectName = "Tree";
    string TreeType = "Oak";
    [SerializeField] GameObject TreeStump;

    public void Harvest(int DamadgeAmount)
    {
        if(DropHitPoint(DamadgeAmount))
        {
            //to be replaced with item's spawn function
            Death();
        }
    }

    public override void Death()
    {
        GameObject drop = Instantiate(Drop().WorldItem, MainPointerObject.transform.position + Vector3.up * 1.3f, Quaternion.Euler(90,0,0));
        drop.GetComponent<ItemInteratable>().setNewStack(new itemStack(Drop(),8));
        Instantiate(TreeStump, MainPointerObject.transform.position,MainPointerObject.transform.rotation);
        Destroy(MainPointerObject);
    }
}
