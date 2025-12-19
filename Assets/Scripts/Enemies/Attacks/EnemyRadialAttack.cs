using UnityEngine;

public class EnemyRadialAttack : IEnemyAttack
{
    private float _cd = 0;
    private Sprite _bulletSprite;

    public EnemyRadialAttack(Sprite spr) => _bulletSprite = spr;

    public void Attack(Transform transform, float cooldown, float speed, Animator anim, AudioSource audio, AudioClip sound)
    {
        _cd += Time.deltaTime;
        if (_cd < cooldown) return;
        _cd = 0;
        var b = GameManager.instance.enemyBulletPool.GetObject();
        b.rotationalSpeed = 0;
        b.speed = speed;
        b.UpdateSprite(_bulletSprite);
        b.SetCreator(GameManager.instance.enemyBulletPool);
        anim.SetTrigger("Shot");
        audio.PlayOneShot(sound);
        ((BulletBase)b.bullet).SetLifetime(8).SetSpeed(speed);
        b.Shoot(GameManager.instance.player.transform.position - transform.position, transform.position);

        b.Swirl();

        for (int i = 1; i < 3; i++)
        {
            b = GameManager.instance.enemyBulletPool.GetObject();
            b.rotationalSpeed = 0;
            b.speed = speed;
            b.UpdateSprite(_bulletSprite);
            b.SetCreator(GameManager.instance.enemyBulletPool);

            ((BulletBase)b.bullet).SetLifetime(8).SetSpeed(speed);
            b.Shoot(Quaternion.AngleAxis(10 * i, Vector3.forward) * (GameManager.instance.player.transform.position - transform.position), transform.position);

            b.Swirl();
        }
        for (int i = 1; i < 3; i++)
        {
            b = GameManager.instance.enemyBulletPool.GetObject();
            b.rotationalSpeed = 0;
            b.speed = speed;
            b.UpdateSprite(_bulletSprite);
            b.SetCreator(GameManager.instance.enemyBulletPool);

            ((BulletBase)b.bullet).SetLifetime(8).SetSpeed(speed);
            b.Shoot(Quaternion.AngleAxis(-10 * i, Vector3.forward) * (GameManager.instance.player.transform.position - transform.position), transform.position);

            b.Swirl();
        }
    }
}
