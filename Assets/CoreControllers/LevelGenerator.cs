using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [HideInInspector]
    public WorldController Controller = null;

    [SerializeField] private GameObject playerPrefab = null;
    public LevelData startingLevel = null;
    public LevelData currentLevel = null;

    public void Init(WorldController controller)
    {
        Controller = controller;
    }

    public void GenerateLevelLayout(LevelData level)
    {
        bool[,] layout = new bool[1,1]; // true = floor, false = wall
        currentLevel = level;
        Camera.main.backgroundColor = level.BackgroundColor;
        Vector2Int playerSpawnCoordinates = new Vector2Int(Mathf.FloorToInt(level.LevelSize.x / 2),
            Mathf.FloorToInt(level.LevelSize.x / 2));

        if (level.GenerationType == LevelData.GenerationTypes.MarchingSquare)
        {
            layout = GenerateWithMarchingSquare(level.LevelSize, level.levelFillPercent);
        }
        else if (level.GenerationType == LevelData.GenerationTypes.Perlin)
        {
            layout = GenerateWithPerlinNoise(level.LevelSize, level.levelFillPercent, 0.1f);
            while (!layout[playerSpawnCoordinates.x, playerSpawnCoordinates.y]) // TODO: make the position correction less hacky (spiral check?)
            {
                playerSpawnCoordinates = new Vector2Int(Random.Range(0, level.LevelSize.x), Random.Range(0, level.LevelSize.y));
            }
        }

        Controller.TileGridController.FillWithLayout(layout, level.DefaultWallPrefab);

        Controller.TileGridController.SpawnCreature(playerSpawnCoordinates, playerPrefab);
        Controller.TileGridController.UpdateGridVisuals();
    }

    public bool[,] GenerateWithPerlinNoise(Vector2Int size, float emptinessRatio, float scale)
    {
        float perlinConstant = Time.realtimeSinceStartup * 2.137f;
        bool[,] result = new bool[size.x, size.y];

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if (Mathf.PerlinNoise((x * scale)+perlinConstant, (y*scale)+perlinConstant) < emptinessRatio)
                {
                    result[x, y] = true;
                }
            }
        }


        return result;
    }

    public bool[,] GenerateWithMarchingSquare(Vector2Int size, float emptinessRatio)
    {
        bool[,] result = new bool[size.x, size.y];
        int requiredFloorTiles = Mathf.FloorToInt(size.x * size.y * emptinessRatio);
        int safetyTurnCounter = size.x * size.y * 2;
        int turnsTaken = 0;
        int currentFloorTiles = 0;

        int currentX = Mathf.FloorToInt(size.x / 2);
        int currentY = Mathf.FloorToInt(size.y / 2);
        result[currentX, currentY] = true;

        while (currentFloorTiles < requiredFloorTiles && turnsTaken < safetyTurnCounter)
        {
            int direction = Random.Range(0, 4);
            switch (direction) // move by one tile
            {
                case 0:
                    if (currentX < size.x - 1)
                        currentX++;
                    break;
                case 1:
                    if (currentX > 0)
                        currentX--;
                    break;
                case 2:
                    if (currentY < size.y - 1)
                        currentY++;
                    break;
                default:
                    if (currentY > 0)
                        currentY--;
                    break;
            }

            if (!result[currentX, currentY]) // turn a wall into a floor if legal
            {
                result[currentX, currentY] = true;
                currentFloorTiles++;
            }
            turnsTaken++;
        }

        return result;
    }

}
