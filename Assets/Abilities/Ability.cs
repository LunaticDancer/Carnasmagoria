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
        FreeAimOnlyVisible,
        FreeAimOnlyVisibleAndPassable
    }

    public enum AbilityTriggers
    {
        OnUse,
        OnTurnStart,
        OnMove,
        OnReceiveDamage,
        OnDeath
    }

    public enum AbilityAiFlags
    {
        MovesSelf,
        MovesEnemies,
        MovesAllies,
        Damages,
        Heals
    }

    [System.Serializable] public class ResourceCost
    {
        public Resource.ResourceTypes ResourceType;
        public float Amount;
    }

    [SerializeField] private int range = 1;
    [SerializeField] private float timeCost = 1; // 0 time cost abilities don't end the turn
    [SerializeField] private AimingModes aimingMode = AimingModes.NoAiming;
    [SerializeField] private AbilityTriggers[] triggerList = { };
    [SerializeField] private AbilityAiFlags[] aiFlagList = { };
    [SerializeField] private ResourceCost[] resourceCosts = null;

    [HideInInspector] public int Range { get => range; }
    [HideInInspector] public float TimeCost { get => timeCost; }
    [HideInInspector] public AimingModes AimingMode { get => aimingMode; }
    [HideInInspector] public AbilityTriggers[] TriggerList { get => triggerList; }
    [HideInInspector] public AbilityAiFlags[] AiFlagList { get => aiFlagList; }
    [HideInInspector] public ResourceCost[] ResourceCosts { get => resourceCosts; }

    public virtual bool Cast(Creature caster, Tile target) // will return false if the cast fails for any reason
    {
        return true; // always overwrite
    }
}
