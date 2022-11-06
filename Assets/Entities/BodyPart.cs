using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : Item
{
    [Header("Body Part Properties")]
    [SerializeField] private float bodyStructureCost = 1;
    [SerializeField] private Ability[] abilities = null;
    [SerializeField] private BodyPart[] dependentBodyParts = null;
    [SerializeField] private float carryCapacityExtension = 0;

    [HideInInspector] public float BodyStructureCost { get => bodyStructureCost; }
    [HideInInspector] public BodyPart[] DependentBodyParts { get => dependentBodyParts; }
    [HideInInspector] public Ability[] Abilities { get => abilities; }
    [HideInInspector] public float CarryCapacityExtension { get => carryCapacityExtension; }
}
