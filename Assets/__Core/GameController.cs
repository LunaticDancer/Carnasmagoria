using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// The highest script in the hierarchy

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    // the start of the game's sequential execution cascade
    void Start()
    {
        // making sure there are no duplicates of the Singleton
        if (Instance)
        {
            Destroy(gameObject);
        }
        Instance = this;

        UIController.Instance.Init();
        WorldController.Instance.Init();

        UIController.Instance.SetMainMenuVisibility(true);
    }

	private void Update()
	{
        if (WorldController.Instance.isThePlayerAlive)
        {
            WorldController.Instance.TurnHandler.UpdateTurnFlow();
        }
	}

	public void StartGame()
    {
        UIController.Instance.SetMainMenuVisibility(false);
        WorldController.Instance.PrepareWorld();
    }
}
