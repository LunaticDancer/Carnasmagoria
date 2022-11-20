using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background = null;
    [SerializeField] private UnityEngine.UI.Text characterDisplay = null;
    [SerializeField] private GameObject visionBlockBox = null;
    private bool blocksVision = false;
    private bool blocksMovement = false;
    private bool wasUncovered = false;

    private List<Entity> entities = new List<Entity>();
    private Vector2Int gridPosition;

    public List<Entity> Entities { get => entities; }
    public Vector2Int GridPosition { get => gridPosition; }
    public bool BlocksVision { get => blocksVision; }
    public bool BlocksMovement { get => blocksMovement; }

    public void Init(Vector2Int position)
    {
        gridPosition = position;
        SetColor(GameController.Instance.WorldController.LevelGenerator.currentLevel.BackgroundColor);
        SetCharacterColor(GameController.Instance.WorldController.LevelGenerator.currentLevel.BackgroundColor);
        UpdateVisuals();
    }

	public void UpdateVisuals()
	{
        if (GameController.Instance.WorldController.TileGridController.IsTileInPlayerVision(this))
        {
            wasUncovered = true;
            Entity priorityEntity = FindHighestRenderingPriorityEntity();
            if (priorityEntity)
            {
                SetCharacter(priorityEntity.Symbol);
                SetColor(priorityEntity.BackgroundColor);
                SetCharacterColor(priorityEntity.SymbolColor);
            }
            else
            {
                SetAsRandomGroundTile();
            }
        }
        else
        {
            if (wasUncovered)
            {
                SetColor(new Color(background.color.grayscale, background.color.grayscale, background.color.grayscale));
                SetCharacterColor(new Color(characterDisplay.color.grayscale, characterDisplay.color.grayscale, characterDisplay.color.grayscale));
            }
        }
	}

    public void SetAsRandomGroundTile()
    {
        int decideCharacter = Random.Range(0, 13);
        switch (decideCharacter)
        {
            case 0:
                SetCharacter(':');
                break;
            case 1:
                SetCharacter(',');
                break;
            case 2:
                SetCharacter('*');
                break;
            default:
                SetCharacter('.');
                break;
        }
        SetColor(GameController.Instance.WorldController.LevelGenerator.currentLevel.FloorColor);
        SetCharacterColor(new Color(0.5f, 0.25f, 0.25f));
    }

    private Entity FindHighestRenderingPriorityEntity()
    {
        Entity result = null;

        if (entities.Count > 0)
        {
            int currentPriority = -9999;
            foreach (Entity entity in entities)
            {
                if (entity.RenderPriority > currentPriority)
                {
                    result = entity;
                }
            }
        }

        return result;
    }

	public void SetCharacter(char character)
    {
        characterDisplay.text = ""+character; // funky char to string conversion
    }

    public void SetCharacterColor(Color newColor)
    {
        characterDisplay.color = newColor;
    }

    public void SetColor(Color newColor)
    {
        if (newColor == Color.magenta)
        {
            background.color = new Color(0.7490196f, 0.3745098f, 0.3745098f, 0); // magenta = transparent tile
        }
        else
        {
            background.color = newColor;
        }
    }

    public void AttachEntity(Entity newEntity)
    {
        entities.Add(newEntity);
        blocksVision = CheckIfBlocksVision();
        SetVisionBlockerActive(blocksVision);
        blocksMovement = CheckIfBlocksMovement();
    }
    public void DettachEntity(Entity entity)
    {
        entities.Remove(entity);
        blocksVision = CheckIfBlocksVision();
        SetVisionBlockerActive(blocksVision);
        blocksMovement = CheckIfBlocksMovement();
    }

    private bool CheckIfBlocksVision()
    {
        if (System.Array.Exists(entities.ToArray(), element => element.IsVisionObstacle))
        {
            return true;
        }
        return false;
    }

    private bool CheckIfBlocksMovement()
    {
        if (System.Array.Exists(entities.ToArray(), element => !element.IsPassable))
        {
            return true;
        }
        return false;
    }

    public void SetVisionBlockerActive(bool state)
    {
        visionBlockBox.SetActive(state);
    }
}
