using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles saving and loading data

public class DataController : MonoBehaviour
{
    public static DataController Instance;

    [HideInInspector]
    public GameController Controller = null;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }
}
