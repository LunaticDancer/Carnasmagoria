using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles everything related to User Interface

public class UIController : MonoBehaviour
{
    [HideInInspector]
    public GameController Controller = null;

    [SerializeField]
    private MainMenuView mainMenuView = null;

    public void Init(GameController controller)
    {
        Controller = controller;
        mainMenuView.Init(this);
    }

    public void SetMainMenuVisibility(bool state)
    {
        mainMenuView.gameObject.SetActive(state);
    }
}
