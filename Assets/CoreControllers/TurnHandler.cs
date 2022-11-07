using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    [HideInInspector]
    public WorldController Controller = null;

    private List<Creature> turnTakers = new List<Creature>();

    public void Init(WorldController controller)
    {
        Controller = controller;
    }

    public void AddCreature(Creature creature)
    {
        turnTakers.Add(creature);
    }

    public void RemoveCreature(Creature creature)
    {
        turnTakers.Remove(creature);
    }
}
