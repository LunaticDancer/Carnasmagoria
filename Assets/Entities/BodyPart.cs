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

    public void Attach(Entity target)
    {
        EventHandler.Instance.OnAbilityUsed += NotifyAbilitiesOfEvent;
        NotifyAbilitiesOfEvent(Ability.AbilityTriggers.OnAttachThisBodyPart, this);
        EventHandler.Instance.SignalAbilityUsed(Ability.AbilityTriggers.OnAttachAnyBodyPart, this);
        attachedTo = target;
        foreach (BodyPart part in dependentBodyParts)
        {
            part.Attach(this);
        }
    }

    public void Detach(Tile targetTile)
    {
        EventHandler.Instance.SignalAbilityUsed(Ability.AbilityTriggers.OnDetachAnyBodyPart, this);
        NotifyAbilitiesOfEvent(Ability.AbilityTriggers.OnDetachThisBodyPart, this);
        EventHandler.Instance.OnAbilityUsed -= NotifyAbilitiesOfEvent;
        attachedTo = null;
        SetTile(targetTile);
    }

    private void NotifyAbilitiesOfEvent(Ability.AbilityTriggers trigger, Entity entity)
    {
        foreach (Ability a in abilities)
        {
            a.ReactToEvent(trigger, entity);
        }
    }

    public Creature GetCreature()
    {
        return GetComponentInParent<Creature>();
    }

    public List<Ability> GatherAbilitiesRecursive()
    {
        List<Ability> result = new List<Ability>();

        foreach (BodyPart part in dependentBodyParts)
        {
            result.AddRange(part.GatherAbilitiesRecursive());
        }
        result.AddRange(abilities);

        return result;
    }

    public float GatherStructureCostRecursive()
    {
        float result = bodyStructureCost;
        foreach (BodyPart part in dependentBodyParts)
        {
            result += part.BodyStructureCost;
        }
        return result;
    }
}
