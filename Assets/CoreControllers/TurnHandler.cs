using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    [HideInInspector]
    public WorldController Controller = null;

    public void Init(WorldController controller)
    {
        Controller = controller;
    }
}
