using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : Item
{
    [Header("Body Part Properties")]
    [SerializeField] private Entity attachedTo = null;
    [SerializeField] private float bodyStructureCost = 1;
    [SerializeField] private List<Ability> abilities = new List<Ability>();
    [SerializeField] private List<BodyPart> dependentBodyParts = new List<BodyPart>();
    [SerializeField] private float carryCapacityExtension = 0;

    [HideInInspector] public Entity AttachedTo { get => attachedTo; }
    [HideInInspector] public float BodyStructureCost { get => bodyStructureCost; }
    [HideInInspector] public List<BodyPart> DependentBodyParts { get => dependentBodyParts; }
    [HideInInspector] public List<Ability> Abilities { get => abilities; }
    [HideInInspector] public float CarryCapacityExtension { get => carryCapacityExtension; }
}
