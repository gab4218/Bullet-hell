using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{ 
    string _id;
    static int _sceneChangeCount = 2;

    public void Initialize(string id)
    {
        _id = id;
        LoadAd();
        Debug.Log("SceneCC = " + _sceneChangeCount);
    }

    void LoadAd() => Advertisement.Load(_id, this);

    public void OnUnityAdsAdLoaded(string placementId)
    {
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log(message);
    }

    public void ShowAd()
    {
        _sceneChangeCount--;
        if (_sceneChangeCount <= 0)
        {
            Advertisement.Show(_id, this);
            _sceneChangeCount = 3;
        }
    }

    public void OnUnityAdsShowClick(string placementId)
    { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        LoadAd();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log(message);
    }

    public void OnUnityAdsShowStart(string placementId)
    { }
}


