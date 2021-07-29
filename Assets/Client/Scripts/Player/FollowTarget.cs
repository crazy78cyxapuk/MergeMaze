using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private Transform _target;

    [SerializeField] private Vector3 offset;

    void Update()
    {
        if (_target != null)
        {
            transform.position = _target.position + offset;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        _target = newTarget;
    }
}
