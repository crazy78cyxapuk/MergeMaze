using UnityEngine;
using UnityEngine.UI;
using LionStudios;
using System.Collections.Generic;
using LionStudios.Debugging;

public class MainMenu : MonoBehaviour
{
    EventTrackingMenu _EventTracking;
    RegularAds _RegularAds;
    DebuggingMenu _Debugger;
    ExampleIAPStore _IAPStore;
    
    #region Buttons
    public Button AppReviewButton;
    public Button InAppPurchaseButton;
    #endregion

	void Start()
    {
        /// get screens
        _EventTracking = GetComponentInChildren<EventTrackingMenu>(true);
        _RegularAds = GetComponentInChildren<RegularAds>(true);
        _Debugger = GetComponentInChildren<DebuggingMenu>(true);
        
        /// get IAP
        _IAPStore = GetComponentInChildren<ExampleIAPStore>(true);

        // listen for attach events
        Analytics.OnAttachDefaultEventParams += UpdateEventParams;
        
        /// toggle buttons
#if LK_USE_APP_REVIEW
        AppReviewButton.interactable = true;
#else
        AppReviewButton.interactable = false;
#endif
        
#if LK_USE_UNITY_IAP
        InAppPurchaseButton.interactable = true;
#else
        InAppPurchaseButton.interactable = false;
#endif
    }

    void UpdateEventParams(Dictionary<string, object> eventParams)
    {
        if (eventParams == null)
            return;

        //eventParams[Analytics.Key.Param.level] = Random.Range(1, 100);
        //eventParams[Analytics.Key.Param.score] = Random.Range(1f, 1000f);
    }

	public void ShowConsentDialog()
	{
#if UNITY_EDITOR && LION_KIT_DEV
        LionStudios.GDPR.LionGDPR.ShowPromptDbg(0);
#else
        LionStudios.GDPR.LionGDPR.Show();
#endif
    }

	public void EventTracking()
	{
        _EventTracking.gameObject.SetActive(true);
    }

	public void RegularAds()
	{
        _RegularAds.gameObject.SetActive(true);
	}

	public void CrossPromoAds()
	{
        if (CrossPromo.Active)
        {
            CrossPromo.Hide();
        }
        else
        {
            CrossPromo.Show();
        }
    }

    public void Debugging()
    {
        LionDebugger.Show(ignoreInstallMode: true);
    }

    public void AppReview()
    {
#if LK_USE_APP_REVIEW
        LionInAppReview.TryGetReview();
#else
        UnityEngine.Debug.Log("In-App Review is disabled!");
#endif
    }

    public void InAppPurchase()
    {
#if LK_USE_UNITY_IAP && UNITY_PURCHASING
        /// open default IAP store
        _IAPStore.OpenStore();
#else
        UnityEngine.Debug.Log("In-App Purchase is disabled!");
#endif
    }
}
