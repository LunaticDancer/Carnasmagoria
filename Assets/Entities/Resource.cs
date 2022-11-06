using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Item
{
    public enum ResourceTypes
    {
        Blood,
        Flesh,
        Bone,
        BrainMatter,
        Metal,
        Electronics,
        Fuel,
        Energy
    }

    [SerializeField] private ResourceTypes resourceType = ResourceTypes.Blood;

    [HideInInspector] public ResourceTypes ResourceType { get => resourceType; }
}
