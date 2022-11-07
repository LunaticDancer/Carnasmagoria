using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Scriptables/Abilities/BasicMovementAbility")]
public class BasicMovementAbility : Ability
{
	public override bool Cast(Creature caster, Tile target)
	{
		GameController.Instance.WorldController.TileGridController.MoveEntity(target, caster);
		return true;
	}
}
