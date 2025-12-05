using UnityEngine;
public abstract class BulletBase : IBullet
{
    protected Rigidbody2D rb;
    protected Vector2 origin;
    private float _damage = 1;
    public float damage { get => _damage; }
    protected Pool<BulletBase> creator;
    protected bool _collidable = true;
    public bool collidable => _collidable;
    protected float maxLifeTime = 4f;
    protected float currentLifeTime = 0;
    protected bool piercing;
    protected float speed = 1;
    protected float rotationalSpeed = 0;


    public BulletBase(Rigidbody2D r) 
    {
        rb = r;
    }
    
    public BulletBase SetSpeed(float s = 1)
    {
        speed = s;
        return this;
    }

    public BulletBase SetLifetime(float l = 4)
    {
        maxLifeTime = l;
        return this;
    }

    public BulletBase SetCollidable(bool col = true)
    {
        _collidable = col;
        return this;
    }

    public BulletBase SetRotationalSpeed(float rs = 0)
    {
        rotationalSpeed = rs;
        return this;
    }

    public BulletBase SetDamage(float d = 1)
    {
        _damage = d;
        return this;
    }

    public BulletBase SetPiercing(bool p = false)
    {
        piercing = p;
        return this;
    }

    private void OnEnabled()
    {
        currentLifeTime = 0;
    }

    public void Shoot(Vector2 dir, Vector2 originPoint)
    {
        rb.velocity = dir.normalized * speed;
        origin = originPoint;
    }

    public void OnUpdate(Transform transform)
    {
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= maxLifeTime) EndLife();
        if (rotationalSpeed == 0) return;
        transform.Rotate(origin, rotationalSpeed * Time.deltaTime);
    }

    public void EndLife()
    {
        creator.Return(this);
    }

    public void SetPool(Pool<BulletBase> pool)
    {
        creator = pool;
    }

    public void OnHit(IHittable entity)
    {
        entity.OnHit(damage);
    }

}

public enum DamageTypes
{
    Normal, Fire, Ice, Poison
}
