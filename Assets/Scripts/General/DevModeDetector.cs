using Unity.Services.RemoteConfig;
using UnityEngine;

public class DevModeDetector : MonoBehaviour
{
    public static DevModeDetector instance;
    public bool devMode;
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

    }

    void Start()
    {
        RemoteConfigService.Instance.FetchCompleted += DetectDev;
    }

    private void DetectDev(ConfigResponse configResponse)
    {
        devMode = RemoteConfigService.Instance.appConfig.GetBool("DevMode");
    }
}
