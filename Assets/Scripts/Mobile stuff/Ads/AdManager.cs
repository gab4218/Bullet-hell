using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener
{

    [SerializeField] private bool _testing = true;
    public static AdManager instance;
    private string _androidAdsId = "6007961";
    [SerializeField] private RewardedAds _staminaAd;
    [SerializeField] private RewardedAds _moneyAd;
    private string _androidRewardedAdsId = "Rewarded_Android";



    public void OnInitializationComplete()
    {
        //if (_staminaAd == null) _staminaAd = GetComponent<RewardedAds>();
        _staminaAd?.Initialize(_androidRewardedAdsId);
        _moneyAd?.Initialize(_androidRewardedAdsId);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        //throw new System.NotImplementedException();
    }

    public void RewardedAd() => _staminaAd.ShowAd();

    public void Dupe() => _moneyAd.ShowAd();

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
    }
}
