using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteractable : Interactable
{
    float harvestPoint;
    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        harvestPoint = Property.getHP();
    }

    //attack the tree
    public override void f1Interaction() {
        if (--harvestPoint < 0) {
            Harvest();
        }
    }
    public void f1Interaction(float AttackPoint)
    {
        harvestPoint -= AttackPoint;
        if (harvestPoint < 0)
        {
            Harvest();
        }
    }

    void Harvest()
    {
        Debug.Log("tree is harvested");
    }

    public override void f2Interaction()
    {
        Debug.Log("this is " + Property.getName());
    }

    static class Property
    {
        static string TreeName = "Oaklin tree";
        static float HarvestPoints = 10;

        public static float getHP() 
        {
            return HarvestPoints;
        }

        public static string getName()
        {
            return TreeName;
        }
    }
}
