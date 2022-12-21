using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelX", menuName = "Scriptables/LevelData")]
public class LevelData : ScriptableObject
{
    public enum GenerationTypes
    {
        MarchingSquare,
        Perlin
    }

    [System.Serializable] public class EnemyOdds
    {
        public GameObject spawnablePrefab;
        public int frequencyModifier;
        public int generationCost;
    }

    public Vector2Int LevelSize = new Vector2Int(50, 50);
    [Range(0.01f, 0.99f)]
    public float levelFillPercent = 0.5f;
    public GenerationTypes GenerationType = GenerationTypes.MarchingSquare;
    public GameObject DefaultWallPrefab = null;
    public int MonsterBudget = 50;
    public EnemyOdds[] EnemySpawningOdds = null;

    public Color BackgroundColor;
    public Color FloorColor;
}
