using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
	public static EventHandler Instance;

	public delegate void AbilityUseCallback(Ability.AbilityTriggers trigger, Entity entity);
	public event AbilityUseCallback OnAbilityUsed;

    void Start()
    {
        // making sure there are no duplicates of the Singleton
        if (Instance)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void SignalAbilityUsed(Ability.AbilityTriggers trigger, Entity entity)
    {
        if (OnAbilityUsed == null)
            return;

        OnAbilityUsed(trigger, entity);
	}
}
