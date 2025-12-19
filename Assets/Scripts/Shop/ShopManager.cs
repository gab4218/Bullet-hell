using System;
using System.Collections.Generic;
using TMPro;
using Unity.Services.RemoteConfig;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour, IScreen
{
    public int defaultPrice = 10;

    public static ShopManager instance;

    public event Action OnPriceChange;

    public List<TMP_Text> text;

    Dictionary<Behaviour, bool> _priorState = new();

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
        MoneyManager.instance.text.AddRange(text);
    }

    void Start()
    {
        RemoteConfigService.Instance.FetchCompleted += ChangePrice;
    }
    
    public void Exit()
    {
        ScreenManager.instance.Pop();
    }

    private void ChangePrice(ConfigResponse configResponse)
    {
        defaultPrice = RemoteConfigService.Instance.appConfig.GetInt("BaseCost");
        OnPriceChange?.Invoke();
    }
    
    public void Inventory()
    {
        ScreenManager.instance.Push("Inventory");
    }

    public void Activate()
    {
        if (_priorState == default) return;
        foreach (var entry in _priorState)
        {
            entry.Key.enabled = entry.Value;
        }
        _priorState.Clear();
    }

    public void Deactivate()
    {
        _priorState = new();
        foreach (var b in GetComponentsInChildren<Behaviour>())
        {
            Debug.Log("asas");
            if (b is Canvas || b is CanvasScaler || b is Image || b is Camera || b is TMP_Text || b is AudioSource || b is AudioListener) continue;
            _priorState.Add(b, b.enabled);
            b.enabled = false;
        }
    }

    public void Free()
    {
        Destroy(gameObject);
    }
}
