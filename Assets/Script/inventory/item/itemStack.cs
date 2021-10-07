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

    public int getCount()
    {
        return count;
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

    public void setCount(int newCount)
    {
        count = newCount;

    }

    public int MergeStack(int count)
    {
        int max = this.Item.getMaxStack();
        if(count + this.count <= max)
        {
            this.count += count;
            //returns -1 if there are no leftover
            return -1;
        }
        else
        {
            this.count = max;
            //return leftover amount if there are leftover
            return (count + this.count) - max;
        }
    }

    public bool isFull()
    {
        return this.count == Item.getMaxStack();
    }
}
