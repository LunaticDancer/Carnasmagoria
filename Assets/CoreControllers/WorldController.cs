using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The gameplay master script

public class WorldController : MonoBehaviour
{
    [HideInInspector]
    public GameController Controller = null;

    private int currentLevel = 0;
    public bool isThePlayerAlive = false;

    public TileGridController TileGridController = null;
    public LevelGenerator LevelGenerator = null;
    public TurnHandler TurnHandler = null;

    public void Init(GameController controller)
    {
        Controller = controller;
        TileGridController.Init(this);
        LevelGenerator.Init(this);
        TurnHandler.Init(this);
    }

    public void PrepareWorld()
    {
        LevelGenerator.GenerateLevelLayout(currentLevel);
        isThePlayerAlive = true;
    }
}
