using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// The highest script in the hierarchy

public class GameController : MonoBehaviour
{
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
        DataController = Instantiate(DataControllerPrefab, this.transform).GetComponent<DataController>();
        UIController = Instantiate(UIControllerPrefab, this.transform).GetComponent<UIController>();
        WorldController = Instantiate(WorldControllerPrefab, this.transform).GetComponent<WorldController>();

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
