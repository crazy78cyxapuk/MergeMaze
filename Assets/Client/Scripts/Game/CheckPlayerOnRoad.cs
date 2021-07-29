using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerOnRoad : MonoBehaviour
{
    [SerializeField] private List<GameObject> otherChecking = new List<GameObject>();

    [SerializeField] private RoadColor[] roadColors;

    [SerializeField] private int side;

    [SerializeField] private float timer;
    [SerializeField] private float timerForTurn;

    [SerializeField] private Renderer _renderer;

    [SerializeField] private Color targetColor;
    [SerializeField] private Color oldColor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TurnOtherChecking(false);
         
            if (otherChecking[0].activeSelf == true)
            {
                return;
            }

            if (timer != 0)
            {
                Debug.Log(gameObject.name);
                StopAllCoroutines();
                ///////////////StartCoroutine(TimerForOldMaterial());
            }

            StartCoroutine(TimerForTargetMaterial());
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        if (timer != 0)
    //        {
    //            TurnOtherChecking(true);
    //        }
    //    }
    //}

    IEnumerator TimerForTargetMaterial()
    {
        List<RoadColor> oldRoadColor = new List<RoadColor>();

        int maxLength = roadColors.Length - 1;

        for (int i = 0; i < roadColors.Length; i++)
        {
            switch (i)
            {
                case (4):
                    yield return new WaitForSeconds(timerForTurn);
                    StartCoroutine(TimerForOldMaterial(oldRoadColor));
                    oldRoadColor.Clear();
                    break;

                case (8):
                    yield return new WaitForSeconds(timerForTurn);
                    StartCoroutine(TimerForOldMaterial1(oldRoadColor));
                    oldRoadColor.Clear();
                    break;

                case (12):
                    yield return new WaitForSeconds(timerForTurn);
                    StartCoroutine(TimerForOldMaterial2(oldRoadColor));
                    oldRoadColor.Clear();
                    break;

                case (15):
                    yield return new WaitForSeconds(timerForTurn);
                    StartCoroutine(TimerForOldMaterial3(oldRoadColor));
                    oldRoadColor.Clear();
                    break;
            }

            //if (i == 4 || i == 8 || i == 12)
            //{
            //    yield return new WaitForSeconds(timerForTurn);

            //    //Debug.Log(i);
            //    //StartCoroutine(TimerForOldMaterial(oldRoadColor));
            //    //MaterialTargetColor newMatTargetColor = gameObject.AddComponent<MaterialTargetColor>();
            //    //newMatTargetColor.StartTargetOldColor(oldRoadColor, _renderer.material.color, oldColor, timer);

            //    StartCoroutine(TimerForOldMaterial(oldRoadColor));
            //    oldRoadColor.Clear();
            //}

            oldRoadColor.Add(roadColors[i]);
            roadColors[i].enabled = true;
            roadColors[i].SetTargetColor(_renderer.material.color, targetColor);

            //Debug.Log(oldRoadColor[oldRoadColor.Count - 1].gameObject.name);

            //if (i == roadColors.Length - 1)
            //{
            //    MaterialTargetColor newMatTargetColor = gameObject.AddComponent<MaterialTargetColor>();
            //    newMatTargetColor.StartTargetOldColor(oldRoadColor, _renderer.material.color, oldColor, timer);

            //    oldRoadColor.Clear();

            //    TurnOtherChecking(true);
            //}
        }
    }

    IEnumerator TimerForOldMaterial(List<RoadColor> oldRoadColor)
    {
        yield return new WaitForSeconds(timer);

        for (int i = 0; i < 4/*oldRoadColor.Count*/; i++)
        {
            //oldRoadColor[i].enabled = true;
            //oldRoadColor[i].SetTargetColor(_renderer.material.color, oldColor);
            roadColors[i].enabled = true;
            roadColors[i].SetTargetColor(_renderer.material.color, oldColor);
        }
    }

    IEnumerator TimerForOldMaterial1(List<RoadColor> oldRoadColor)
    {
        yield return new WaitForSeconds(timer);

        for (int i = 4; i < 8/*oldRoadColor.Count*/; i++)
        {
            //oldRoadColor[i].enabled = true;
            //oldRoadColor[i].SetTargetColor(_renderer.material.color, oldColor);
            roadColors[i].enabled = true;
            roadColors[i].SetTargetColor(_renderer.material.color, oldColor);
        }
    }

    IEnumerator TimerForOldMaterial2(List<RoadColor> oldRoadColor)
    {
        yield return new WaitForSeconds(timer);

        for (int i = 8; i < 12/*oldRoadColor.Count*/; i++)
        {
            //oldRoadColor[i].enabled = true;
            //oldRoadColor[i].SetTargetColor(_renderer.material.color, oldColor);
            roadColors[i].enabled = true;
            roadColors[i].SetTargetColor(_renderer.material.color, oldColor);
        }
    }

    IEnumerator TimerForOldMaterial3(List<RoadColor> oldRoadColor)
    {
        yield return new WaitForSeconds(timer);

        for (int i = 12; i < 16/*oldRoadColor.Count*/; i++)
        {
            //oldRoadColor[i].enabled = true;
            //oldRoadColor[i].SetTargetColor(_renderer.material.color, oldColor);
            roadColors[i].enabled = true;
            roadColors[i].SetTargetColor(_renderer.material.color, oldColor);
        }

        TurnOtherChecking(true);
    }

    private void TurnOtherChecking(bool isTurn)
    {
        for (int i = 0; i < otherChecking.Count; i++)
        {
            otherChecking[i].SetActive(isTurn);
        }
    }
}
