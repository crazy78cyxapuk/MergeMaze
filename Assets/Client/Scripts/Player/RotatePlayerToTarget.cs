using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayerToTarget : MonoBehaviour
{
    private Vector3 _target;
    private Quaternion _look;
    private float _timer = 0;

    private void OnEnable()
    {
        _target = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        _look = Quaternion.LookRotation(_target - transform.position);
    }

    private void Update()
    {
        if (_target != null)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _look, _timer);
            _timer += Time.deltaTime * 1.5f;
        }
    }
}
