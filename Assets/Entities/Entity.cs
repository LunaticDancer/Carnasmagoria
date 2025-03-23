using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the topmost parent of all game actors

public class Entity : MonoBehaviour
{
    [Header("Basic Interactability Properties")]
    private Tile currentTile = null;
    [SerializeField] private bool isDestructible = true;
    [SerializeField] protected bool isVisible = true;
    [SerializeField] protected bool isVisionObstacle = false;
    [SerializeField] private bool isPassable = true;
    [SerializeField] private bool isOrganic = true;
    [Header("Display Properties")]
    [SerializeField] private int renderPriority = 0;
    [SerializeField] private string nameLabel = "Entity";
    [SerializeField] [TextArea(3, 20)] private string flavorDescription = "";
    [SerializeField] private char symbol = '#';
    [SerializeField] private ColorPalette symbolColor = ColorPalette.BloodDark;
    [SerializeField] private ColorPalette backgroundColor = ColorPalette.NONE;

    [HideInInspector] public Tile CurrentTile { get => currentTile; }
    [HideInInspector] public bool IsDestructible { get => isDestructible; }
    [HideInInspector] public bool IsVisible { get => isVisible; }
    [HideInInspector] public bool IsVisionObstacle { get => isVisionObstacle; }
    [HideInInspector] public bool IsPassable { get => isPassable; }
    [HideInInspector] public bool IsOrganic { get => isOrganic; }
    [HideInInspector] public int RenderPriority { get => renderPriority; }
    [HideInInspector] public string NameLabel { get => name; }
    [HideInInspector] public char Symbol { get => symbol; }
    [HideInInspector] public ColorPalette SymbolColor { get => symbolColor; }
    [HideInInspector] public ColorPalette BackgroundColor { get => backgroundColor; }

    public void SetTile(Tile newTile)
    {
        currentTile = newTile;
        transform.parent = newTile.transform;
        transform.position = newTile.transform.position;
    }
}
