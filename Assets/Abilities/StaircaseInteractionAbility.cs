using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Scriptables/Abilities/StaircaseInteractionAbility")]
public class StaircaseInteractionAbility : Ability
{
	[SerializeField] private LevelData targetLevel = null;

	[HideInInspector] public LevelData TargetLevel { get => targetLevel; }

	public override void ReactToEvent(AbilityTriggers trigger, Entity entity)
	{
		if (trigger == AbilityTriggers.OnInteracted)
		{
			Cast((Creature)entity, null);
		}
	}

	public override bool Cast(Creature caster, Tile target)
	{
		if (targetLevel)
		{
			WorldController.Instance.LevelGenerator.MoveBetweenLevels(targetLevel, WorldController.Instance.TurnHandler.CurrentCreature);
			return true;
		}
		return false;
	}
}
