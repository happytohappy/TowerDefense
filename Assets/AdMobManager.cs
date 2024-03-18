using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class AdMobManager : MonoBehaviour
{
    [Header("안드로이드 id")]
    public string androidUnitId = "ca-app-pub-3940256099942544/5224354917";

    private string adUnitId;
    private RewardedAd rewardedAd;

    //public Text text;

    private void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            //초기화 완료
        });

#if UNITY_ANDROID
        adUnitId = androidUnitId;
#else
        adUnitId = "unexpected_platform";
#endif

        LoadRewardedAd();
    }

    // 광고 로드
    public void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");
        //text.text = "Loading the rewarded ad.";

        var adRequest = new AdRequest();
        RewardedAd.Load(adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.Log("Rewarded ad failed to load an ad " + "with error : " + error);
                //text.text = "Rewarded ad failed to load an ad " + "with error : " + error;

                return;
            }

            Debug.Log("Rewarded ad loaded with response: " + ad.GetResponseInfo());
            //text.text = "Rewarded ad loaded with response : " + ad.GetResponseInfo();

            rewardedAd = ad;
        });
    }

    // 광고 보기
    public void ShowAd()
    {
        const string rewardMsg = "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // 보상 획득
                Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
                //text.text = string.Format(rewardMsg, reward.Type, reward.Amount);
            });
        }
        else
        {
            LoadRewardedAd();
        }
    }

    // 광고 재로드
    private void RegisterReloadHandler(RewardedAd ad)
    {
        ad.OnAdFullScreenContentClosed += (null);
        {
            Debug.Log("Rewarded Ad full screen content closed.");
            //text.text = "Rewarded Ad full screen content closed.";

            LoadRewardedAd();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.Log("Rewarded ad failed to open full screen content " + "with error : " + error);
            //text.text = "Rewarded ad failed to open full screen content " + "with error : " + error;

            LoadRewardedAd();
        };
    }
}
