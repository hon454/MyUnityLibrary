using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _speed;
    
    public Vector3 Direction
    {
        get => _direction;
        set => _direction = value;
    }

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    private void FixedUpdate()
    {
        transform.Rotate(_direction * _speed);
    }
}
