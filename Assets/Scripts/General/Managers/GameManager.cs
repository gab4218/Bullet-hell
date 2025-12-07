using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerMain player;
    public Pool<Bullet> enemyBulletPool;
    public Bullet enemyBulletPrefab;
    public Pool<Bullet> playerBulletPool;
    public Bullet playerBulletPrefab;
    public Pool<AOE> playerAOEPool;
    public AOE playerAOEPrefab;
    public Pool<AOE> enemyAOEPool;
    public AOE enemyAOEPrefab;
    public List<EnemyBase> activeEnemies;

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
        enemyBulletPool = new Pool<Bullet>(new Factory<Bullet>().SetPrefab(enemyBulletPrefab), Bullet.SetState);
        playerBulletPool = new Pool<Bullet>(new Factory<Bullet>().SetPrefab(playerBulletPrefab), Bullet.SetState);
        playerAOEPool = new Pool<AOE>(new Factory<AOE>().SetPrefab(playerAOEPrefab), AOE.SetState, 30);
        enemyAOEPool = new Pool<AOE>(new Factory<AOE>().SetPrefab(enemyAOEPrefab), AOE.SetState, 30);
        activeEnemies = new List<EnemyBase>();
    }
    

    

    
}
