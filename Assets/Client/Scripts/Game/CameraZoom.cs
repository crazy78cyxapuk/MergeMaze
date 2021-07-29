using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Transform _target;
    private float _timer = 0;

    [Header("Расстояние между таргетом и камерой")]
    [SerializeField] private float _distance;

    private void Update()
    {
        if (_target != null)
        {
            if (Vector3.Distance(transform.position, _target.position) > _distance)
            {
                transform.position = Vector3.Lerp(transform.position, _target.position, _timer);

                //Quaternion rotation = Quaternion.LookRotation(_target.position - transform.position, transform.forward);
                //transform.rotation = rotation;

                _timer += Time.deltaTime / 5f;
            }
        }
    }

    public void SetTarget(Transform newTarget)
    {
        _target = newTarget;
    }
}
