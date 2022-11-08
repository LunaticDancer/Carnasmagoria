using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [HideInInspector]
    public GameController Controller = null;

    private Transform objectToFollow = null;
    [SerializeField]
    private float followLerpSpeed = 1f;

    public void Init(GameController controller)
    {
        Controller = controller;
    }

	private void Update()
	{
        if (objectToFollow)
        {
            transform.position = Vector3.Lerp(transform.position, objectToFollow.position, followLerpSpeed*Time.deltaTime);
        }
	}

    public void SetFollowTarget(Transform transform)
    {
        objectToFollow = transform;
    }
}
