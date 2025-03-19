using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public enum InputStates // was tempted to handle this with a state machine, but decided it would be an overkill
    {
        Idle,
        Aiming
    }

    [HideInInspector]
    public TurnHandler Controller = null;

    private InputStates inputState = InputStates.Idle;
    private Creature playerAbilityCaster;
    private Ability aimedAbility;

    public InputStates InputState { get => inputState; }

    public void Init(TurnHandler controller)
    {
        Controller = controller;
    }

    public void HandlePlayerInput()
    {
        if (inputState == InputStates.Aiming)
        {
            if (Input.GetButtonDown("Interact"))
            {
                Creature interactiveEntity = playerAbilityCaster.TryInteract();
                if (interactiveEntity)
                {
                    interactiveEntity.Interacted(playerAbilityCaster);
                }
                else
                {
                    Debug.Log("Nothing to interact with here.");
                }
            }
            else
            {
                Aim();
            }
        }
    }

    private void Aim()
    {
        if (aimedAbility.AimingMode == Ability.AimingModes.NoAiming)
        {
            aimedAbility.Cast(playerAbilityCaster, playerAbilityCaster.CurrentTile);
        }
        else if (aimedAbility.AimingMode == Ability.AimingModes.EightDirections)
        {
            if (Input.GetButtonDown("UpDirection"))
            {
                HandleEightDirectionAiming(new Vector2Int(0, 1));
            }
            else if (Input.GetButtonDown("DownDirection"))
            {
                HandleEightDirectionAiming(new Vector2Int(0, -1));
            }
            else if (Input.GetButtonDown("LeftDirection"))
            {
                HandleEightDirectionAiming(new Vector2Int(-1, 0));
            }
            else if (Input.GetButtonDown("RightDirection"))
            {
                HandleEightDirectionAiming(new Vector2Int(1, 0));
            }
        }
    }

    private void HandleEightDirectionAiming(Vector2Int aimVector)
    {
        Vector2Int TargetTileCoords = playerAbilityCaster.CurrentTile.GridPosition;
        int spaceCounter = 0;
        while (spaceCounter < aimedAbility.Range)
        {
            if (WorldController.Instance.TileGridController.IsPointLegalToCast(TargetTileCoords + aimVector, aimedAbility.CaresForVision, aimedAbility.CaresForCollision))
            {
                TargetTileCoords += aimVector;
            }
            else
            {
                break;
            }
            spaceCounter++;
        }
        aimedAbility.Cast(playerAbilityCaster, WorldController.Instance.TileGridController.TileArray[TargetTileCoords.x, TargetTileCoords.y]);
        inputState = InputStates.Idle;
    }

    public void StartAiming(Creature caster, Ability ability)
    {
        playerAbilityCaster = caster;
        aimedAbility = ability;
        inputState = InputStates.Aiming;
    }
}
