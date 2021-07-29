using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForward : MonoBehaviour
{
    private bool _isForwardClear = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            _isForwardClear = false;
        }

        //if(other.gameObject.TryGetComponent(out Enemy enemy))
        //{
        //    transform.parent.GetComponent<AnimationState>().OnHit();
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            _isForwardClear = true;
        }
    }

    public bool IsForwardClearing()
    {
        return _isForwardClear;
    }
}
