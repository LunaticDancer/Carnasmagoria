using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Scriptables/Abilities/VisionAbility")]
public class VisionAbility : Ability
{


	public override bool Cast(Creature caster, Tile target)
	{
		caster.SetVisionRange(range, false);
		return true;
	}
}
