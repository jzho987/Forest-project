using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectProperty : MonoBehaviour
{
    string ObjectType;
    [SerializeField] string ObjectName;

    public string getObjectType()
    {
        return ObjectType;
    }
    public string getObjectName()
    {
        return ObjectName;
    }
}
