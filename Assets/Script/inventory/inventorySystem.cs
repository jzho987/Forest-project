using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventorySystem : MonoBehaviour
{
    [SerializeField] List<itemStack> itemList = new List<itemStack>();
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

    protected void introduce(itemStack newItem, int i)
    {
        itemList[i] = newItem;
    }

    void remove(int i)
    {
        itemList.RemoveAt(i);
    }
}
