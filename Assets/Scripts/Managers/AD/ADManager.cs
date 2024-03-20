using GoogleMobileAds.Api;
using UnityEngine;
using System;

public class ADManager : MonoBehaviour
{
    private string adUnitId;
    private RewardedAd rewardedAd;

    public bool AdmobInit { get; private set; }

    public void Init()
    {
        AdmobInit = false;
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            AdmobInit = true;
            Debug.Log("±¸±Û ¾Öµå¸÷ ÃÊ±âÈ­ ¿Ï·á");
        });

#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";
#else
        adUnitId = "unexpected_platform";
#endif

        LoadRewardedAd();
    }

    // ±¤°í ·Îµå
    public void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        var adRequest = new AdRequest();
        RewardedAd.Load(adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.Log("Rewarded ad failed to load an ad " + "with error : " + error);

                return;
            }

            Debug.Log("Rewarded ad loaded with response: " + ad.GetResponseInfo());

            rewardedAd = ad;
        });
    }

    // ±¤°í º¸±â
    public void ShowAd(Action in_callback)
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // º¸»ó È¹µæ
                if (in_callback != null)
                    in_callback.Invoke();
            });
        }
        else
        {
            LoadRewardedAd();
        }
    }

    // ±¤°í Àç·Îµå
    private void RegisterReloadHandler(RewardedAd ad)
    {
        ad.OnAdFullScreenContentClosed += (null);
        {
            Debug.Log("Rewarded Ad full screen content closed.");

            LoadRewardedAd();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.Log("Rewarded ad failed to open full screen content " + "with error : " + error);

            LoadRewardedAd();
        };
    }
}
