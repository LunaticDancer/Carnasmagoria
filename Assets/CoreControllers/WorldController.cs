using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The gameplay master script

public class WorldController : MonoBehaviour
{
    [HideInInspector]
    public GameController Controller = null;

    public void Init(GameController controller)
    {
        Controller = controller;
    }

    public void PrepareWorld()
    {

    }
}
