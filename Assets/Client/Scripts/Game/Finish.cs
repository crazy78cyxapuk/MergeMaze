using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private bool isFinish = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out CheckInput checkInput) && isFinish == false)
        {
            isFinish = true;
            checkInput.EndGame();
            Camera.main.GetComponent<UIController>().GameWin();

            CameraZoom cameraZoom = Camera.main.GetComponent<CameraZoom>();
            cameraZoom.enabled = true;
            cameraZoom.SetTarget(other.gameObject.transform);
        }
    }
}
