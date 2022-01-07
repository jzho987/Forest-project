using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MaterialNodeObject : ObjectProperty
{
    [SerializeField] protected float HitPoint;
    [SerializeField] item[] ItemDrop;

    public virtual void Harvest() { }

    public item Drop()
    {
        return ItemDrop[0];
    }

    /**
     * this handles the material node dropping items
     */
    public void DropItem()
    {
        itemStack spawnStack = new itemStack(Drop(), 1);
        GameObject itemSpawned = PhotonNetwork.Instantiate(Drop().getItemName(), MainPointerObject.transform.position + Vector3.up * 0.4f, Quaternion.identity);
        itemSpawned.GetComponent<ItemInteratable>().setNewStack(spawnStack);
    }
}
