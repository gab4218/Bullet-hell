using UnityEngine;

public class EnemyConstantSpin : IEnemyAttack
{
    private float _cd = 0;
    private int _i = 0;
    public void Attack(Transform transform, float cooldown, float speed)
    {
        _cd += Time.deltaTime;
        if (_cd < cooldown) return;
        _cd = 0;
        _i++;

        var b = GameManager.instance.enemyBulletPool.GetObject();
        b.rotationalSpeed = 0;
        b.speed = speed;
        b.SetCreator(GameManager.instance.enemyBulletPool);

        ((BulletBase)b.bullet).SetLifetime(8).SetSpeed(speed);
        b.Shoot(Quaternion.AngleAxis(20 * _i, Vector3.forward) * Vector2.up, transform.position);

        if (_i > 17) _i = 0;
        
    }
}
