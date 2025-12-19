using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerMain player;
    public Pool<EnemyBase> enemyPool;
    public EnemyBase enemyPrefab;
    public Pool<Bullet> enemyBulletPool;
    public Bullet enemyBulletPrefab;
    public Pool<Bullet> playerBulletPool;
    public Bullet playerBulletPrefab;
    public Pool<AOE> playerAOEPool;
    public AOE playerAOEPrefab;
    public Pool<AOE> enemyAOEPool;
    public AOE enemyAOEPrefab;
    public List<EnemyBase> activeEnemies;
    public Vector4 mapSize = new(-50, 50, -50, 50);
    public Transform parent;
    public TMP_Text roundText;
    public TMP_Text enemyCountText;
    public GameObject[] missionImages;
    public int enemyDeathCount = 0;
    public int eyeDeathCount = 0;
    public static bool devMode = false;
    public bool[] missions = new bool[4];

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
        EventManager.ClearEvents();
        enemyBulletPool = new Pool<Bullet>(new Factory<Bullet>().SetParent(parent).SetPrefab(enemyBulletPrefab), Bullet.SetState, 500);
        playerBulletPool = new Pool<Bullet>(new Factory<Bullet>().SetParent(parent).SetPrefab(playerBulletPrefab), Bullet.SetState);
        playerAOEPool = new Pool<AOE>(new Factory<AOE>().SetParent(parent).SetPrefab(playerAOEPrefab), AOE.SetState, 30);
        enemyAOEPool = new Pool<AOE>(new Factory<AOE>().SetParent(parent).SetPrefab(enemyAOEPrefab), AOE.SetState, 30);
        enemyPool = new Pool<EnemyBase>(new Factory<EnemyBase>().SetParent(parent).SetPrefab(enemyPrefab), EnemyBase.SetState, 30);
        activeEnemies = new List<EnemyBase>();

        enemyDeathCount = 0;
        eyeDeathCount = 0;
        foreach (var i in missionImages)
        {
            i.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        enemyCountText.text = activeEnemies.Count.ToString();
        roundText.text = RoundManager.instance.round.ToString();

        missions[0] = RoundManager.instance.round >= 5;
        missions[1] = MoneyManager.newMoney >= 3;
        missions[2] = enemyDeathCount >= 15;
        missions[3] = eyeDeathCount >= 2;
        bool completed = true;
        for (int i = 0; i < missions.Length; i++)
        {
            missionImages[i].gameObject.SetActive(missions[i]);
            if (missions[i] == false) completed = false;
        }
        if (completed)
        {
            enemyDeathCount = 0;
            missions[1] = false;
            ScreenManager.instance.Push("WinScreen");
        }
    }



}
