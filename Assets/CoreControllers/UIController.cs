using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private MainMenuView mainMenuView = null;

    public void SetMainMenuVisibility(bool state)
    {
        mainMenuView.gameObject.SetActive(state);
    }
}
