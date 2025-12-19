using UnityEngine;
using UnityEngine.Advertisements;

public class MoneyAd : RewardedAds
{
    public override void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == _id)
        {
            if (showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED)) MoneyManager.newMoney *= 2;
            else if (showCompletionState.Equals(UnityAdsShowCompletionState.SKIPPED)) MoneyManager.newMoney += (int)(MoneyManager.newMoney/4f);
            else if (showCompletionState.Equals(UnityAdsShowCompletionState.UNKNOWN)) Debug.Log("Huh");
            MoneyManager.money += MoneyManager.newMoney;
            MoneyManager.newMoney = 0;
        }
        LoadAd();
    }
}
