using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background = null;
    [SerializeField] private UnityEngine.UI.Text characterDisplay = null;

    private List<Entity> entities = new List<Entity>();

    public List<Entity> Entities { get => entities; }

    public void Init()
    {
        UpdateVisuals();
    }

	public void UpdateVisuals()
	{
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

    public void SetAsRandomGroundTile()
    {
        SetCharacter('.');
        SetColor(new Color(1f, 0.5f, 0.5f));
        SetCharacterColor(new Color(0.5f, 0, 0));
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
            background.color = new Color(0,0,0,0); // magenta = transparent tile
        }
        else
        {
            background.color = newColor;
        }
    }

    public void AttachEntity(Entity newEntity)
    {
        entities.Add(newEntity);
    }
}
