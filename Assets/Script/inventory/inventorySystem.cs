using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventorySystem : MonoBehaviour
{
    [SerializeField] protected List<itemStack> itemList = new List<itemStack>();
    [SerializeField] int inventorySize;
    [SerializeField] string inventoryName;

    private void Start()
    {
    }

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
