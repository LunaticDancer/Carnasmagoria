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
        SetAsRandomGroundTile();
	}

    public void SetAsRandomGroundTile()
    {
        SetCharacter('.');
        SetColor(new Color(1f, 0.5f, 0.5f));
        SetCharacterColor(new Color(0.5f, 0, 0));
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
}
