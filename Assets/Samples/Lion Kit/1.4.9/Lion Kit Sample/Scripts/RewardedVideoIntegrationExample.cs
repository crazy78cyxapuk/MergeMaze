using UnityEngine;
using LionStudios;
using LionStudios.Ads;

public class RewardedVideoIntegrationExample : MonoBehaviour
{
    public int playerLevel = 1;

    // Hook this up through buttons or directly in code
    // ===========================================================
    public void ShowRewardedVideo()
    {
        _ShowRewardedAdRequest.SetLevel(playerLevel);
        RewardedAd.Show(_ShowRewardedAdRequest);
    }
    // ===========================================================

    private void Start()
    {
        // Create show ad request when initializing
        _ShowRewardedAdRequest = new ShowAdRequest();

        // Ad event callbacks
        _ShowRewardedAdRequest.OnDisplayed += adUnitId => Debug.Log("Displayed Rewarded Ad :: Ad Unit ID = " + adUnitId);
        _ShowRewardedAdRequest.OnClicked += adUnitId => Debug.Log("Clicked Rewarded Ad :: Ad Unit ID = " + adUnitId);
        _ShowRewardedAdRequest.OnHidden += adUnitId => Debug.Log("Closed Rewarded Ad :: Ad Unit ID = " + adUnitId);
        _ShowRewardedAdRequest.OnFailedToDisplay += (adUnitId, error) => Debug.LogError("Failed To Display Rewarded Ad :: Error = " + error + " :: Ad Unit ID = " + adUnitId);
        _ShowRewardedAdRequest.OnReceivedReward += (adUnitId, reward) => Debug.Log("Received Reward :: Reward = " + reward + " :: Ad Unit ID = " + adUnitId);

        // Analytics settings and data
        _ShowRewardedAdRequest.sendAnalyticsEvents = true; // Defaults to true
        _ShowRewardedAdRequest.SetPlacement("rewarded_video_example");
        _ShowRewardedAdRequest.SetLevel(42); // This will be set prior to “showing” the ad as well.
        _ShowRewardedAdRequest.SetEventParam("custom_param", "custom_param_value");
    }

    ShowAdRequest _ShowRewardedAdRequest;
}