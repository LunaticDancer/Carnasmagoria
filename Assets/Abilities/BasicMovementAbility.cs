using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Scriptables/Abilities/BasicMovementAbility")]
public class BasicMovementAbility : Ability
{
	public override bool Cast(Creature caster, Tile target)
	{
		WorldController.Instance.TileGridController.MoveEntity(target, caster);
		WorldController.Instance.TileGridController.UpdateGridVisuals();
		EventHandler.Instance.SignalAbilityUsed(AbilityTriggers.OnMove, caster);
		caster.ChangeTurnTimer(timeCost);
		return true;
	}
}
