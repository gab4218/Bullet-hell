using UnityEngine;

public class EnemySingleBurst : IEnemyAttack
{
    private float _cd = 0;
    private float _t;
    private int _index = 0;
    public void Attack(Transform transform, float cooldown, float speed)
    {
        _cd += Time.deltaTime;
        if (_cd < cooldown) return;
        if (_index < 5)
        {
            _t += Time.deltaTime;
            if (_t < 0.2f) return;
            var b = GameManager.instance.enemyBulletPool.GetObject();
            _t = 0f;
            _index++;
            b.rotationalSpeed = 0;
            b.speed = speed;
            b.SetCreator(GameManager.instance.enemyBulletPool);
            ((BulletBase)b.bullet).SetLifetime(8).SetSpeed(speed);
            b.Shoot((Vector2)(GameManager.instance.player.transform.position - transform.position).normalized, transform.position);
            return;
        }
        _t = 0;
        _index = 0;
        _cd = 0;
    }
}
