using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public enum AimingModes
    {
        NoAiming,
        EightDirections,
        FreeAim,
        FreeAimOnlyVisibleTiles
    }

    public enum AbilityTriggers
    {
        OnUse,
        OnMove,
        OnReceiveDamage,
        OnDeath
    }

    [System.Serializable] public class ResourceCost
    {
        public Resource.ResourceTypes ResourceType;
        public float Amount;
    }

    [SerializeField] private int range = 1;
    [SerializeField] private float timeCost = 1;
    [SerializeField] private AimingModes aimingMode = AimingModes.NoAiming;
    [SerializeField] private AbilityTriggers[] triggerList = {  };
    [SerializeField] private ResourceCost[] resourceCosts = null;

    [HideInInspector] public int Range { get => range; }
    [HideInInspector] public float TimeCost { get => timeCost; }
    [HideInInspector] public AimingModes AimingMode { get => aimingMode; }
    [HideInInspector] public AbilityTriggers[] TriggerList { get => triggerList; }
    [HideInInspector] public ResourceCost[] ResourceCosts { get => resourceCosts; }

    public virtual bool Cast(Creature caster) // will return false if the cast fails for any reason
    {
        return true; // always overwrite
    }
}
