//using GoogleMobileAds.Api;
using System;
using UnityEngine;

/*
public class AdMobManager : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private int gold;

    private void Start()
    {
        InitAds();
    }

    private void InitAds()
    {
        string adUnitId;

#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";
#else
        adUnitId = "unexpected_platform";
#endif

        RewardedAd.Load(adUnitId, new AdRequest.Builder().Build(), LoadCallback);
    }

    private void LoadCallback(RewardedAd rewardedAd, LoadAdError loadAdError)
    {
        if (rewardedAd != null)
        {
            this.rewardedAd = rewardedAd;

            Debug.Log("로드 성공");
        }
        else
            Debug.Log(loadAdError.GetMessage());
    }

    public void ShowAds()
    {
        if (rewardedAd.CanShowAd())
            rewardedAd.Show(GetReward);
        else
            Debug.Log("광고 재생 실패");
    }

    private void GetReward(Reward reward)
    {
        gold += (int)reward.Amount;

        Debug.Log("골드 : " + gold.ToString());

        InitAds();
    }
}
*/
