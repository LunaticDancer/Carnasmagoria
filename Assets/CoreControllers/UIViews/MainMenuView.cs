using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuView : MonoBehaviour
{
    [HideInInspector]
    public UIController Controller = null;

    public void Init(UIController controller)
    {
        Controller = controller;
    }

    public void OnStartPressed()
    {
        GameController.Instance.StartGame();
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }
}
