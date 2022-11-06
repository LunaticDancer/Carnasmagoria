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

    [SerializeField] private int range = 1;
    [SerializeField] private float timeCost = 1;
    [SerializeField] private AimingModes aimingMode = AimingModes.EightDirections;

    [HideInInspector] public int Range { get => range; }
    [HideInInspector] public float TimeCost { get => timeCost; }
    [HideInInspector] public AimingModes AimingMode { get => aimingMode; }

    public virtual bool Cast(Creature caster) // will return false if the cast fails for any reason
    {
        return true; // always overwrite
    }
}
