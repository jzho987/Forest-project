using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The flint Rock Node will decay upon taking enough damadge, it will reset its health based on the new decay stage health, and drop rocks every new decay stage
 * 
 */
public class FlintRockObject : MaterialNodeObject
{
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
            Decay();
        }
    }

    /**
     * death is only for deleting the object
     */
    public override void Death()
    {
        Drop().spawnItemInWorld(4 , MainPointerObject.transform.position + Vector3.up * 0.4f);
        Destroy(MainPointerObject);
    }

    /**
     * decay sets back the decay state of the object, and move it to a more decayed state until its death
     */
    public void Decay()
    {
        if (++decayStage > maxDecayStage)
        {
            Death();
        }
        else
        {
            displayDecayStage(decayStage);
            HitPoint = DecayHitPoints[decayStage];
            DecayDrop();
        }
    }

    public void DecayDrop()
    {
        Drop().spawnItemInWorld(3, MainPointerObject.transform.position + Vector3.up * 0.4f);
    }

    public void displayDecayStage(int stage)
    {
        GameObject newModel = Instantiate(DecayStates[stage], modelPointer.transform.position, modelPointer.transform.rotation, MainPointerObject.transform);
        Destroy(modelPointer);
        modelPointer = newModel;
    }
}
