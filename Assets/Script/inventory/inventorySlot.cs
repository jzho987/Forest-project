using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventorySlot : MonoBehaviour
{
    [SerializeField] Image itemImage;

    public void onClickAction()
    {
        Debug.Log("player clicked on" + this.name);
    }

    public void updateImage(Sprite itemSprite)
    {
        Debug.Log("image changed");
        itemImage.sprite = itemSprite;
    }
}
