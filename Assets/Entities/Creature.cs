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

    public class AbilityTriggerGroup
    {
        public Ability.AbilityTriggers trigger;
        public List<Ability> abilities;
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
    private List<Ability> availableActiveAbilityList = new List<Ability>();
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
        FilterForActiveAbilities();
        primaryCombatAbility = FindPrimaryCombatAbility();
        primaryMovementAbility = FindPrimaryMovementAbility();
    }
    public void TakeTurn()
    {
        EventHandler.Instance.SignalAbilityUsed(Ability.AbilityTriggers.OnTurnStart, this);
        if (isUnderPlayerControl)
        {
            Debug.Log("Player started their turn.");
            CameraController.Instance.SetFollowTarget(transform);
            if (primaryMovementAbility)
            {
                WorldController.Instance.TurnHandler.InputHandler.StartAiming(this, primaryMovementAbility);
            }
        }
        else
        {
            Debug.Log(NameLabel + " started their turn.");
            if (availableActiveAbilityList.Count == 0)
            {
                // no active abilities = skip the turn
                turnTimer = 1000;
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

    private void FilterForActiveAbilities()
	{
        foreach (Ability a in availableAbilityList)
        {
            if (a.IsManuallyActivated)
                availableActiveAbilityList.Add(a);
        }
	}

    private Ability FindPrimaryMovementAbility()
    {
        foreach (Ability ability in availableActiveAbilityList)
        {
            if (System.Array.Exists(ability.AiFlagList, element => element is Ability.AbilityAiFlags.MovesSelf)) // if is a movement ability
            {
                return ability;
            }
        }
        return null;
    }

    private Ability FindPrimaryCombatAbility()
    {
        foreach (Ability ability in availableActiveAbilityList)
        {
            if (System.Array.Exists(ability.AiFlagList, element => element is Ability.AbilityAiFlags.Damages))
            {
                return ability;
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
    public void NotifyAbilitiesOfEvent(Ability.AbilityTriggers trigger, Entity entity)
    {
        foreach (Ability a in availableAbilityList)
        {
            a.ReactToEvent(trigger, entity);
        }
    }

    // creating distinction between healing and dealing damage in case of future effects that care only about one or the other
    public void DealDamage(int amount)
    {
        EventHandler.Instance.SignalAbilityUsed(Ability.AbilityTriggers.OnReceiveDamage, this);
        CurrentHealth = CurrentHealth - amount;
    }

    public void HealDamage(int amount)
    {
        CurrentHealth = CurrentHealth + amount;
    }

    protected void Die()
    {
        EventHandler.Instance.SignalAbilityUsed(Ability.AbilityTriggers.OnDeath, this);
    }

    public void ChangeTurnTimer(float amount)
    {
        turnTimer += amount;
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

    public void TryInteract()
    {
        foreach (Creature creature in CurrentTile.Entities)
        {
            creature.NotifyAbilitiesOfEvent(Ability.AbilityTriggers.OnInteracted, this);
        }
        return;
    }
}
