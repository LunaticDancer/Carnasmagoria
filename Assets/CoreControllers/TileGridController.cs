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
    private List<Creature> playerCameras = new List<Creature>();

    public Tile[,] TileArray { get => tileArray; }

    public void Init(WorldController controller)
    {
        Controller = controller;
    }

    public void UpdateGridVisuals()
    {
        playerCameras = Controller.TurnHandler.GetPlayerVisionSources();
        foreach (Tile tile in tileArray)
        {
            tile.UpdateVisuals();
        }
    }

    public bool IsTileInPlayerVision(Tile tile)
    {
        foreach (Creature source in playerCameras)
        {
            if (Vector2.Distance(tile.transform.position, source.transform.position) <= source.VisionRange)
            {
                return true;
            }
        }
        return false;
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
                tileArray[x, y].Init(new Vector2Int(x, y));
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
        spawned.Init();
        Controller.TurnHandler.AddCreature(spawned);
    }

    public void MoveEntity(Vector2Int newCoordinates, Entity entity)
    {
        entity.CurrentTile.DettachEntity(entity);
        AttachEntity(newCoordinates, entity);
    }

    public void MoveEntity(Tile newTile, Entity entity)
    {
        entity.CurrentTile.DettachEntity(entity);
        AttachEntity(newTile, entity);
    }

    public void AttachEntity(Vector2Int coordinates, Entity newEntity)
    {
        newEntity.SetTile(tileArray[coordinates.x, coordinates.y]);
        tileArray[coordinates.x, coordinates.y].AttachEntity(newEntity);
    }

    public void AttachEntity(Tile tile, Entity newEntity)
    {
        newEntity.SetTile(tile);
        tile.AttachEntity(newEntity);
    }

    public bool IsPointLegalToCast(Vector2Int coordinates, bool caresForVision, bool caresForCollision)
    {
        if (IsPointInBounds(coordinates))
        {
            if (caresForCollision && tileArray[coordinates.x, coordinates.y].BlocksMovement)
            {
                return false;
            }
            return true;
        }
        return false;
    }

    public bool IsPointInBounds(Vector2Int coordinates)
    {
        if (coordinates.x > 0) // handled as an if cascade to minimize the number of checks executed
        {
            if (coordinates.y > 0)
            {
                if (coordinates.x < tileArray.GetLength(0) - 1)
                {
                    if (coordinates.y < tileArray.GetLength(1) - 1)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
