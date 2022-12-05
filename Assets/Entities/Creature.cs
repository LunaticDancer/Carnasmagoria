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

    public enum AiBehaviorFlags
    {
        Cowardly, // will actively run away to keep the player at max attack distance
        CloseAndPersonal, // prioritises getting in melee range
        Supportive // prioritizes helping allies out over direct combat
    }

    [Header("Creature Properties")]
    [SerializeField] private bool isUnderPlayerControl = false;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float visionRange = 1;
    [SerializeField] private float turnTimer = 1;
    [SerializeField] private HostilitySettings hostilityMode = HostilitySettings.Neutral;
    [SerializeField] private HostilitySettings[] alliedToCreaturesWith = { };
    [SerializeField] private float carryCapacity = 10;
    [SerializeField] private float bodyStructureBudget = 100;
    [SerializeField] private List<BodyPart> bodyParts = new List<BodyPart>();

    private List<Ability> availableAbilityList = new List<Ability>();
    private Ability primaryMovementAbility = null;
    private Ability primaryCombatAbility = null;


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
    [HideInInspector] public float VisionRange { get => visionRange; }
    [HideInInspector] public float TurnTimer { get => turnTimer; }
    [HideInInspector] public HostilitySettings HostilityMode { get => hostilityMode; }
    [HideInInspector] public HostilitySettings[] AlliedToCreaturesWith { get => alliedToCreaturesWith; }
    [HideInInspector] public float CarryCapacity { get => carryCapacity; }
    [HideInInspector] public float BodyStructurebudget { get => bodyStructureBudget; }
    [HideInInspector] public List<BodyPart> BodyParts { get => bodyParts; }


    public void Init()
    {
        InitBodyParts();
        availableAbilityList = GatherAllAbilities();
        primaryCombatAbility = FindPrimaryCombatAbility();
        primaryMovementAbility = FindPrimaryMovementAbility();
    }
    public void TakeTurn()
    {
        if (isUnderPlayerControl)
        {
            GameController.Instance.CameraController.SetFollowTarget(transform);
            if (primaryMovementAbility)
            {
                GameController.Instance.WorldController.TurnHandler.InputHandler.StartAiming(this, primaryMovementAbility);
            }
        }
    }

    private List<Ability> GatherAllAbilities()
    {
        List<Ability> result = new List<Ability>();
        foreach (BodyPart bodyPart in bodyParts)
        {
            List<Ability> partList = bodyPart.GatherAbilitiesRecursive();
            foreach (Ability ability in partList)
            {
                if (!result.Contains(ability))
                {
                    result.Add(ability);
                }
            }
        }
        return result;
    }

    private Ability FindPrimaryMovementAbility()
    {
        foreach (Ability ability in availableAbilityList)
        {
            if (System.Array.Exists(ability.AiFlagList, element => element is Ability.AbilityAiFlags.MovesSelf)) // if is a movement ability
            {
                if (System.Array.Exists(ability.TriggerList, element => element is Ability.AbilityTriggers.OnUse)) // if can be manually used
                {
                    return ability;
                }
            }
        }
        return null;
    }

    private Ability FindPrimaryCombatAbility()
    {
        foreach (Ability ability in availableAbilityList)
        {
            if (System.Array.Exists(ability.AiFlagList, element => element is Ability.AbilityAiFlags.Damages))
            {
                if (System.Array.Exists(ability.TriggerList, element => element is Ability.AbilityTriggers.OnUse))
                {
                    return ability;
                }
            }
        }
        return null;
    }

    // this is only meant to be used on approved creature compositions, can cause invalid creatures otherwise (skips structure costs)
    private void InitBodyParts()
    {
        foreach (BodyPart part in bodyParts)
        {
            part.Attach(this);
        }
    }

    // creating distinction between healing and dealing damage in case of future effects that care only about one or the other
    public void DealDamage(int amount)
    {
        CurrentHealth = CurrentHealth - amount;
    }

    public void HealDamage(int amount)
    {
        CurrentHealth = CurrentHealth + amount;
    }

    protected void Die()
    {
        // yeah, I still gotta think through how dying will be handled
    }

    public void LowerTurnTimer(float amount)
    {
        turnTimer -= amount;
    }

    public void SetVisionRange(float range, bool forceOverwrite)
    {
        if (!forceOverwrite)
        {
            if (range < visionRange) // if not forced to overwrite, the function favors the greater of the two values (useful in case of the player having multiple eyes)
            {
                return;
            }
        }
        visionRange = range;
    }
}
