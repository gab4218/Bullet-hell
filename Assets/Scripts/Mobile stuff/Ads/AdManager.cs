using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener
{

    [SerializeField] private bool _testing = true;
    public static AdManager instance;
    private string _androidAdsId = "6007961";
    [SerializeField] private RewardedAds _staminaAd;
    [SerializeField] private RewardedAds _moneyAd;
    [SerializeField] private InterstitialAds _interstitialAd;
    private string _androidRewardedAdsId = "Rewarded_Android";
    private string _androidInterstitialAdsId = "Interstitial_Android";
    public static bool changedOnce = false;



    public void OnInitializationComplete()
    {
        //if (_staminaAd == null) _staminaAd = GetComponent<RewardedAds>();
        _staminaAd?.Initialize(_androidRewardedAdsId);
        _moneyAd?.Initialize(_androidRewardedAdsId);
        _interstitialAd?.Initialize(_androidInterstitialAdsId);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        //throw new System.NotImplementedException();
    }

    public void RewardedAd() => _staminaAd.ShowAd();

    public void Dupe() => _moneyAd.ShowAd();

    public void InterstitialAd() => _interstitialAd.ShowAd();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_androidAdsId, _testing, this);
        }
        else
        {
            InterstitialAd();
        }
    }
}
