using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public enum AimingModes
    {
        NoAiming,
        EightDirections,
        FreeAim
    }

    public enum AbilityTriggers
    {
        OnUse,
        OnTurnStart,
        OnMove,
        OnReceiveDamage,
        OnDeath,
        OnAttachThisBodyPart,
        OnDetachThisBodyPart,
        OnAttachAnyBodyPart,
        OnDetachAnyBodyPart,
        OnInteracted
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

    [SerializeField] protected bool caresForVision = false;
    [SerializeField] protected bool caresForCollision = false;
    [SerializeField] protected int range = 1;
    [SerializeField] protected float timeCost = 1; // 0 time cost abilities don't end the turn
    [SerializeField] protected bool isManuallyActivated = false;
    [SerializeField] protected AimingModes aimingMode = AimingModes.NoAiming;
    [SerializeField] protected AbilityAiFlags[] aiFlagList = { };
    [SerializeField] protected ResourceCost[] resourceCosts = null;

    [HideInInspector] public bool CaresForVision { get => caresForVision; }
    [HideInInspector] public bool CaresForCollision { get => caresForCollision; }
    [HideInInspector] public int Range { get => range; }
    [HideInInspector] public float TimeCost { get => timeCost; }
    [HideInInspector] public AimingModes AimingMode { get => aimingMode; }
    [HideInInspector] public bool IsManuallyActivated { get => isManuallyActivated; }
    [HideInInspector] public AbilityAiFlags[] AiFlagList { get => aiFlagList; }
    [HideInInspector] public ResourceCost[] ResourceCosts { get => resourceCosts; }

    public virtual bool Cast(Creature caster, Tile target) // will return false if the cast fails for any reason
    {
        return true; // always overwrite
    }

    public virtual void ReactToEvent(Ability.AbilityTriggers trigger, Entity entity)
    {

    }
}
