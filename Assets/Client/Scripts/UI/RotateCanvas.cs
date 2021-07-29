using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateCanvas : MonoBehaviour
{
    [SerializeField] private bool isPlayer = false;
    [SerializeField] private Image image;
    private Vector3 offset;
    [SerializeField] private Transform target;

    private Canvas canvas;
    private Camera cam;

    private void Start()
    {
        canvas = GetComponent<Canvas>();

        Vector3 centerPosition = new Vector3(-Screen.width/ 2f,- Screen.height / 2f , 0);
        //offset = new Vector3(Screen.width / 50f, Screen.height / 25f, 0) + centerPosition;
        offset = new Vector3(Screen.width / 25f, Screen.height / 20f, 0) + centerPosition;

        if (isPlayer == false)
        {
            //StartCoroutine(TimerDisable());
            if (image != null)
                image.transform.position = worldToUISpace(canvas, target.position) + new Vector3(0, Screen.height / 35f, 0);
        }
    }

    private void LateUpdate()
    {
        //////////Quaternion rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position, Camera.main.transform.up);
        //////////transform.rotation = rotation;

        //////////transform.rotation = Quaternion.Euler(-transform.localRotation.eulerAngles.x, -180f, 0);
        //////////transform.Rotate(0, -180, 0);
        if (isPlayer)
        {
            //cam = Camera.main;
            //Vector3 point = cam.WorldToScreenPoint(target.position);
            //point += offset;
            //image.transform.position = point;
            image.transform.position = worldToUISpace(canvas, target.position) + offset;
        }
    }

    IEnumerator TimerDisable()
    {
        yield return new WaitForSeconds(0.25f);

        //transform.rotation = Quaternion.Euler(-transform.localRotation.eulerAngles.x, -180f, 0);
        //transform.Rotate(0, -180, 0);



        cam = Camera.main;
        Vector3 point = cam.WorldToScreenPoint(transform.parent.transform.position);
        point += offset;


        if(image != null)
            image.transform.position = point;


        GetComponent<RotateCanvas>().enabled = false;
    }

    private Vector3 worldToUISpace(Canvas parentCanvas, Vector3 worldPos)
    {
        //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector2 movePos;

        //Convert the screenpoint to ui rectangle local point
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
        //Convert the local point to world point
        return parentCanvas.transform.TransformPoint(movePos);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
