using System.Collections.Generic;
using Unity.Services.RemoteConfig;
using UnityEngine;


public enum EnemyTypes
{
    TP_StarSpin, TP_StarNormal, Surround_AOE, Follow_Radial, Surround_Seeking, Static_Spinning
}
public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;
    public GameObject coin;
    public int round = 0;
    private float _coinDropChance = 0.05f;
    private float _baseEnemyHP = 5f;
    public Dictionary<EnemyTypes, int> prices = new Dictionary<EnemyTypes, int>()
    {
        { EnemyTypes.TP_StarSpin, 2 },
        { EnemyTypes.TP_StarNormal, 1 },
        { EnemyTypes.Surround_AOE, 3 },
        { EnemyTypes.Follow_Radial, 4 },
        { EnemyTypes.Surround_Seeking, 5 },
        { EnemyTypes.Static_Spinning, 1 }
    };

    public RuntimeAnimatorController[] enemyAnimators;
    public Sprite[] enemyBulletSprites;

    private bool _goodToGo = true;

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


    private void Start()
    {
        EventManager.Subscribe(EventType.RoundStart, StartRound);
        EventManager.TriggerEvent(EventType.RoundStart);
        RemoteConfigService.Instance.FetchCompleted += ChangeChance;
        RemoteConfigService.Instance.FetchCompleted += ChangeHP;
        _coinDropChance = RemoteConfigService.Instance.appConfig.GetFloat("CoinDropChance");
        _baseEnemyHP = 1;

    }
    private void ChangeChance(ConfigResponse configResponse)
    {
        _coinDropChance = RemoteConfigService.Instance.appConfig.GetFloat("CoinDropChance");
        Debug.Log("cd = " + _coinDropChance);
    }

    private void ChangeHP(ConfigResponse configResponse)
    {
        _baseEnemyHP = RemoteConfigService.Instance.appConfig.GetFloat("BaseEnemyHP");
    }

    private void Update()
    {
        if (GameManager.instance.activeEnemies.Count <= 0 && _goodToGo)
        {
            _goodToGo = false;
            ScreenManager.instance.Push("RoundClear");
        }
    }

    private void StartRound(params object[] p)
    {
        round++;
        _goodToGo = true;
        int currentPrice = 0;
        while (currentPrice < EnemyScaler.instance.enemyCurrency)
        {
            EnemyTypes t = (EnemyTypes)Random.Range(0, 6);

            if(round < 8)
            {
                if(round < 5)
                {
                    if (t == EnemyTypes.Surround_AOE || t == EnemyTypes.TP_StarSpin) continue;
                }
                if (t == EnemyTypes.Surround_Seeking) continue;
            }

            currentPrice += prices[t];

            if (currentPrice > EnemyScaler.instance.enemyCurrency)
            {
                currentPrice -= prices[t];
                continue;
            }

            var e = GameManager.instance.enemyPool.GetObject();
            e.SetCoin(coin).SetCoinDropChance(_coinDropChance).SetBaseHP(_baseEnemyHP).SetCreator(GameManager.instance.enemyPool);
            
            e.SetAnimator(enemyAnimators[(int)t]);
            SetPos(e.transform);
            switch (t)
            {
                case EnemyTypes.Surround_AOE:
                    e.SetBehaviour(new EnemySurround(), new EnemySingleBurst(enemyBulletSprites[(int)t])).SetSpeed(3).SetHPMultiplier(1.25f).SetCooldown(4);
                    break;
                case EnemyTypes.TP_StarSpin:
                    e.SetBehaviour(new EnemyTeleporter(), new EnemyStarSpin(enemyBulletSprites[(int)t])).SetSpeed(1).SetHPMultiplier(1.25f).SetCooldown(2);
                    break;
                case EnemyTypes.Static_Spinning:
                    Debug.Log("spin");
                    e.SetBehaviour(new EnemyStatic(), new EnemyConstantSpin(enemyBulletSprites[(int)t])).SetSpeed(0).SetHPMultiplier(2f).SetCooldown(0.2f);
                    break;
                case EnemyTypes.TP_StarNormal:
                    e.SetBehaviour(new EnemyTeleporter(), new EnemyStarBurst(enemyBulletSprites[(int)t])).SetSpeed(1).SetHPMultiplier(1f).SetCooldown(2);
                    break;
                case EnemyTypes.Follow_Radial:
                    Debug.Log("radial");
                    e.SetBehaviour(new EnemySurround(), new EnemyRadialAttack(enemyBulletSprites[(int)t])).SetSpeed(3).SetHPMultiplier(1f).SetCooldown(3);
                    break;
                case EnemyTypes.Surround_Seeking:
                    e.SetBehaviour(new EnemySurround(), new EnemySeekingBurst(enemyBulletSprites[(int)t])).SetSpeed(2).SetHPMultiplier(2f).SetCooldown(4);
                    break;
                default:
                    break;
            }
        }
    }
    public void SetPos(Transform t)
    {
        t.position = new Vector2(Random.Range(GameManager.instance.mapSize.x, GameManager.instance.mapSize.y), Random.Range(GameManager.instance.mapSize.z, GameManager.instance.mapSize.w));
        if (Vector2.Distance(t.position, GameManager.instance.player.transform.position) <= 2) SetPos(t);
    }
}
