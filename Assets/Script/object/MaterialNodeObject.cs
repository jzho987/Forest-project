using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialNodeObject : ObjectProperty
{
    string ObjectType = "MaterialNode";
    [SerializeField] protected int HitPoint;
    [SerializeField] GameObject[] ItemDrop;

    public virtual void Harvest() { }

    public GameObject Drop()
    {
        return ItemDrop[0];
    }

    public void WeightedDrop()
    {

    }

    public bool DropHitPoint(int amount)
    {
        HitPoint -= amount;
        if (HitPoint <= 0)
            return true;
        else return false;
    }
}
