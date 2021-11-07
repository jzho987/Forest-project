using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TreeObjectProperty : MaterialNodeObject
{
    PhotonView PV;

    //call this in awake to get PV on loading
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    public void Harvest(float DamageAmount)
    {
        PV.RPC("RPC_Harvest", RpcTarget.All, DamageAmount);
    }

    [PunRPC]
    public void RPC_Harvest(float DamageAmount)
    {
        if (!PV.IsMine)
            return;

        Debug.Log(DamageAmount);
        if (DropHitPoint(DamageAmount))
        {
            //to be replaced with item's spawn function
            Death();
        }
    }

    //drop hit point should be broad cast to all players
    public bool DropHitPoint(float amount)
    {
        HitPoint -= amount;
        if (HitPoint <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /**
     * death should only be called by ONE machine
     * 
     * this is mostly likely going to be the host, who will run the command on a empty game object
     * this is to be implemented later, currently it will work
     */
    public override void Death()
    {
        DropItem();
        PhotonNetwork.Instantiate("TreeStumpPrefab", MainPointerObject.transform.position,MainPointerObject.transform.rotation);
        PhotonNetwork.Destroy(MainPointerObject);
    }
}
