using UnityEngine;

public class EnemyStarBurst : IEnemyAttack
{
    private float _cd = 0;
    private Sprite _bulletSprite;

    public EnemyStarBurst(Sprite spr) => _bulletSprite = spr;

    public void Attack(Transform transform, float cooldown, float speed, Animator anim)
    {
        _cd += Time.deltaTime;
        if (_cd < cooldown) return;
        _cd = 0;
        anim.SetTrigger("Shot");
        for (int i = 0; i < 8; i++)
        {
            var b = GameManager.instance.enemyBulletPool.GetObject();
            b.rotationalSpeed = 0;
            b.speed = speed;
            b.UpdateSprite(_bulletSprite);
            b.SetCreator(GameManager.instance.enemyBulletPool);
            ((BulletBase)b.bullet).SetLifetime(8).SetSpeed(speed);
            b.Shoot((Vector2)(Quaternion.AngleAxis(45 * i, Vector3.forward) * Vector2.up).normalized, transform.position);
        }
    }
}
