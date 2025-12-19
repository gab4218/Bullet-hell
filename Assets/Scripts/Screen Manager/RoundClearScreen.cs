using System.Collections.Generic;
using UnityEngine;

public enum RewardTypes
{
    Heal, MaxHealth, Damage, Speed, Firerate, AOE, Seeking, Range, Resistance, BulletSpeed, BulletCount, Swirl, Piercing
}

public class RoundClearScreen : MonoBehaviour, IScreen
{
    [SerializeField] private UpgradeOption[] _upgradeObjects;

    private Dictionary<RewardTypes, int> _probabilities = new()
    {
        {RewardTypes.Heal, 15},
        {RewardTypes.Range, 15},
        {RewardTypes.Damage, 10},
        {RewardTypes.Speed, 10},
        {RewardTypes.BulletSpeed, 10},
        {RewardTypes.Firerate, 10},
        {RewardTypes.MaxHealth, 8},
        {RewardTypes.Swirl, 5},
        {RewardTypes.BulletCount, 5},
        {RewardTypes.Resistance, 5},
        {RewardTypes.Piercing, 3},
        {RewardTypes.Seeking, 2},
        {RewardTypes.AOE, 2}
    };

    [SerializeField] private Sprite[] _sprites;


    public void Activate()
    {
        RewardTypes[] rewards = new RewardTypes[2];

        for (int i = 0; i < 2; i++)
        {
            int rand = Random.Range(0, 100);
            int val = 0;
            bool allowed = false;
            foreach (var entry in _probabilities)
            {
                val += entry.Value;


                if (rand < val)
                {
                    if (entry.Key == RewardTypes.AOE && (GameManager.instance.player.hasAOE || !InventoryManager.unlockedItems[1])) break;
                    if (entry.Key == RewardTypes.Swirl && GameManager.instance.player.hasSwirl) break;
                    if (entry.Key == RewardTypes.Seeking && (GameManager.instance.player.hasSeek || !InventoryManager.unlockedItems[0])) break;
                    rewards[i] = entry.Key;
                    allowed = true;
                    break;
                }

            }
            if (!allowed)
            {
                Debug.Log("brokeEarly");
                Activate();
                return;
            }
        }

        if (rewards[0] == rewards[1])
        {
            Debug.Log("brokeLate");
            Activate();
            return;
        }

        _upgradeObjects[0].ChangeValues(rewards[0], _sprites[(int)rewards[0]]);
        _upgradeObjects[1].ChangeValues(rewards[1], _sprites[(int)rewards[1]]);
        
    }

    public void Deactivate()
    {
        
    }

    public void Free()
    {
        if (!PauseScreen.paused) EventManager.TriggerEvent(EventType.RoundStart);
        Destroy(gameObject);
    }

    public void PickUpgrade(UpgradeOption selection)
    {
        switch (selection.reward)
        {
            case RewardTypes.Heal:
                GameManager.instance.player.Heal(4);
                break;
            case RewardTypes.Swirl:
                GameManager.instance.player.Swirly();
                break;
            case RewardTypes.Seeking:
                GameManager.instance.player.Seeking();
                break;
            case RewardTypes.Firerate:
                GameManager.instance.player.UpgradeFireRate();
                break;
            case RewardTypes.Speed:
                GameManager.instance.player.UpgradeSpeed();
                break;
            case RewardTypes.MaxHealth:
                GameManager.instance.player.UpgradeHealth();
                break;
            case RewardTypes.AOE:
                GameManager.instance.player.AOE();
                break;
            case RewardTypes.BulletCount:
                GameManager.instance.player.UpgradeMultiShot();
                break;
            case RewardTypes.Damage:
                GameManager.instance.player.UpgradeDamage();
                break;
            case RewardTypes.Resistance:
                GameManager.instance.player.UpgradeResistance();
                break;
            case RewardTypes.Range:
                GameManager.instance.player.UpgradeRange();
                break;
            case RewardTypes.BulletSpeed:
                GameManager.instance.player.UpgradeBulletSpeed();
                break;
            default:
                Debug.Log("other");
                break;
        }
        ScreenManager.instance.Pop();
    }
}
