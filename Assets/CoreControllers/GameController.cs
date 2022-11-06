using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// The highest script in the hierarchy

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField]
    private GameObject UIControllerPrefab = null;
    [HideInInspector]
    public UIController UIController = null;

    [SerializeField]
    private GameObject DataControllerPrefab = null;
    [HideInInspector]
    public DataController DataController = null;

    [SerializeField]
    private GameObject WorldControllerPrefab = null;
    [HideInInspector]
    public WorldController WorldController = null;

    void Start()
    {
        // making sure there are no duplicates of the Singleton
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DataController = Instantiate(DataControllerPrefab, transform).GetComponent<DataController>();
        UIController = Instantiate(UIControllerPrefab, transform).GetComponent<UIController>();
        WorldController = Instantiate(WorldControllerPrefab, transform).GetComponent<WorldController>();

        DataController.Init(this);
        UIController.Init(this);
        WorldController.Init(this);

        UIController.SetMainMenuVisibility(true);
    }

    public void StartGame()
    {
        UIController.SetMainMenuVisibility(false);
        WorldController.PrepareWorld();
    }
}
