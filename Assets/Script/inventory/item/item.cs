using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item", menuName = "item")]
public class item : ScriptableObject
{
    [Header("overview")]
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    [SerializeField] int maxStack;

    /**
     * this is for instantiating, currently not in use
     */
    [SerializeField] GameObject WorldItem;

    /**
     * The sprite is used for display in inventory view
     */
    [SerializeField] Sprite itemSprite;

    //getters
    public Sprite getItemSprite() { return itemSprite; }
    public string getItemName() { return itemName; }
    public int getMaxStack() { return maxStack; }

    //temporary equals method to be used for testing purposes
    public override bool Equals(object other)
    {
        item Item = (item)other;
        return Item.itemName == this.itemName;
    }

    /**
     * Use should be triggered when a character is holding this particular item,
     * and press a certain key
     */
    public virtual void use() { }
}
