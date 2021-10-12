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

    //if the prociency is -1 then that means the tool is unusable for that purpose
    [SerializeField] float AxeProficiency;
    [SerializeField] float PickaxeProficiency;
    [SerializeField] float ShovelProficiency;

    //world item
    [SerializeField] GameObject WorldItem;
    [SerializeField] GameObject HoldingItem;

    //inventory item
    [SerializeField] Sprite itemSprite;

    //getters
    public Sprite getItemSprite() { return itemSprite; }

    //public GameObject getWorldItem() { return WorldItem; }

    public int getMaxStack() { return maxStack; }

    public float getToolProficiency() { return ToolProficiency; }

    public float getAxeProficiency() { return AxeProficiency; }

    public float getPickaxeProficiency() { return PickaxeProficiency; }

    public float getShovelProficiency() { return ShovelProficiency; }

    public GameObject getHoldingItem() { return HoldingItem; }

    //spawn in the item in world state, with an amount
    public void spawnItemInWorld(int amount, Vector3 location)
    {
        GameObject drop = Instantiate(WorldItem, location, Quaternion.identity);
        drop.GetComponent<ItemInteratable>().setNewStack(new itemStack(this, amount));
    }

    public void spawnItemInWorld(int amount, Vector3 location, Quaternion rotation)
    {
        GameObject drop = Instantiate(WorldItem, location, rotation);
        drop.GetComponent<ItemInteratable>().setNewStack(new itemStack(this, amount));
    }

    //temporary equals method to be used for testing purposes
    public override bool Equals(object other)
    {
        item Item = (item)other;
        return Item.itemName == this.itemName;
    }
}
