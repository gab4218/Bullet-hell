using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum EnemyTypes
{
    TP_StarSpin, TP_StarNormal, Follower_Burst, Surround_Radial, Surround_Seeking, Random_Star, Static_Spinning
}
public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;
    public int round = 0;
    public Dictionary<EnemyTypes, int> prices = new Dictionary<EnemyTypes, int>()
    {
        { EnemyTypes.TP_StarSpin, 2 },
        { EnemyTypes.TP_StarNormal, 1 },
        { EnemyTypes.Follower_Burst, 3 },
        { EnemyTypes.Surround_Radial, 4 },
        { EnemyTypes.Surround_Seeking, 5 },
        { EnemyTypes.Random_Star, 1 },
        { EnemyTypes.Static_Spinning, 1 }
    };

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

    }

    private void Update()
    {
        if (GameManager.instance.activeEnemies.Count <= 0)
        {
            EventManager.TriggerEvent(EventType.RoundClear);
        }
    }

    private void StartRound()
    {
        round++;
        int currentPrice = 0;
        while (currentPrice < EnemyScaler.instance.enemyCurrency)
        {
            EnemyTypes t = (EnemyTypes)Random.Range(0, 7);

            if(round < 8)
            {
                if(round < 5)
                {
                    if (t == EnemyTypes.Follower_Burst || t == EnemyTypes.TP_StarSpin) continue;
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

            SetPos(e.transform);

            switch (t)
            {
                case EnemyTypes.Follower_Burst:
                    e.SetBehaviour(new EnemyBlindChase(), new EnemySingleBurst());
                    break;
                case EnemyTypes.TP_StarSpin:
                    e.SetBehaviour(new EnemyTeleporter(), new EnemyStarSpin());
                    break;
                case EnemyTypes.Static_Spinning:
                    e.SetBehaviour(new EnemyStatic(), new EnemyConstantSpin());
                    break;
                case EnemyTypes.TP_StarNormal:
                    e.SetBehaviour(new EnemyTeleporter(), new EnemyStarBurst());
                    break;
                case EnemyTypes.Surround_Radial:
                    e.SetBehaviour(new EnemySurround(), new EnemyRadialAttack());
                    break;
                case EnemyTypes.Random_Star:
                    e.SetBehaviour(new EnemyRandomMovement(), new EnemyStarBurst());
                    break;
                case EnemyTypes.Surround_Seeking:
                    e.SetBehaviour(new EnemySurround(), new EnemySeekingBurst());
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
