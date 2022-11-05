using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles saving and loading data

public class DataController : MonoBehaviour
{
    [HideInInspector]
    public GameController Controller = null;

    public void Init(GameController controller)
    {
        Controller = controller;
    }
}
