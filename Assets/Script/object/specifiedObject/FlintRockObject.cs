﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The flint Rock Node will decay upon taking enough damadge, it will reset its health based on the new decay stage health, and drop rocks every new decay stage
 * 
 */
public class FlintRockObject : MaterialNodeObject
{
    string ObjectName = "RockNode";
    string RockType = "flint";
    int decayStage = 0;
    [SerializeField] int maxDecayStage;
    [SerializeField] GameObject[] DecayStates;
    [SerializeField] int[] DecayHitPoints;
    [SerializeField] GameObject modelPointer;

    public void Start()
    {
        //set this to be the hit point of the max decay stage
        //since rock node hit points are stored in the decay hit point attribute instead of the hitpoint attribute
        HitPoint = DecayHitPoints[0];
    }

    public void Harvest(float DamadgeAmount)
    {
        if (DropHitPoint(DamadgeAmount))
        {
            //to be replaced with item's spawn function
            if(Decay())
            {
                Death();
            }
        }
    }

    /**
     * death is only for deleting the object
     */
    public override void Death()
    {
        GameObject drop = Instantiate(Drop().WorldItem, MainPointerObject.transform.position + Vector3.up * 0.4f, Quaternion.Euler(90, 0, 0));
        drop.GetComponent<ItemInteratable>().setNewStack(new itemStack(Drop(), 3));
        Destroy(MainPointerObject);
    }

    /**
     * decay sets back the decay state of the object, and move it to a more decayed state until its death
     */
    public bool Decay()
    {
        if (++decayStage > maxDecayStage)
        {
            return true;
        }
        else
        {
            displayDecayStage(decayStage);
            HitPoint = DecayHitPoints[decayStage];
            DecayDrop();
            return false;
        }
    }

    public void DecayDrop()
    {
        GameObject drop = Instantiate(Drop().WorldItem, MainPointerObject.transform.position + Vector3.up * 0.4f, Quaternion.Euler(90, 0, 0));
        drop.GetComponent<ItemInteratable>().setNewStack(new itemStack(Drop(), 3));
    }

    public void displayDecayStage(int stage)
    {
        GameObject newModel = Instantiate(DecayStates[stage], modelPointer.transform.position, modelPointer.transform.rotation, MainPointerObject.transform);
        Destroy(modelPointer);
        modelPointer = newModel;
    }
}
