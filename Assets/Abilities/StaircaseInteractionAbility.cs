using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Scriptables/Abilities/StaircaseInteractionAbility")]
public class StaircaseInteractionAbility : Ability
{
	[SerializeField] private LevelData targetLevel = null;

	[HideInInspector] public LevelData TargetLevel { get => targetLevel; }

	public override bool Cast(Creature caster, Tile target)
	{
		if (targetLevel)
		{
			GameController.Instance.WorldController.LevelGenerator.MoveBetweenLevels(targetLevel, GameController.Instance.WorldController.TurnHandler.CurrentCreature);
			return true;
		}
		return false;
	}
}
