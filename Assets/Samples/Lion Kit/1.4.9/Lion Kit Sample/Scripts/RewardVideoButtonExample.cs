using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardVideoButtonExample : LionStudios.RewardedAdButton
{
    public override void OnReceivedReward(string adUnitId, MaxSdkBase.Reward reward)
    {
        base.OnReceivedReward(adUnitId, reward);
        Debug.Log("RewardVideoExample - OnReceivedReward");
    }

    public override void OnClose(string adUnitId)
    {
        base.OnClose(adUnitId);
        Debug.Log("RewardVideoExample - OnClose");
    }
}
