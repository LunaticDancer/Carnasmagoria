using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles everything related to User Interface

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField]
    private MainMenuView mainMenuView = null;

	private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

	public void Init()
    {
        mainMenuView.Init(this);
    }

    public void SetMainMenuVisibility(bool state)
    {
        mainMenuView.gameObject.SetActive(state);
    }
}
