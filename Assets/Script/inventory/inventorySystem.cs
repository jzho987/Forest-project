using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventorySystem : MonoBehaviour
{
    [SerializeField] List<item> itemList = new List<item>();

    int inventorySize;
    [SerializeField] string inventoryName;

    private void Start()
    {
        inventorySize = itemList.Count;
    }

    public int getSize()
    {
        return inventorySize;
    }

    public List<item> getInvetory()
    {
        return itemList;
    }

    public item getInventory(int i)
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

    protected void introduce(item newItem, int i)
    {
        itemList[i] = newItem;
    }

    void remove(int i)
    {
        itemList.RemoveAt(i);
    }
}
