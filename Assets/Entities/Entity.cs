using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the topmost parent of all game actors

public class Entity : MonoBehaviour
{
    [Header("Basic Interactability Properties")]
    private Tile currentTile = null;
    [SerializeField] private bool isDestructible = true;
    [SerializeField] private bool isVisible = true;
    [SerializeField] private bool isVisionObstacle = false;
    [SerializeField] private bool isPassable = true;
    [SerializeField] private bool isOrganic = true;
    [Header("Display Properties")]
    [SerializeField] private int renderPriority = 0;
    [SerializeField] private string nameLabel = "Entity";
    [SerializeField] [TextArea(3, 20)] private string flavorDescription = "";
    [SerializeField] private char symbol = '#';
    [SerializeField] private Color symbolColor = Color.red;
    [SerializeField] private Color backgroundColor = Color.magenta;     // magenta = no color, would be null if the Color class was nullable

    [HideInInspector] public Tile CurrentTile { get => currentTile; }
    [HideInInspector] public bool IsDestructible { get => isDestructible; }
    [HideInInspector] public bool IsVisible { get => isVisible; }
    [HideInInspector] public bool IsVisionObstacle { get => isVisionObstacle; }
    [HideInInspector] public bool IsPassable { get => isPassable; }
    [HideInInspector] public bool IsOrganic { get => isOrganic; }
    [HideInInspector] public int RenderPriority { get => renderPriority; }
    [HideInInspector] public string NameLabel { get => name; }
    [HideInInspector] public char Symbol { get => symbol; }
    [HideInInspector] public Color SymbolColor { get => symbolColor; }
    [HideInInspector] public Color BackgroundColor { get => backgroundColor; }

    public void SetTile(Tile newTile)
    {
        currentTile = newTile;
        transform.parent = newTile.transform;
        transform.position = newTile.transform.position;
    }
}
