using UnityEngine;

public class EnemyConstantSpin : IEnemyAttack
{
    private float _cd = 0;
    private int _i = 0;
    private Sprite _bulletSprite;

    public EnemyConstantSpin(Sprite spr) => _bulletSprite = spr;

    public void Attack(Transform transform, float cooldown, float speed, Animator anim, AudioSource audio, AudioClip sound)
    {
        _cd += Time.deltaTime;
        if (_cd < cooldown) return;
        _cd = 0;
        _i++;
        audio.PlayOneShot(sound);
        var b = GameManager.instance.enemyBulletPool.GetObject();
        b.rotationalSpeed = 0;
        b.speed = speed;
        b.UpdateSprite(_bulletSprite);
        b.SetCreator(GameManager.instance.enemyBulletPool);

        ((BulletBase)b.bullet).SetLifetime(8).SetSpeed(speed);
        b.Shoot(Quaternion.AngleAxis(20 * _i, Vector3.forward) * Vector2.up, transform.position);

        if (_i > 17) _i = 0;
        
    }
}
