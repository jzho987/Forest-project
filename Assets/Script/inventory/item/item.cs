using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item", menuName = "item")]
public class item : ScriptableObject
{
    //overview
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;

    //world item
    [SerializeField] GameObject WorldItem;

    //inventory item
    [SerializeField] Sprite itemSprite;
}
