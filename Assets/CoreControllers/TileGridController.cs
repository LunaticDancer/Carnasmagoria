using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGridController : MonoBehaviour
{
    [HideInInspector]
    public WorldController Controller = null;

    [SerializeField] private GameObject tilePrefab = null;
    [SerializeField] private GameObject indestructibleWallPrefab = null;
    private Tile[,] tileArray = null;

    public Tile[,] TileArray { get => tileArray; }

    public void Init(WorldController controller)
    {
        Controller = controller;
    }

    public void FillWithLayout(bool[,] layout, GameObject wallPrefab)
    {
        tileArray = new Tile[layout.GetLength(0), layout.GetLength(1)];

        for (int x = 0; x < layout.GetLength(0); x++)
        {
            for (int y = 0; y < layout.GetLength(1); y++)
            {
                tileArray[x, y] = Instantiate(tilePrefab, transform).GetComponent<Tile>();
                tileArray[x, y].transform.position = new Vector2(x, y);
                if (!layout[x, y]) // if there's supposed to be a wall on this tile, create a wall
                {
                    AttachEntity(new Vector2Int(x, y), Instantiate(wallPrefab).GetComponent<Entity>());
                }
                tileArray[x, y].Init();
            }
        }
        CreateIndestructibleEdges();
    }

    private void CreateIndestructibleEdges()
    {
        int extremeX = tileArray.GetLength(0);
        int extremeY = tileArray.GetLength(1);
        for (int x = 0; x < extremeX; x++)
        {
            AttachEntity(new Vector2Int(x, 0), Instantiate(indestructibleWallPrefab).GetComponent<Entity>());
            tileArray[x, 0].UpdateVisuals();
            AttachEntity(new Vector2Int(x, extremeY-1), Instantiate(indestructibleWallPrefab).GetComponent<Entity>());
            tileArray[x, extremeY-1].UpdateVisuals();
        }
        extremeY--; // the goal of this is to prevent repeated "extremeY - 1" operations in the loop, since the variable will not be used anymore either way
        for (int y = 1; y < extremeY; y++)
        {
            AttachEntity(new Vector2Int(0, y), Instantiate(indestructibleWallPrefab).GetComponent<Entity>());
            tileArray[0, y].UpdateVisuals();
            AttachEntity(new Vector2Int(extremeX-1, y), Instantiate(indestructibleWallPrefab).GetComponent<Entity>());
            tileArray[extremeX-1, y].UpdateVisuals();
        }
    }

    public void SpawnCreature(Vector2Int coordinates, GameObject creature)
    {
        Creature spawned = Instantiate(creature).GetComponent<Creature>();
        AttachEntity(coordinates, spawned);
        Controller.TurnHandler.AddCreature(spawned);
    }

    public void AttachEntity(Vector2Int coordinates, Entity newEntity)
    {
        newEntity.SetTile(tileArray[coordinates.x, coordinates.y]);
        tileArray[coordinates.x, coordinates.y].AttachEntity(newEntity);
    }
}
