using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Entity
{
    [SerializeField] private float mass = 1;

    [HideInInspector] public float Mass { get => mass; }
}
