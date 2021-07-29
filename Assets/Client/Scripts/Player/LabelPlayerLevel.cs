using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelPlayerLevel : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] Vector3 offset;

    private void Start()
    {
        offset = new Vector3(0, 1.5f, 0.5f);
    }

    private void Update()
    {
        transform.position = target.position + offset;
    }

    public void SetTarget(Transform newTarget, Evolution evolution = default)
    {
        target = newTarget;

        int stage = target.gameObject.GetComponent<Evolution>().GetStage();

        switch (stage)
        {
            case (1):
                offset = new Vector3(0, 1.5f, 0.5f);
                break;

            case (2):
                offset = new Vector3(0, 2f, 0.5f);
                break;

            case (3):
                offset = new Vector3(0, 2.5f, 0.5f);
                break;

            case (4):
                offset = new Vector3(0, 2.5f, 0.5f);
                break;
        }
    }
}
