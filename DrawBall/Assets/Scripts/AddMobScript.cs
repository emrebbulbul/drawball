using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AddMobScript : MonoBehaviour
{
    private InterstitialAd interstitial;
    public void RequestInterstitial()
    {
      
        string AdUnitId;
            #if UNITY_ANDROID
              AdUnitId = "ca-app-pub-2056532586207766/2426650571";
#elif UNITY_IPHONE
            AdUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
            AdUnitId="unexpected_platform";
#endif


        interstitial = new InterstitialAd(AdUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
        interstitial.OnAdClosed += InterstitialAdOff;
    }
    void InterstitialAdOff(object sender , EventArgs args)
    {
        interstitial.Destroy();
        RequestInterstitial();

    }
    public void InterstitialAdShow()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
            interstitial.Destroy();
            RequestInterstitial();
        }

    }


}
