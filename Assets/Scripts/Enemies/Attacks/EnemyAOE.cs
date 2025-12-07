using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAOE : IEnemyAttack
{
    private float _cd = 0;
    public void Attack(Transform transform, float cooldown, float speed)
    {
        _cd += Time.deltaTime;
        if (_cd < cooldown) return;
        _cd = 0;
        var b = GameManager.instance.enemyBulletPool.GetObject();
        b.rotationalSpeed = 0;
        b.speed = speed;
        b.lifetime = 2;
        b.SetCreator(GameManager.instance.enemyBulletPool);

        ((BulletBase)b.bullet).SetLifetime(2).SetSpeed(speed);
        b.Shoot(GameManager.instance.player.transform.position - transform.position, transform.position);

        b.SetAOE(GameManager.instance.enemyAOEPool);
    }
}
