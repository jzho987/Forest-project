using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item", menuName = "itemStack")]
public class itemStack : ScriptableObject
{
    [SerializeField] item Item;
    [SerializeField] int count;

    public itemStack(item Item, int count)
    {
        this.Item = Item;
        this.count = count;
    }

    public itemStack(item Item)
    {
        this.Item = Item;
        count = 1;
    }

    public item getItem()
    {
        return Item;
    }

    public bool incrementCount()
    {
        if(++count > Item.getMaxStack())
        {
            count--;
            return false;
        }
        return true;
    }

    public bool decrementCount()
    {
        if (--count < 0)
        {
            count++;
            return false;
        }
        return true;
    }
}
