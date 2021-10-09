using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item", menuName = "item")]
public class item : ScriptableObject
{
    //overview
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    [SerializeField] int maxStack;
    [SerializeField] float ToolProficiency;

    //world item
    [SerializeField] public GameObject WorldItem;

    //inventory item
    [SerializeField] Sprite itemSprite;

    //getters
    public Sprite getItemSprite()
    {
        return itemSprite;
    }

    public int getMaxStack()
    {
        return maxStack;
    }

    public float getToolProficiency()
    {
        return ToolProficiency;
    }

    //temporary equals method to be used for testing purposes
    public override bool Equals(object other)
    {
        item Item = (item)other;
        return Item.itemName == this.itemName;
    }
}
