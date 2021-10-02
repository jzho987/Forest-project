using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIscript : MonoBehaviour
{
    [SerializeField] GameObject[] SlotUIList;

    public void UpdateUI(itemStack[] slotItems)
    {
        int size = SlotUIList.Length;
        for(int i = 0; i < size; i++)
        {
            if (!(slotItems[i] == null))
            {
                Debug.Log("update " + i);
                SlotUIList[i].GetComponent<inventorySlot>().updateImage(slotItems[i].getItem().getItemSprite());
            }
        }
    }
}
