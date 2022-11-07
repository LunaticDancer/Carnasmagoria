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
        bool[,] layout = new bool[1,1]; // true = floor, false = wall

        if (levels[targetLevel].GenerationType == LevelData.GenerationTypes.MarchingSquareCavesHighPercent)
        {
            layout = GenerateWithMarchingSquare(levels[targetLevel].LevelSize, 0.5f);
        }

        Controller.TileGridController.FillWithLayout(layout, levels[targetLevel].DefaultWallPrefab);
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
                    if(currentX < size.x - 1)
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
