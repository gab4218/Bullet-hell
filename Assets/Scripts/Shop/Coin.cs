using UnityEngine;
using Unity.Services.RemoteConfig;

public class Coin : MonoBehaviour
{
    private int _coinVal = 1;
    [SerializeField] private AudioSource _source;

    private void Start()
    {
        RemoteConfigService.Instance.FetchCompleted += ChangeMoney;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMain>())
        {
            MoneyManager.newMoney += _coinVal;
            var d = Instantiate(_source, transform.position, Quaternion.identity);
            d.Play();
            Destroy(d.gameObject, d.clip.length);
            Destroy(gameObject);
        }
    }

    private void ChangeMoney(ConfigResponse configResponse)
    {
        _coinVal = RemoteConfigService.Instance.appConfig.GetInt("CoinValue");
    }
}
