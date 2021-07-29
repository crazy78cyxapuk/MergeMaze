using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTargetColor : MonoBehaviour
{
    public void StartTargetOldColor(List<RoadColor> oldRoadColor, Color newColor, Color oldColor, float timer)
    {
        StartCoroutine(TargetOldMaterial(oldRoadColor, newColor, oldColor, timer));
    }

    IEnumerator TargetOldMaterial(List<RoadColor> oldRoadColor, Color newColor, Color oldColor, float timer)
    {
        yield return new WaitForSeconds(timer);

        for (int i = 0; i < oldRoadColor.Count; i++)
        {
            oldRoadColor[i].enabled = true;
            oldRoadColor[i].SetTargetColor(newColor, oldColor);

            if (i == oldRoadColor.Count - 1)
            {
                //Destroy(GetComponent<MaterialTargetColor>());
            }
        }





        //TurnOtherChecking(true);

        //int count = 0;

        //for(int i= 0; i < roadColors.Length ; i++)
        //{
        //    count += 1;

        //    if(count == 12 || count == 8 || count == 4)
        //    {
        //        yield return new WaitForSeconds(timerForTurn);
        //    }

        //    roadColors[i].enabled = true;
        //    roadColors[i].SetTargetColor(_renderer.material.color, oldColor);
        //}
    }
}
