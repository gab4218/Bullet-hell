using UnityEngine;
using Unity.Services.RemoteConfig;

public class Coin : MonoBehaviour
{
    private int _coinVal = 1;
    private void Start()
    {
        RemoteConfigService.Instance.FetchCompleted += ChangeMoney;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMain>())
        {
            MoneyManager.newMoney += _coinVal;
            Destroy(gameObject);
        }
    }

    private void ChangeMoney(ConfigResponse configResponse)
    {
        _coinVal = RemoteConfigService.Instance.appConfig.GetInt("CoinValue");
    }
}
