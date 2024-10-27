using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    void Start()
    {
        _initialXPosition = transform.position.x;
    }

    void Update()
    {
        float newX = _initialXPosition + (Mathf.Sin(Time.time * _movementSpeed) * _moveDistance);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private float _movementSpeed = 1.5f;
    private float _moveDistance = 8f;
    private float _initialXPosition;
}
