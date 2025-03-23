using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles saving and loading data

public class DataController : MonoBehaviour
{
    public static DataController Instance;

    [EnumNamedArray( typeof(ColorPalette) )]
    public Color[] BasicColorPalette;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public Color GetColor(ColorPalette index)
    {
        return BasicColorPalette[(int)index];
    }
}

[System.Serializable]
public enum ColorPalette
{
        NONE,
        MetalLight,
        MetalDark,
        BloodLight,
        BloodDark,
        Bone,
        FleshLight,
        FleshMedium,
        FleshDark,
        Darkness
}
