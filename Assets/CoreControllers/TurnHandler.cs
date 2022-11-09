using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    [HideInInspector]
    public WorldController Controller = null;

    public InputHandler InputHandler = null;

    private List<Creature> turnTakers = new List<Creature>();
    private Creature currentCreature = null;

    public void Init(WorldController controller)
    {
        Controller = controller;
        InputHandler.Init(this);
    }

    public void UpdateTurnFlow()
    {
        if (currentCreature)
        {
            if (currentCreature.IsUnderPlayerControl && currentCreature.TurnTimer <= 0)
            {
                InputHandler.HandlePlayerInput();
            }
            else
            {
                ActivateCreatureWithNearestTurn();
            }
        }
        else
        {
            ActivateCreatureWithNearestTurn();
        }
    }

    private void ActivateCreatureWithNearestTurn()
    {
        currentCreature = FindCreatureWithNearestTurn();
        foreach (Creature creature in turnTakers)
        {
            creature.LowerTurnTimer(currentCreature.TurnTimer);
        }
        currentCreature.TakeTurn();
    }

    private Creature FindCreatureWithNearestTurn()
    {
        if (turnTakers.Count == 0)
        {
            return null;
        }
        else
        {
            float shortestTimer = 999;
            Creature result = null;
            foreach (Creature creature in turnTakers)
            {
                if (creature.TurnTimer < shortestTimer)
                {
                    result = creature;
                }
            }
            return result;
        }
    }

    public void AddCreature(Creature creature)
    {
        turnTakers.Add(creature);
    }

    public void RemoveCreature(Creature creature)
    {
        turnTakers.Remove(creature);
    }

    public List<Creature> GetPlayerVisionSources()
    {
        List<Creature> result = new List<Creature>();
        foreach (Creature creature in turnTakers)
        {
            if (creature.IsUnderPlayerControl)
            {
                result.Add(creature);
            }
        }
        return result;
    }
}
