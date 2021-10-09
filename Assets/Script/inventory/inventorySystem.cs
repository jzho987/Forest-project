using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventorySystem : MonoBehaviour
{
    public List<itemStack> itemList = new List<itemStack>();
    [SerializeField] int inventorySize;
    [SerializeField] string inventoryName;

    public int getSize()
    {
        return inventorySize;
    }

    public List<itemStack> getInvetory()
    {
        return itemList;
    }

    public itemStack getInventory(int i)
    {
        if(itemList.Count == 0)
        {
            for (int j = 0; j < inventorySize; j++)
            {
                itemList.Add(null);
            }
        }

        if (i > inventorySize)
        {
            return null;
        }
        else
        {
            return itemList[i];
        }
    }

    protected void introduce(itemStack newItemStack, int i)
    {
        itemList[i] = newItemStack;
    }

    protected int merge(itemStack newItemStack, int i)
    {
        return itemList[i].MergeStack(newItemStack.getCount());
    }

    void remove(int i)
    {
        itemList.RemoveAt(i);
    }
}
