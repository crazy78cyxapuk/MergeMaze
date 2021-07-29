using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeToMove : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            gameObject.SetActive(false);
        }
    }
}
