using UnityEngine;

public class BulletUpgrade : IBullet
{
    protected IBullet bullet;

    public virtual float damage => bullet.damage;

    public virtual bool collidable => bullet.collidable;

    public BulletUpgrade(IBullet bullet)
    {
        this.bullet = bullet;
    }

    public virtual void EndLife()
    {
        bullet.EndLife();
    }

    public virtual void OnHit(IHittable hit)
    {
        bullet.OnHit(hit);
    }

    public virtual void OnUpdate(Transform transform)
    {
        bullet.OnUpdate(transform);
    }
}
