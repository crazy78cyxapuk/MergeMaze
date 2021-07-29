using UnityEngine;
using LionStudios;
using LionStudios.Debugging;
using LionStudios.Ads;
using UnityEngine.Analytics;
using ShowAdRequest = LionStudios.Ads.ShowAdRequest;

public class AdIntegration : LionMenu
{
    // Hooked up through buttons
    // ===========================================================
    public void ShowInterstitial()
    {
        Debug.Log("Clicked Show Interstitial Button");
        // NOTE:  We are passing our level when showing the interstitial.
        // This allows our level to automatically be logged when showing ads.
        // It also prevents ads from showing if our level is below the LionSettings.InterstitialStartLevel
        LionStudios.Ads.Interstitial.Show(_ShowInterstitialRequest, _Level++);
    }

    public void ShowBanner()
    {
        Debug.Log("Clicked Show Banner Button");
        Banner.Show(_ShowBannerCallbacks);
    }

    public void HideBanner()
    {
        Debug.Log("Clicked Hide Banner Button");
        Banner.Hide();
    }

    // ===========================================================
    private void Start()
    {
        SetupCallbacks();
    }

    /// <summary>
    /// - Callback setup - 
    /// This is only necessary if you want callbacks on interstitial or banner ads
    /// Most of the time you will not need these callbacks.
    /// </summary>
    void SetupCallbacks()
    {
        SetupInterstitialCallbacks();
        SetupBannerAdCallbacks();
    }

    void SetupInterstitialCallbacks()
    {
        // Create callbacks for interstitial ads
        _ShowInterstitialRequest = new ShowAdRequest();
        _ShowInterstitialRequest.SetPlacement("between_levels");
        _ShowInterstitialRequest.SetLevel(_Level);
        _ShowInterstitialRequest.OnDisplayed += adUnitID => LionDebug.Log("Displayed Interstitial Ad :: Ad Unit ID = " + adUnitID);
        _ShowInterstitialRequest.OnClicked += adUnitID => LionDebug.Log("Clicked Interstitial Ad :: Ad Unit ID = " + adUnitID);
        _ShowInterstitialRequest.OnHidden += adUnitID => LionDebug.Log("Closed Interstitial Ad :: Ad Unit ID = " + adUnitID);
        _ShowInterstitialRequest.OnFailedToDisplay += (adUnitID, error) => LionDebug.LogError("Failed To Display Interstitial Video :: Error = " + error + " :: Ad Unit ID = " + adUnitID);

    }

    void SetupBannerAdCallbacks()
    {
        // Create callbacks for banner ads
        _ShowBannerCallbacks = new ShowAdRequest();
        _ShowBannerCallbacks.OnDisplayed += adUnitID => LionDebug.Log("Displayed Banner Ad :: Ad Unit ID = " + adUnitID);
        _ShowBannerCallbacks.OnClicked += adUnitID => LionDebug.Log("Clicked Banner Ad :: Ad Unit ID = " + adUnitID);
        _ShowBannerCallbacks.OnHidden += adUnitID => LionDebug.Log("Closed Banner Ad :: Ad Unit ID = " + adUnitID);
        _ShowBannerCallbacks.OnFailedToDisplay += (adUnitID, error) => LionDebug.LogError("Failed To Display Banner Ad :: Error = " + error + " :: Ad Unit ID = " + adUnitID);
    }

    int _Level;
    ShowAdRequest _ShowInterstitialRequest;
    ShowAdRequest _ShowBannerCallbacks;
}
