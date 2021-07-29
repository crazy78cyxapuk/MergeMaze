using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GA : MonoBehaviour
{
    public static GA instance;

    private void Awake()
    {
        instance = this;

        GameAnalytics.Initialize();

        DontDestroyOnLoad(this);
    }

    public void OnLevelComplete(string _level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, _level);
    }

    public void OnLevelFailed(string _level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, _level);
    }

    public void OnLevelStart(string _level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, _level);
    }
}
