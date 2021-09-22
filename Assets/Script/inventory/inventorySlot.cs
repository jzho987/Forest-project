using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventorySlot : MonoBehaviour
{
    public void onClickAction()
    {
        Debug.Log("player clicked on" + this.name);
    }
}
