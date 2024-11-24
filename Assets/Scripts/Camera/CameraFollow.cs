using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    void Update()
    {
        if (_transformToFollow == null)
        {
            return;
        }
        Vector3 targetPosition = new Vector3(_transformToFollow.position.x, _offset.y, _offset.z);
        targetPosition.x = Mathf.Clamp(targetPosition.x, _xMinLimit, _xMaxLimit);
        transform.position = targetPosition;
    }

    [SerializeField] private Transform _transformToFollow;
    private Vector3 _offset = new Vector3(0f, 0f, -10f);
    private float _xMinLimit = -28.5f;
    private float _xMaxLimit = 28.5f;
}
