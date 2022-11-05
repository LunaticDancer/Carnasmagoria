using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject UIControllerPrefab = null;

    [HideInInspector]
    public UIController UIController = null;

    void Start()
    {
        UIController = Instantiate(UIControllerPrefab, this.transform).GetComponent<UIController>();

        UIController.SetMainMenuVisibility(true);
    }
}
