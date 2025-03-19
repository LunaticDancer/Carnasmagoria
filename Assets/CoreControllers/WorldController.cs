using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The gameplay master script

public class WorldController : MonoBehaviour
{
    public static WorldController Instance;

    [HideInInspector]
    public GameController Controller = null;

    private int currentLevel = 0;
    public bool isThePlayerAlive = false;

    public TileGridController TileGridController = null;
    public LevelGenerator LevelGenerator = null;
    public TurnHandler TurnHandler = null;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void Init()
    {
        TileGridController.Init(this);
        LevelGenerator.Init(this);
        TurnHandler.Init(this);
    }

    public void PrepareWorld()
    {
        LevelGenerator.StartFromScratchInLevel(LevelGenerator.startingLevel);
        isThePlayerAlive = true;
    }
}
