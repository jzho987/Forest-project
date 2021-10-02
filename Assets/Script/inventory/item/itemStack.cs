using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemStack : MonoBehaviour
{
    item Item;
    int count;

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
