using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelX", menuName = "Scriptables/LevelData")]
public class LevelData : ScriptableObject
{
    public enum GenerationTypes
    {
        MarchingSquareCaves20Percent,
        MarchingSquareCaves30Percent,
        MarchingSquareCaves40Percent,
        MarchingSquareCaves50Percent,
        PerlinOpenSpace,
        PerlinDestructibleCaves
    }

    [System.Serializable] public class EnemyOdds
    {
        public GameObject spawnablePrefab;
        public int frequencyModifier;
        public int generationCost;
    }

    public Vector2Int LevelSize = new Vector2Int(50, 50);
    public GenerationTypes GenerationType = GenerationTypes.MarchingSquareCaves50Percent;
    public GameObject DefaultWallPrefab = null;
    public int monsterBudget = 50;
    public EnemyOdds[] EnemySpawningOdds = null;

}
