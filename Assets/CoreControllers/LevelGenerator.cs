using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [HideInInspector]
    public WorldController Controller = null;

    [SerializeField] private LevelData[] levels = null;

    public void Init(WorldController controller)
    {
        Controller = controller;
    }

    public void GenerateLevelLayout(int targetLevel)
    {
        bool[,] layout = new bool[levels[targetLevel].LevelSize.x, levels[targetLevel].LevelSize.y];

        // generation code here
        Controller.TileGridController.FillWithLayout(layout, levels[targetLevel].DefaultWallPrefab);
    }
}
