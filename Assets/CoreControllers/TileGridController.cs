using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGridController : MonoBehaviour
{
    [HideInInspector]
    public WorldController Controller = null;

    [SerializeField] private GameObject tilePrefab = null;
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
    }

    public void AttachEntity(Vector2Int coordinates, Entity newEntity)
    {
        newEntity.SetTile(tileArray[coordinates.x, coordinates.y]);
        tileArray[coordinates.x, coordinates.y].AttachEntity(newEntity);
    }
}
