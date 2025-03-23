using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Scriptables/Abilities/VisionAbility")]
public class VisionAbility : Ability
{
	public override void ReactToEvent(AbilityTriggers trigger, Entity entity)
	{
		if (trigger == AbilityTriggers.OnAttachThisBodyPart)
		{
			Cast(((BodyPart)entity).GetCreature(), null);
		}
	}

	public override bool Cast(Creature caster, Tile target)
	{
		if (caster == null)
			return false;

		caster.SetVisionRange(range, false);
		return true;
	}
}
