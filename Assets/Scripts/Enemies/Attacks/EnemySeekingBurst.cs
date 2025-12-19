using UnityEngine;

public class EnemySeekingBurst : IEnemyAttack
{
    private float _cd = 0;
    private float _t;
    private int _index = 0;
    private Sprite _bulletSprite;

    public EnemySeekingBurst(Sprite spr) => _bulletSprite = spr;

    public void Attack(Transform transform, float cooldown, float speed, Animator anim, AudioSource audio, AudioClip sound)
    {
        _cd += Time.deltaTime;
        if (_cd < cooldown) return;
        if (_index < 4)
        {
            _t += Time.deltaTime;
            if (_t < 0.5f) return;
            anim.SetTrigger("Shot");
            audio.PlayOneShot(sound);
            var b = GameManager.instance.enemyBulletPool.GetObject();
            _t = 0f;
            _index++;
            b.rotationalSpeed = 0;
            b.speed = speed;
            b.UpdateSprite(_bulletSprite);
            b.SetCreator(GameManager.instance.enemyBulletPool);
            ((BulletBase)b.bullet).SetLifetime(8).SetSpeed(speed);
            b.Shoot((Vector2)(GameManager.instance.player.transform.position - transform.position).normalized, transform.position);
            b.Seeking(true);
            return;
        }
        _t = 0;
        _index = 0;
        _cd = 0;
    }
}
