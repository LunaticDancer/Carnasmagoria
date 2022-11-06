using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : Entity
{
    public enum HostilitySettings
    {
        Neutral,
        HostileToEveryone,
        HostileToMachines,
        HostileToFlesh,
        HostileToPlayerOnly
    }

    [Header("Creature Properties")]
    [SerializeField] private bool isUnderPlayerControl = false;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float turnTimer = 1;
    [SerializeField] private HostilitySettings hostilityMode = HostilitySettings.Neutral;
    [SerializeField] private float carryCapacity = 10;
    [SerializeField] private float bodyStructureBudget = 100;
    [SerializeField] private BodyPart[] bodyParts = null;


    [HideInInspector] public bool IsUnderPlayerControl { get => isUnderPlayerControl; }
    [HideInInspector] public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else if (currentHealth <= 0)
            {
                Die();
            }
        }
    }
    [HideInInspector] public float TurnTimer { get => turnTimer; }
    [HideInInspector] public HostilitySettings HostilityMode { get => hostilityMode; }
    [HideInInspector] public float CarryCapacity { get => carryCapacity; }
    [HideInInspector] public float BodyStructurebudget { get => bodyStructureBudget; }
    [HideInInspector] public BodyPart[] BodyParts { get => bodyParts; }

    public virtual void TakeTurn() { }

    // creating distinction between healing and dealing damage in case of future effects that care only about one or the other
    public void DealDamage(int amount)
    {
        CurrentHealth = CurrentHealth - amount;
    }

    public void HealDamage(int amount)
    {
        CurrentHealth = CurrentHealth + amount;
    }

    protected virtual void Die()
    {
        // yeah, I still gotta think through how dying will be handled
    }
}
