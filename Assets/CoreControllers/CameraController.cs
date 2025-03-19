using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    private Transform objectToFollow = null;
    [SerializeField]
    private float followLerpSpeed = 1f;

	private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        Instance = this;
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
